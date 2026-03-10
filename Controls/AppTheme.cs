namespace SelectMaster
{
    /// <summary>浅色主题色</summary>
    public static class AppTheme
    {
        // 背景
        public static System.Drawing.Color Background => System.Drawing.Color.FromArgb(248, 249, 250);   // #f8f9fa
        public static System.Drawing.Color Surface => System.Drawing.Color.FromArgb(255, 255, 255);     // #ffffff
        public static System.Drawing.Color SurfaceLight => System.Drawing.Color.FromArgb(241, 243, 245); // #f1f3f5
        public static System.Drawing.Color Header => System.Drawing.Color.FromArgb(233, 236, 239);      // #e9ecef 浅灰标题栏

        // 文字
        public static System.Drawing.Color TextPrimary => System.Drawing.Color.FromArgb(33, 37, 41);     // #212529
        public static System.Drawing.Color TextSecondary => System.Drawing.Color.FromArgb(73, 80, 87);  // #495057
        public static System.Drawing.Color TextMuted => System.Drawing.Color.FromArgb(108, 117, 125);   // #6c757d

        // 强调色（浅色背景下深色字更清晰）
        public static System.Drawing.Color AccentCyan => System.Drawing.Color.FromArgb(0, 180, 216);   // 青色
        public static System.Drawing.Color AccentGreen => System.Drawing.Color.FromArgb(40, 167, 69);   // 绿色
        public static System.Drawing.Color AccentRed => System.Drawing.Color.FromArgb(220, 53, 69);     // 红色
        public static System.Drawing.Color AccentOrange => System.Drawing.Color.FromArgb(253, 126, 20); // 橙色
        public static System.Drawing.Color AccentBlue => System.Drawing.Color.FromArgb(0, 123, 255);   // 蓝色

        // 控件
        public static System.Drawing.Color InputBack => System.Drawing.Color.FromArgb(255, 255, 255);   // 输入框背景
        public static System.Drawing.Color InputBorder => System.Drawing.Color.FromArgb(206, 212, 218);  // 边框
        public static System.Drawing.Color GridLine => System.Drawing.Color.FromArgb(222, 226, 230);     // 表格线
        public static System.Drawing.Color GridAltRow => System.Drawing.Color.FromArgb(248, 249, 250);  // 表格交替行
    }
}
