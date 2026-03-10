namespace SelectMaster.Models
{
    /// <summary>
    /// 定时执行配置模型
    /// </summary>
    public class TimerConfig
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 是否启用定时执行
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// 是否一直执行（无限次）
        /// </summary>
        public bool InfiniteExecution { get; set; } = true;

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int IntervalSeconds { get; set; } = 60;

        /// <summary>
        /// 执行次数限制（-1表示无限次，即一直执行）
        /// </summary>
        public int MaxExecutionCount { get; set; } = -1;

        /// <summary>
        /// 已执行次数
        /// </summary>
        public int ExecutedCount { get; set; } = 0;

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public string LastExecutionTime { get; set; } = string.Empty;

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public string NextExecutionTime { get; set; } = string.Empty;

        /// <summary>
        /// 请求失败时的重试次数（0 表示不重试，仅尝试一次）
        /// </summary>
        public int RequestRetryCount { get; set; } = 2;

        /// <summary>
        /// 每次重试前的等待时间（毫秒）
        /// </summary>
        public int RequestRetryDelayMs { get; set; } = 500;

        /// <summary>
        /// 请求超时时间（毫秒）。定时检查时使用；0 表示使用各映射自身的超时。
        /// </summary>
        public int RequestTimeoutMs { get; set; } = 5000;

        public TimerConfig()
        {
            Id = Guid.NewGuid();
        }
    }
}
