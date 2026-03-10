using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SelectMaster.Models
{
    /// <summary>
    /// 完整配置类（包含映射和定时器配置）
    /// </summary>
    public class CompleteConfig
    {
        [JsonPropertyName("mappings")]
        public List<IpBatMapping> Mappings { get; set; } = new List<IpBatMapping>();

        [JsonPropertyName("timerConfig")]
        public TimerConfig TimerConfig { get; set; } = new TimerConfig();
    }
}
