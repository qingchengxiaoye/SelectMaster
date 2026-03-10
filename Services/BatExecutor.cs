using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SelectMaster.Models;

namespace SelectMaster.Services
{
    /// <summary>
    /// Bat文件执行服务
    /// </summary>
    public class BatExecutor
    {
        /// <summary>
        /// 执行Bat文件（无弹窗，用于后台执行）
        /// </summary>
        public (bool Success, string Output, string Error) ExecuteBat(string batFilePath)
        {
            try
            {
                // 检查文件是否存在
                if (!File.Exists(batFilePath))
                {
                    return (false, "", $"Bat文件不存在: {batFilePath}");
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = batFilePath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(batFilePath),
                    StandardOutputEncoding = System.Text.Encoding.GetEncoding("GBK"),
                    StandardErrorEncoding = System.Text.Encoding.GetEncoding("GBK")
                };

                using var process = new Process { StartInfo = startInfo };
                process.Start();

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    return (true, output, error);
                }
                else
                {
                    return (false, output, $"Bat文件执行失败，退出码: {process.ExitCode}，错误信息: {error}");
                }
            }
            catch (Exception ex)
            {
                return (false, "", $"执行Bat文件时发生异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 弹窗执行Bat文件（用于用户交互）
        /// </summary>
        public (bool Success, int ExitCode) ExecuteBatWithWindow(string batFilePath)
        {
            try
            {
                // 检查文件是否存在
                if (!File.Exists(batFilePath))
                {
                    MessageBox.Show($"Bat文件不存在: {batFilePath}", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false, -1);
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = batFilePath,
                    UseShellExecute = true,  // 使用系统shell执行，会弹窗
                    WorkingDirectory = Path.GetDirectoryName(batFilePath)
                };

                using var process = new Process { StartInfo = startInfo };
                process.Start();
                process.WaitForExit();

                return (true, process.ExitCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行Bat文件时发生异常: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false, -1);
            }
        }

        /// <summary>
        /// 异步执行Bat文件（无弹窗）
        /// </summary>
        public async Task<(bool Success, string Output, string Error)> ExecuteBatAsync(string batFilePath)
        {
            return await Task.Run(() => ExecuteBat(batFilePath));
        }

        /// <summary>
        /// 异步弹窗执行Bat文件
        /// </summary>
        public async Task<(bool Success, int ExitCode)> ExecuteBatWithWindowAsync(string batFilePath)
        {
            return await Task.Run(() => ExecuteBatWithWindow(batFilePath));
        }
    }
}
