using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SelectMaster.Models;

namespace SelectMaster.Services
{
    /// <summary>
    /// 配置文件管理服务
    /// </summary>
    public class ConfigManager
    {
        private const string ConfigFileName = "config.json";
        private readonly string _configFilePath;

        public ConfigManager(string? basePath = null)
        {
            _configFilePath = basePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
        }

        /// <summary>
        /// 保存配置到文件
        /// </summary>
        public void SaveConfig(List<IpBatMapping> mappings)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var json = JsonSerializer.Serialize(mappings, options);
                File.WriteAllText(_configFilePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"保存配置失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从文件加载配置
        /// </summary>
        public List<IpBatMapping> LoadConfig()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    return new List<IpBatMapping>();
                }

                var json = File.ReadAllText(_configFilePath);
                var mappings = JsonSerializer.Deserialize<List<IpBatMapping>>(json);

                return mappings ?? new List<IpBatMapping>();
            }
            catch (Exception ex)
            {
                throw new Exception($"加载配置失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        public string GetConfigFilePath()
        {
            return _configFilePath;
        }
    }
}
