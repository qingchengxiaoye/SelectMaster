using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SelectMaster.Models;

namespace SelectMaster.Services
{
    /// <summary>
    /// HTTP地址可访问性检查服务
    /// </summary>
    public class HttpChecker
    {
        /// <summary>
        /// 检查地址是否可访问，支持失败后重试
        /// </summary>
        /// <param name="url">要检查的地址</param>
        /// <param name="timeout">超时时间（毫秒）</param>
        /// <param name="retryCount">失败后的重试次数，0 表示不重试</param>
        /// <param name="retryDelayMs">每次重试前的等待时间（毫秒）</param>
        public async Task<(bool IsAccessible, string Message, long ResponseTime)> CheckUrlAsync(
            string url,
            int timeout = 5000,
            int retryCount = 0,
            int retryDelayMs = 500)
        {
            int maxAttempts = 1 + Math.Max(0, retryCount);
            string? lastMessage = null;
            long lastResponseTime = 0;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                var result = await CheckUrlOnceAsync(url, timeout);
                lastMessage = result.Message;
                lastResponseTime = result.ResponseTime;

                if (result.IsAccessible)
                    return result;

                if (attempt < maxAttempts && retryDelayMs > 0)
                    await Task.Delay(retryDelayMs);
            }

            return (false, lastMessage ?? "未知错误", lastResponseTime);
        }

        /// <summary>
        /// 单次请求，不重试
        /// </summary>
        private async Task<(bool IsAccessible, string Message, long ResponseTime)> CheckUrlOnceAsync(string url, int timeout)
        {
            try
            {
                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromMilliseconds(timeout)
                };

                var startTime = DateTime.UtcNow;
                var response = await httpClient.GetAsync(url);
                var responseTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds;

                if (response.IsSuccessStatusCode)
                {
                    return (true, $"访问成功 (状态码: {(int)response.StatusCode})", responseTime);
                }
                else
                {
                    return (false, $"访问失败 (状态码: {(int)response.StatusCode})", responseTime);
                }
            }
            catch (TaskCanceledException)
            {
                return (false, "连接超时", 0);
            }
            catch (HttpRequestException ex)
            {
                return (false, $"连接错误: {ex.Message}", 0);
            }
            catch (Exception ex)
            {
                return (false, $"未知错误: {ex.Message}", 0);
            }
        }

        /// <summary>
        /// 检查多个地址的可访问性，返回第一个可访问的映射
        /// </summary>
        public async Task<(IpBatMapping? AccessibleMapping, string Message, long ResponseTime)> FindFirstAccessibleAsync(
            System.Collections.Generic.List<IpBatMapping> mappings,
            int retryCount = 0,
            int retryDelayMs = 500)
        {
            if (mappings == null || mappings.Count == 0)
            {
                return (null, "没有配置映射关系", 0);
            }

            var sortedMappings = mappings
                .Where(m => m.Enabled)
                .OrderBy(m => m.Priority)
                .ToList();

            if (!sortedMappings.Any())
            {
                return (null, "没有启用的映射关系", 0);
            }

            foreach (var mapping in sortedMappings)
            {
                var result = await CheckUrlAsync(mapping.Url, mapping.Timeout, retryCount, retryDelayMs);
                if (result.IsAccessible)
                {
                    return (mapping, $"地址 [{mapping.Url}] 可访问", result.ResponseTime);
                }
            }

            return (null, "所有地址均不可访问", 0);
        }
    }
}
