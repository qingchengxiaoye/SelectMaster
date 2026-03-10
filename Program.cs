using System;
using System.Windows.Forms;

namespace SelectMaster
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 添加全局异常处理
            Application.ThreadException += (sender, e) =>
            {
                MessageBox.Show($"未处理的异常:\n{e.Exception.Message}\n\n堆栈跟踪:\n{e.Exception.StackTrace}",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"未处理的异常:\n{ex.Message}\n\n堆栈跟踪:\n{ex.StackTrace}",
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Application.Run(new MainForm());
        }
    }
}
