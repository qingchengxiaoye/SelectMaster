using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SelectMaster.Models;

namespace SelectMaster.Services
{
    /// <summary>
    /// 定时执行服务
    /// </summary>
    public class TimerService : IDisposable
    {
        private readonly System.Threading.Timer _timer;
        private readonly HttpChecker _httpChecker;
        private readonly BatExecutor _batExecutor;
        private TimerConfig? _config;
        private List<IpBatMapping>? _mappings;
        private Action<string>? _logAction;
        private Action<IpBatMapping?>? _currentMappingChangedAction;
        private bool _isRunning = false;
        private int? _currentExecutingPriority;
        private bool _callbackRunning; // 保证定时任务串行，上一次执行未结束不触发下一次

        public TimerService(HttpChecker httpChecker, BatExecutor batExecutor)
        {
            _httpChecker = httpChecker;
            _batExecutor = batExecutor;
            _timer = new System.Threading.Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// 配置并启动定时器
        /// </summary>
        public void Start(TimerConfig config, List<IpBatMapping> mappings, Action<string> logAction, Action<IpBatMapping?>? currentMappingChangedAction = null)
        {
            _config = config;
            _mappings = mappings;
            _logAction = logAction;
            _currentMappingChangedAction = currentMappingChangedAction;

            if (!config.Enabled)
            {
                _isRunning = false;
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                return;
            }

            // 每次启动时重置执行次数和当前执行的优先级
            config.ExecutedCount = 0;
            _currentExecutingPriority = null;
            _currentMappingChangedAction?.Invoke(null); // 通知当前执行的映射已清除
            _isRunning = true;

            // 计算下一次执行时间
            var nextTime = DateTime.Now.AddSeconds(config.IntervalSeconds);
            config.NextExecutionTime = nextTime.ToString("yyyy-MM-dd HH:mm:ss");

            // 使用“单次触发 + 执行完再排期”实现串行：必须等上一次任务执行完成才排下一次
            int dueMs = config.IntervalSeconds * 1000;
            _timer.Change(dueMs, Timeout.Infinite);

            string executionInfo = config.MaxExecutionCount < 0 ? "一直执行" : $"最多执行{config.MaxExecutionCount}次";
            _logAction?.Invoke($"定时执行已启动，间隔: {config.IntervalSeconds}秒，{executionInfo}");
            _logAction?.Invoke($"下次执行时间: {config.NextExecutionTime}");
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _logAction?.Invoke("定时执行已停止");
        }

        /// <summary>
        /// 定时器回调（串行：本次执行结束后才排下一次）
        /// </summary>
        private async void TimerCallback(object? state)
        {
            if (!_isRunning || _config == null || _mappings == null)
                return;
            if (_callbackRunning)
                return; // 上一轮未结束，本次直接忽略
            _callbackRunning = true;
            try
            {
                await RunOneCycleAsync();
            }
            finally
            {
                _callbackRunning = false;
                // 串行：仅在本轮执行结束后再排下一次
                if (_isRunning && _config != null)
                {
                    int dueMs = _config.IntervalSeconds * 1000;
                    _timer.Change(dueMs, Timeout.Infinite);
                }
            }
        }

        private async Task RunOneCycleAsync()
        {
            if (_config == null! || _mappings == null!)
                return;
            if (_config.MaxExecutionCount >= 0 && _config.ExecutedCount >= _config.MaxExecutionCount)
            {
                Stop();
                _logAction?.Invoke($"已达到最大执行次数限制 ({_config.MaxExecutionCount}次)，定时执行已停止");
                return;
            }

            _logAction?.Invoke($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========== 定时执行开始 ==========");
            _logAction?.Invoke($"这是第 {_config.ExecutedCount + 1} 次执行");

            try
            {
                // 更新最后执行时间
                _config.LastExecutionTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _config.ExecutedCount++;

                // 按优先级排序并过滤已启用的
                var sortedMappings = _mappings
                    .Where(m => m.Enabled)
                    .OrderBy(m => m.Priority)
                    .ToList();

                if (!sortedMappings.Any())
                {
                    _logAction?.Invoke("没有启用的映射关系！");
                    return;
                }

                _logAction?.Invoke($"开始检查 {sortedMappings.Count} 个映射地址...");

                bool found = false;
                int? accessiblePriority = null;

                // 如果之前有执行的映射，检查它是否仍然可访问
                if (_currentExecutingPriority.HasValue)
                {
                    var currentMapping = sortedMappings.FirstOrDefault(m => m.Priority == _currentExecutingPriority.Value);
                    if (currentMapping != null)
                    {
                        _logAction?.Invoke($"[检查当前执行的映射] 优先级 {currentMapping.Priority}: {currentMapping.Url}");
                        int timeoutMs = _config.RequestTimeoutMs > 0 ? _config.RequestTimeoutMs : currentMapping.Timeout;
                        var currentResult = await _httpChecker.CheckUrlAsync(
                            currentMapping.Url,
                            timeoutMs,
                            _config.RequestRetryCount,
                            _config.RequestRetryDelayMs);

                        if (currentResult.IsAccessible)
                        {
                            _logAction?.Invoke($"  ✓ 当前映射仍然可访问");
                            accessiblePriority = currentMapping.Priority;
                        }
                        else
                        {
                            _logAction?.Invoke($"  ✗ 当前映射已不可访问: {currentResult.Message}");
                        }
                    }
                    else
                    {
                        _logAction?.Invoke($"当前优先级 {_currentExecutingPriority.Value} 的映射已不存在");
                    }
                }

                // 按优先级检查所有映射
                foreach (var mapping in sortedMappings)
                {
                    _logAction?.Invoke($"[优先级 {mapping.Priority}] 正在检查: {mapping.Url}");
                    int timeoutMs = _config.RequestTimeoutMs > 0 ? _config.RequestTimeoutMs : mapping.Timeout;
                    var result = await _httpChecker.CheckUrlAsync(
                        mapping.Url,
                        timeoutMs,
                        _config.RequestRetryCount,
                        _config.RequestRetryDelayMs);

                    if (result.IsAccessible)
                    {
                        _logAction?.Invoke($"  ✓ 可访问! 响应时间: {result.ResponseTime}ms");

                        // 检查是否需要执行：
                        // 1. 当前映射不可访问，或者
                        // 2. 发现了更高优先级的可访问映射（更小的优先级数字表示更高优先级）
                        bool shouldExecute = !accessiblePriority.HasValue || mapping.Priority < accessiblePriority.Value;

                        if (shouldExecute)
                        {
                            _logAction?.Invoke($"  将执行Bat文件: {mapping.BatFilePath}");

                            // 使用弹窗执行bat文件
                            var execResult = await _batExecutor.ExecuteBatWithWindowAsync(mapping.BatFilePath);

                            _logAction?.Invoke($"执行结果: {(execResult.Success ? "成功" : "失败")}, 退出码: {execResult.ExitCode}");

                            if (execResult.Success)
                            {
                                // 只有执行成功时才更新当前优先级
                                _currentExecutingPriority = mapping.Priority;
                                _logAction?.Invoke($"已更新当前执行优先级: {mapping.Priority}");
                                _currentMappingChangedAction?.Invoke(mapping); // 通知当前执行的映射已更改
                            }

                            found = true;
                            break;
                        }
                        else
                        {
                            _logAction?.Invoke($"  该映射优先级不高于当前执行映射，跳过");
                        }
                    }
                    else
                    {
                        _logAction?.Invoke($"  ✗ 不可访问: {result.Message}");
                    }
                }

                if (!found)
                {
                    _logAction?.Invoke("没有找到需要执行的映射（所有不可访问或优先级不高于当前）");
                }

                // 更新下次执行时间
                var nextTime = DateTime.Now.AddSeconds(_config.IntervalSeconds);
                _config.NextExecutionTime = nextTime.ToString("yyyy-MM-dd HH:mm:ss");

                if (_config.MaxExecutionCount < 0)
                {
                    _logAction?.Invoke($"下次执行时间: {_config.NextExecutionTime} (一直执行)");
                }
                else
                {
                    _logAction?.Invoke($"下次执行时间: {_config.NextExecutionTime} (还将执行{_config.MaxExecutionCount - _config.ExecutedCount}次)");
                }
            }
            catch (Exception ex)
            {
                _logAction?.Invoke($"定时执行出错: {ex.Message}");
            }

            _logAction?.Invoke($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========== 定时执行结束 ==========" + Environment.NewLine);
        }

        /// <summary>
        /// 获取定时器状态
        /// </summary>
        public (bool IsRunning, TimerConfig Config) GetStatus()
        {
            return (_isRunning, _config ?? new TimerConfig());
        }

        public void Dispose()
        {
            Stop();
            _timer?.Dispose();
        }
    }
}
