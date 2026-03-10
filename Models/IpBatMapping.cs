using System;

namespace SelectMaster.Models
{
    /// <summary>
    /// IP地址与Bat文件映射模型
    /// </summary>
    public class IpBatMapping
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 优先级（数字越小优先级越高）
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// HTTP地址
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Bat文件路径
        /// </summary>
        public string BatFilePath { get; set; } = string.Empty;

        /// <summary>
        /// 超时时间（毫秒），默认5000ms
        /// </summary>
        public int Timeout { get; set; } = 5000;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public IpBatMapping()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"[优先级:{Priority}] {Url} -> {BatFilePath}";
        }
    }
}
