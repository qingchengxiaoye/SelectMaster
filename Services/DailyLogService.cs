using System;
using System.IO;

namespace SelectMaster.Services
{
    /// <summary>
    /// 按天滚动的日志文件写入服务，每天一个文件
    /// </summary>
    public class DailyLogService
    {
        private readonly string _logDirectory;
        private readonly string _logFilePrefix;
        private readonly object _lock = new object();
        private string _currentDate = "";
        private StreamWriter? _writer;

        public DailyLogService(string? logDirectory = null, string logFilePrefix = "SelectMaster")
        {
            _logDirectory = logDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            _logFilePrefix = logFilePrefix;
        }

        /// <summary>
        /// 写入一行日志到当日日志文件，并自动按日期切换文件
        /// </summary>
        public void WriteLine(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            lock (_lock)
            {
                try
                {
                    string today = DateTime.Now.ToString("yyyy-MM-dd");
                    if (_currentDate != today)
                    {
                        _writer?.Dispose();
                        _writer = null;
                        _currentDate = today;
                    }

                    if (!Directory.Exists(_logDirectory))
                        Directory.CreateDirectory(_logDirectory);

                    string fileName = $"{_logFilePrefix}_{_currentDate}.log";
                    string filePath = Path.Combine(_logDirectory, fileName);

                    _writer ??= new StreamWriter(filePath, append: true) { AutoFlush = true };
                    _writer.WriteLine(message);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DailyLogService WriteLine 失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 释放当前写入器（切换日期时会自动重新创建）
        /// </summary>
        public void Dispose()
        {
            lock (_lock)
            {
                _writer?.Dispose();
                _writer = null;
            }
        }
    }
}
