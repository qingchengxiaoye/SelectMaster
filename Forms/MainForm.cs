using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using SelectMaster.Models;
using SelectMaster.Services;
using System.Threading;

namespace SelectMaster
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ConfigManager _configManager;
        private readonly HttpChecker _httpChecker;
        private readonly BatExecutor _batExecutor;
        private readonly TimerService _timerService;
        private readonly DailyLogService _dailyLog;
        private BindingList<IpBatMapping> _mappings = null!;
        private TimerConfig _timerConfig = new TimerConfig();
        private System.Windows.Forms.Timer? _countdownTimer;
        private DateTime _nextExecutionTime = DateTime.MinValue;

        public MainForm()
        {
            InitializeComponent();
            ApplyTheme();
            _configManager = new ConfigManager();
            _httpChecker = new HttpChecker();
            _batExecutor = new BatExecutor();
            _timerService = new TimerService(_httpChecker, _batExecutor);
            _dailyLog = new DailyLogService();

            tableLayoutPanelTimer.SetColumnSpan(lblCurrentIP, 8);  // 占满第一行剩余空间，有空间就不省略
            tableLayoutPanelTimer.SetColumnSpan(panelTimerRow, 10);  // 第二行整行统一间距
            tableLayoutPanelTimer.SetColumnSpan(panelRetryRow, 10);  // 请求重试整行单行显示

            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;

            // 初始化倒计时计时器
            _countdownTimer = new System.Windows.Forms.Timer();
            _countdownTimer.Interval = 1000; // 每秒更新一次
            _countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadMappings();
            LoadTimerConfig();
            SetupSingleLineLabelTooltips();
        }

        /// <summary>单行标签悬停/点击时显示完整内容（展开“...”部分）</summary>
        private void SetupSingleLineLabelTooltips()
        {
            if (toolTipMain == null) return;
            toolTipMain.AutoPopDelay = 15000;
            toolTipMain.InitialDelay = 300;
            void UpdateTooltip(object? s, EventArgs _)
            {
                if (s is Control c && !string.IsNullOrEmpty(c.Text))
                    toolTipMain.SetToolTip(c, c.Text);
            }
            void ShowFullTextOnClick(object? s, EventArgs _)
            {
                if (s is Control c && !string.IsNullOrEmpty(c.Text))
                    toolTipMain.Show(c.Text, c, 2, c.Height + 2, 12000);
            }
            foreach (var lbl in new Control[] { lblCountdown, lblTotalExecutions, lblCurrentIP })
            {
                lbl.MouseEnter += UpdateTooltip;
                lbl.Click += ShowFullTextOnClick;
            }
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _timerService?.Dispose();
            _countdownTimer?.Stop();
            _countdownTimer?.Dispose();
            _dailyLog?.Dispose();
            SaveMappings();
            SaveTimerConfig();
        }

        /// <summary>应用酷炫深色科技风主题</summary>
        private void ApplyTheme()
        {
            // 窗体与主背景
            BackColor = AppTheme.Background;
            ForeColor = AppTheme.TextPrimary;

            // 标题栏
            panelHeader.BackColor = AppTheme.Header;
            label1.ForeColor = AppTheme.TextPrimary;
            label1.Font = new Font("Microsoft YaHei UI", 12f, FontStyle.Bold);

            // 分组与面板
            groupBoxMapping.BackColor = AppTheme.Surface;
            groupBoxMapping.ForeColor = AppTheme.TextPrimary;
            groupBoxTimer.BackColor = AppTheme.Surface;
            groupBoxTimer.ForeColor = AppTheme.TextPrimary;
            groupBoxLog.BackColor = AppTheme.Surface;
            groupBoxLog.ForeColor = AppTheme.TextPrimary;
            panelButtons.BackColor = AppTheme.Surface;
            tableLayoutPanelButtons.BackColor = AppTheme.Surface;
            tableLayoutPanelTimer.BackColor = AppTheme.Surface;
            tableLayoutPanelLog.BackColor = AppTheme.Surface;
            panelTimerRow.BackColor = AppTheme.Surface;
            panelTimerButtons.BackColor = AppTheme.Surface;
            panelRetryRow.BackColor = AppTheme.Surface;

            // 标签
            lblLog.ForeColor = AppTheme.TextPrimary;
            lblCountdown.ForeColor = AppTheme.AccentCyan;
            lblTotalExecutions.ForeColor = AppTheme.TextSecondary;
            lblCurrentIP.ForeColor = AppTheme.TextSecondary;
            lblInterval.ForeColor = AppTheme.TextPrimary;
            lblMinutes.ForeColor = AppTheme.TextSecondary;
            lblMaxCount.ForeColor = AppTheme.TextPrimary;
            lblTimes.ForeColor = AppTheme.TextSecondary;
            lblRetry.ForeColor = AppTheme.TextPrimary;
            lblRetryTimes.ForeColor = AppTheme.TextSecondary;
            lblRetryDelay.ForeColor = AppTheme.TextPrimary;
            lblRetryMs.ForeColor = AppTheme.TextSecondary;
            lblTimeout.ForeColor = AppTheme.TextPrimary;

            // 输入框
            Color inputBack = AppTheme.InputBack;
            Color inputFore = AppTheme.TextPrimary;
            txtInterval.BackColor = inputBack;
            txtInterval.ForeColor = inputFore;
            txtInterval.BorderStyle = BorderStyle.FixedSingle;
            txtMaxCount.BackColor = inputBack;
            txtMaxCount.ForeColor = inputFore;
            txtMaxCount.BorderStyle = BorderStyle.FixedSingle;
            txtRetryCount.BackColor = inputBack;
            txtRetryCount.ForeColor = inputFore;
            txtRetryCount.BorderStyle = BorderStyle.FixedSingle;
            txtRetryDelayMs.BackColor = inputBack;
            txtRetryDelayMs.ForeColor = inputFore;
            txtRetryDelayMs.BorderStyle = BorderStyle.FixedSingle;
            txtTimeout.BackColor = inputBack;
            txtTimeout.ForeColor = inputFore;
            txtTimeout.BorderStyle = BorderStyle.FixedSingle;

            // 复选框
            chkTimerEnabled.ForeColor = AppTheme.TextPrimary;
            chkInfiniteExecution.ForeColor = AppTheme.TextPrimary;

            // 日志区
            txtLog.BackColor = AppTheme.InputBack;
            txtLog.ForeColor = inputFore;
            txtLog.BorderStyle = BorderStyle.None;

            // 状态栏
            lblStatus.BackColor = AppTheme.Header;
            lblStatus.ForeColor = AppTheme.TextPrimary;

            // 按钮：扁平风格 + 强调色
            void StyleButton(Button btn, Color back, Color fore)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = AppTheme.InputBorder;
                btn.FlatAppearance.BorderSize = 1;
                btn.BackColor = back;
                btn.ForeColor = fore;
            }
            StyleButton(btnExecute, AppTheme.AccentCyan, Color.Black);
            StyleButton(btnTest, AppTheme.SurfaceLight, AppTheme.TextPrimary);
            StyleButton(btnImport, AppTheme.SurfaceLight, AppTheme.TextPrimary);
            StyleButton(btnExport, AppTheme.SurfaceLight, AppTheme.TextPrimary);
            StyleButton(btnAdd, AppTheme.AccentBlue, AppTheme.TextPrimary);
            StyleButton(btnDelete, AppTheme.SurfaceLight, AppTheme.TextPrimary);
            StyleButton(btnSave, AppTheme.AccentGreen, Color.Black);
            StyleButton(btnSelectBat, AppTheme.SurfaceLight, AppTheme.TextPrimary);
            StyleButton(btnStartTimer, AppTheme.AccentGreen, Color.Black);
            StyleButton(btnStopTimer, AppTheme.AccentRed, AppTheme.TextPrimary);

            // 统一圆角半径
            const int cornerRadius = 8;
            foreach (var btn in new[] { btnExecute, btnTest, btnImport, btnExport, btnAdd, btnDelete, btnSave, btnSelectBat, btnStartTimer, btnStopTimer })
            {
                if (btn is RoundedButton rb)
                    rb.CornerRadius = cornerRadius;
            }

            // DataGridView 深色主题
            dataGridView.BackgroundColor = AppTheme.Surface;
            dataGridView.GridColor = AppTheme.GridLine;
            dataGridView.BorderStyle = BorderStyle.FixedSingle;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.Header;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = AppTheme.TextPrimary;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = AppTheme.Header;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = AppTheme.TextPrimary;
            dataGridView.DefaultCellStyle.BackColor = AppTheme.SurfaceLight;
            dataGridView.DefaultCellStyle.ForeColor = AppTheme.TextPrimary;
            dataGridView.DefaultCellStyle.SelectionBackColor = AppTheme.AccentCyan;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.GridAltRow;
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = AppTheme.TextPrimary;
            dataGridView.RowHeadersVisible = false;
        }

        /// <summary>
        /// 追加日志到界面并写入当日日志文件。包含“不可访问”时以红色高亮显示。
        /// </summary>
        private void AppendLog(string message, bool highlightAsError = false)
        {
            if (string.IsNullOrEmpty(message))
                return;
            string line = message.TrimEnd();
            if (line.Length == 0)
                return;
            _dailyLog.WriteLine(line);
            // 总结句“没有找到需要执行的映射（…不可访问…）”为正常情况，不高亮
            bool isError = highlightAsError
                || (line.Contains("不可访问", StringComparison.Ordinal) && !line.Contains("没有找到需要执行的映射", StringComparison.Ordinal));
            void DoAppend()
            {
                txtLog.SelectionStart = txtLog.TextLength;
                txtLog.SelectionLength = 0;
                txtLog.SelectionColor = isError ? AppTheme.AccentRed : txtLog.ForeColor;
                txtLog.AppendText(line + Environment.NewLine);
                txtLog.SelectionColor = txtLog.ForeColor;
                txtLog.ScrollToCaret();
            }
            if (txtLog.InvokeRequired)
                txtLog.Invoke(new Action(DoAppend));
            else
                DoAppend();
        }

        private void InitializeDataGridView()
        {
            // 设置列
            dataGridView.Columns.Clear();
            dataGridView.AutoGenerateColumns = false;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Enabled",
                HeaderText = "启用",
                DataPropertyName = "Enabled",
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dataGridView.Columns.Add(checkColumn);

            var priorityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Priority",
                HeaderText = "优先级",
                DataPropertyName = "Priority",
                Width = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dataGridView.Columns.Add(priorityColumn);

            var urlColumn = new DataGridViewTextBoxColumn
            {
                Name = "Url",
                HeaderText = "HTTP地址",
                DataPropertyName = "Url",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            dataGridView.Columns.Add(urlColumn);

            var batColumn = new DataGridViewTextBoxColumn
            {
                Name = "BatFilePath",
                HeaderText = "Bat文件路径",
                DataPropertyName = "BatFilePath",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            dataGridView.Columns.Add(batColumn);

            // 允许编辑
            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
            dataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadMappings()
        {
            try
            {
                var mappings = _configManager.LoadConfig();
                _mappings = new BindingList<IpBatMapping>(mappings);
                dataGridView.DataSource = _mappings;

                UpdateStatus($"已加载 {mappings.Count} 条映射关系", AppTheme.AccentGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _mappings = new BindingList<IpBatMapping>();
                dataGridView.DataSource = _mappings;
            }
        }

        private void SaveMappings()
        {
            try
            {
                var list = new List<IpBatMapping>();
                foreach (var mapping in _mappings)
                {
                    if (!string.IsNullOrWhiteSpace(mapping.Url) && !string.IsNullOrWhiteSpace(mapping.BatFilePath))
                    {
                        list.Add(mapping);
                    }
                }
                _configManager.SaveConfig(list);
                UpdateStatus($"已保存 {list.Count} 条映射关系", AppTheme.AccentGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            btnExecute.Enabled = false;
            txtLog.Clear();
            UpdateStatus("正在检查地址...", AppTheme.AccentCyan);

            try
            {
                var mappings = new List<IpBatMapping>(_mappings);
                AppendLog($"开始检查 {mappings.Count} 个映射地址...");
                AppendLog(new string('-', 50));

                // 按优先级排序并过滤已启用的
                var sortedMappings = mappings
                    .Where(m => m.Enabled)
                    .OrderBy(m => m.Priority)
                    .ToList();

                if (!sortedMappings.Any())
                {
                    AppendLog("没有启用的映射关系！");
                    UpdateStatus("没有启用的映射关系", AppTheme.AccentRed);
                    btnExecute.Enabled = true;
                    return;
                }

                AppendLog($"启用的映射数量: {sortedMappings.Count}");
                AppendLog("");

                bool found = false;
                foreach (var mapping in sortedMappings)
                {
                    AppendLog($"[优先级 {mapping.Priority}] 正在检查: {mapping.Url}");
                    int timeout = _timerConfig.RequestTimeoutMs > 0 ? _timerConfig.RequestTimeoutMs : mapping.Timeout;
                    var result = await _httpChecker.CheckUrlAsync(
                        mapping.Url,
                        timeout,
                        _timerConfig.RequestRetryCount,
                        _timerConfig.RequestRetryDelayMs);

                    if (result.IsAccessible)
                    {
                        AppendLog($"  ✓ 可访问! 响应时间: {result.ResponseTime}ms");
                        AppendLog($"  {result.Message}");
                        AppendLog(new string('=', 50));
                        AppendLog($"将执行Bat文件: {mapping.BatFilePath}");
                        UpdateStatus($"执行中: {Path.GetFileName(mapping.BatFilePath)}", AppTheme.AccentCyan);

                        // 使用弹窗执行bat文件
                        var execResult = await _batExecutor.ExecuteBatWithWindowAsync(mapping.BatFilePath);

                        AppendLog("");
                        AppendLog("=== 执行结果 ===");
                        AppendLog($"成功: {execResult.Success}, 退出码: {execResult.ExitCode}");

                        if (execResult.Success)
                        {
                            UpdateStatus($"执行成功: {Path.GetFileName(mapping.BatFilePath)} (退出码: {execResult.ExitCode})", AppTheme.AccentGreen);
                        }
                        else
                        {
                            UpdateStatus($"执行失败: {Path.GetFileName(mapping.BatFilePath)} (退出码: {execResult.ExitCode})", AppTheme.AccentRed);
                        }

                        found = true;
                        break;
                    }
                    else
                    {
                        AppendLog($"  ✗ 不可访问");
                        AppendLog($"  {result.Message}");
                        AppendLog("");
                    }
                }

                if (!found)
                {
                    AppendLog(new string('=', 50));
                    AppendLog("所有地址均不可访问！");
                    UpdateStatus("所有地址均不可访问", AppTheme.AccentRed);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"发生异常: {ex.Message}");
                AppendLog($"堆栈跟踪: {ex.StackTrace}");
                UpdateStatus($"执行出错: {ex.Message}", AppTheme.AccentRed);
            }
            finally
            {
                btnExecute.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMappings();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 临时取消绑定，避免状态冲突
                var oldDataSource = dataGridView.DataSource;
                dataGridView.DataSource = null;

                var newMapping = new IpBatMapping
                {
                    Priority = _mappings.Count + 1,
                    Enabled = true,
                    Timeout = 5000
                };
                _mappings.Add(newMapping);

                // 重新绑定数据源
                dataGridView.DataSource = _mappings;
                dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
                UpdateStatus("已添加新映射", AppTheme.AccentGreen);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"btnAdd_Click 错误: {ex.Message}");
                MessageBox.Show($"添加失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"btnDelete_Click 被调用");
                System.Diagnostics.Debug.WriteLine($"SelectedRows.Count: {dataGridView.SelectedRows.Count}");
                System.Diagnostics.Debug.WriteLine($"CurrentRow: {dataGridView.CurrentRow != null}");
                if (dataGridView.CurrentRow != null)
                {
                    System.Diagnostics.Debug.WriteLine($"CurrentRow.Index: {dataGridView.CurrentRow.Index}, IsNewRow: {dataGridView.CurrentRow.IsNewRow}");
                }
                System.Diagnostics.Debug.WriteLine($"_mappings.Count: {_mappings.Count}");

                // 获取所有要删除的索引
                var indicesToDelete = new List<int>();

                // 检查 SelectedRows
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    System.Diagnostics.Debug.WriteLine($"SelectedRow - Index: {row.Index}, IsNewRow: {row.IsNewRow}");
                    if (!row.IsNewRow && row.Index >= 0 && row.Index < _mappings.Count)
                    {
                        indicesToDelete.Add(row.Index);
                    }
                }

                // 如果 SelectedRows 为空，检查 CurrentRow
                if (indicesToDelete.Count == 0 && dataGridView.CurrentRow != null)
                {
                    System.Diagnostics.Debug.WriteLine($"使用 CurrentRow 删除");
                    var row = dataGridView.CurrentRow;
                    if (!row.IsNewRow && row.Index >= 0 && row.Index < _mappings.Count)
                    {
                        indicesToDelete.Add(row.Index);
                    }
                }

                System.Diagnostics.Debug.WriteLine($"待删除索引数量: {indicesToDelete.Count}");
                foreach (var idx in indicesToDelete)
                {
                    System.Diagnostics.Debug.WriteLine($"  索引: {idx}");
                }

                if (indicesToDelete.Count == 0)
                {
                    MessageBox.Show("请先选择要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 从后往前删除，避免索引变化
                indicesToDelete.Sort((a, b) => b.CompareTo(a));
                foreach (int index in indicesToDelete)
                {
                    System.Diagnostics.Debug.WriteLine($"删除索引: {index}, 当前_mappings.Count: {_mappings.Count}");
                    if (index >= 0 && index < _mappings.Count)
                    {
                        _mappings.RemoveAt(index);
                        System.Diagnostics.Debug.WriteLine($"  删除成功，剩余: {_mappings.Count}");
                    }
                }

                UpdateStatus($"已删除 {indicesToDelete.Count} 条映射", AppTheme.AccentGreen);
                System.Diagnostics.Debug.WriteLine($"删除完成，最终_mappings.Count: {_mappings.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"btnDelete_Click 错误: {ex.Message}\n堆栈: {ex.StackTrace}");
                MessageBox.Show($"删除失败: {ex.Message}\n\n{ex.StackTrace}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectBat_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Bat文件 (*.bat)|*.bat|所有文件 (*.*)|*.*";
                dialog.Title = "选择Bat文件";

                if (dialog.ShowDialog() == DialogResult.OK && dataGridView.CurrentRow != null)
                {
                    dataGridView.CurrentRow.Cells["BatFilePath"].Value = dialog.FileName;
                }
            }
        }

        private void UpdateStatus(string message, Color color)
        {
            try
            {
                if (lblStatus.IsDisposed)
                    return;

                lblStatus.Text = message;
                lblStatus.ForeColor = color;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateStatus 错误: {ex.Message}");
            }
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.IsNewRow)
            {
                MessageBox.Show("请先选择一行进行测试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var url = dataGridView.CurrentRow.Cells["Url"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("当前行的HTTP地址为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int timeout = _timerConfig.RequestTimeoutMs > 0 ? _timerConfig.RequestTimeoutMs : 5000;
            if (int.TryParse(txtTimeout.Text, out int t) && t > 0)
                timeout = t;

            btnTest.Enabled = false;
            txtLog.Clear();
            AppendLog($"测试地址: {url}");
            AppendLog($"超时设置: {timeout}ms");
            AppendLog(new string('-', 50));
            UpdateStatus("正在测试地址...", AppTheme.AccentCyan);

            try
            {
                var result = await _httpChecker.CheckUrlAsync(
                    url,
                    timeout,
                    _timerConfig.RequestRetryCount,
                    _timerConfig.RequestRetryDelayMs);

                AppendLog($"结果: {(result.IsAccessible ? "✓ 可访问" : "✗ 不可访问")}");
                AppendLog($"消息: {result.Message}");
                if (result.ResponseTime > 0)
                {
                    AppendLog($"响应时间: {result.ResponseTime}ms");
                }

                if (result.IsAccessible)
                {
                    UpdateStatus("地址测试成功", AppTheme.AccentGreen);
                }
                else
                {
                    UpdateStatus("地址测试失败", AppTheme.AccentRed);
                }
            }
            catch (Exception ex)
            {
                AppendLog($"发生异常: {ex.Message}");
                UpdateStatus($"测试出错: {ex.Message}", AppTheme.AccentRed);
            }
            finally
            {
                btnTest.Enabled = true;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "JSON配置文件 (*.json)|*.json|所有文件 (*.*)|*.*";
                    dialog.Title = "导出配置";
                    dialog.FileName = $"SelectMaster_Config_{DateTime.Now:yyyyMMdd_HHmmss}.json";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // 创建完整配置对象
                        var completeConfig = new CompleteConfig
                        {
                            Mappings = _mappings.Where(m =>
                                !string.IsNullOrWhiteSpace(m.Url) &&
                                !string.IsNullOrWhiteSpace(m.BatFilePath)).ToList(),
                            TimerConfig = _timerConfig
                        };

                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };
                        var json = JsonSerializer.Serialize(completeConfig, options);
                        File.WriteAllText(dialog.FileName, json);

                        MessageBox.Show($"配置已成功导出到:\n{dialog.FileName}\n\n包含:\n- {completeConfig.Mappings.Count} 条映射配置\n- 定时器配置",
                            "导出成功",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateStatus($"配置已导出: {Path.GetFileName(dialog.FileName)}", AppTheme.AccentGreen);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出配置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"导出失败: {ex.Message}", AppTheme.AccentRed);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "JSON配置文件 (*.json)|*.json|所有文件 (*.*)|*.*";
                    dialog.Title = "导入配置";
                    dialog.CheckFileExists = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var json = File.ReadAllText(dialog.FileName);

                        // 尝试导入完整配置（包含定时器）
                        try
                        {
                            var completeConfig = JsonSerializer.Deserialize<CompleteConfig>(json);
                            if (completeConfig != null && completeConfig.Mappings != null && completeConfig.Mappings.Count > 0)
                            {
                                var result = MessageBox.Show(
                                    $"检测到 {completeConfig.Mappings.Count} 条映射配置。\n\n" +
                                    $"定时器配置: {(completeConfig.TimerConfig.Enabled ? "启用" : "禁用")}\n\n" +
                                    "点击\"是\"导入并覆盖当前配置\n" +
                                    "点击\"否\"导入并追加到当前配置",
                                    "导入配置",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);

                                if (result == DialogResult.Cancel)
                                {
                                    return;
                                }

                                if (result == DialogResult.Yes)
                                {
                                    // 覆盖模式
                                    _mappings.Clear();
                                    foreach (var mapping in completeConfig.Mappings)
                                    {
                                        _mappings.Add(mapping);
                                    }

                                // 导入定时器配置
                                _timerConfig = completeConfig.TimerConfig;
                                chkTimerEnabled.Checked = _timerConfig.Enabled;
                                chkInfiniteExecution.Checked = _timerConfig.InfiniteExecution;
                                txtInterval.Text = _timerConfig.IntervalSeconds.ToString();
                                txtMaxCount.Text = _timerConfig.MaxExecutionCount.ToString();
                                txtMaxCount.Enabled = !_timerConfig.InfiniteExecution && _timerConfig.Enabled;
                                txtRetryCount.Text = _timerConfig.RequestRetryCount.ToString();
                                txtRetryDelayMs.Text = _timerConfig.RequestRetryDelayMs.ToString();
                                txtTimeout.Text = _timerConfig.RequestTimeoutMs.ToString();
                                UpdateTimerStatus();
                                }
                                else if (result == DialogResult.No)
                                {
                                    // 追加模式
                                    var maxPriority = _mappings.Count > 0 ? _mappings.Max(m => m.Priority) : 0;
                                    foreach (var mapping in completeConfig.Mappings)
                                    {
                                        mapping.Id = Guid.NewGuid();
                                        mapping.Priority = ++maxPriority;
                                        _mappings.Add(mapping);
                                    }

                                    // 询问是否导入定时器配置
                                    var timerResult = MessageBox.Show(
                                        "是否也导入定时器配置？",
                                        "导入定时器配置",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                                    if (timerResult == DialogResult.Yes)
                                    {
                                        _timerConfig = completeConfig.TimerConfig;
                                        chkTimerEnabled.Checked = _timerConfig.Enabled;
                                        chkInfiniteExecution.Checked = _timerConfig.InfiniteExecution;
                                        txtInterval.Text = _timerConfig.IntervalSeconds.ToString();
                                        txtMaxCount.Text = _timerConfig.MaxExecutionCount.ToString();
                                        txtMaxCount.Enabled = !_timerConfig.InfiniteExecution && _timerConfig.Enabled;
                                        txtRetryCount.Text = _timerConfig.RequestRetryCount.ToString();
                                        txtRetryDelayMs.Text = _timerConfig.RequestRetryDelayMs.ToString();
                                        txtTimeout.Text = _timerConfig.RequestTimeoutMs.ToString();
                                        UpdateTimerStatus();
                                    }
                                }

                                SaveMappings();
                                SaveTimerConfig();
                                MessageBox.Show($"成功导入 {completeConfig.Mappings.Count} 条映射配置！", "导入成功",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                UpdateStatus($"配置已导入: {Path.GetFileName(dialog.FileName)}", AppTheme.AccentGreen);
                                return;
                            }
                        }
                        catch
                        {
                            // 如果导入完整配置失败，尝试导入旧的映射配置格式
                        }

                        // 尝试导入旧的映射配置格式
                        var mappings = JsonSerializer.Deserialize<List<IpBatMapping>>(json);

                        if (mappings != null && mappings.Count > 0)
                        {
                            var result = MessageBox.Show(
                                $"检测到 {mappings.Count} 条配置记录。\n\n点击\"是\"导入并覆盖当前配置\n点击\"否\"导入并追加到当前配置",
                                "导入配置",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question);

                            if (result == DialogResult.Cancel)
                            {
                                return;
                            }

                            if (result == DialogResult.Yes)
                            {
                                // 覆盖模式
                                _mappings.Clear();
                                foreach (var mapping in mappings)
                                {
                                    _mappings.Add(mapping);
                                }
                            }
                            else if (result == DialogResult.No)
                            {
                                // 追加模式
                                var maxPriority = _mappings.Count > 0 ? _mappings.Max(m => m.Priority) : 0;
                                foreach (var mapping in mappings)
                                {
                                    mapping.Id = Guid.NewGuid();
                                    mapping.Priority = ++maxPriority;
                                    _mappings.Add(mapping);
                                }
                            }

                            SaveMappings();
                            MessageBox.Show($"成功导入 {mappings.Count} 条配置！", "导入成功",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateStatus($"配置已导入: {Path.GetFileName(dialog.FileName)}", AppTheme.AccentGreen);
                        }
                        else
                        {
                            MessageBox.Show("配置文件中没有有效的数据！", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导入配置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus($"导入失败: {ex.Message}", AppTheme.AccentRed);
            }
        }

        private void chkTimerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _timerConfig.Enabled = chkTimerEnabled.Checked;
            txtInterval.Enabled = chkTimerEnabled.Checked;
            txtTimeout.Enabled = chkTimerEnabled.Checked;
            chkInfiniteExecution.Enabled = chkTimerEnabled.Checked;
            txtMaxCount.Enabled = chkTimerEnabled.Checked && !_timerConfig.InfiniteExecution;
        }

        private void chkInfiniteExecution_CheckedChanged(object sender, EventArgs e)
        {
            _timerConfig.InfiniteExecution = chkInfiniteExecution.Checked;
            txtMaxCount.Enabled = !_timerConfig.InfiniteExecution && chkTimerEnabled.Checked;
            if (_timerConfig.InfiniteExecution)
            {
                txtMaxCount.Text = "-1";
            }
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtInterval.Text, out int interval) || interval < 1)
            {
                MessageBox.Show("执行间隔必须是大于0的整数！", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int maxCount = -1; // 默认为一直执行
            if (!_timerConfig.InfiniteExecution)
            {
                if (!int.TryParse(txtMaxCount.Text, out maxCount) || maxCount < 1)
                {
                    MessageBox.Show("最多执行次数必须是大于0的整数！", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _timerConfig.Enabled = chkTimerEnabled.Checked;
            _timerConfig.IntervalSeconds = interval;
            _timerConfig.MaxExecutionCount = maxCount;
            if (int.TryParse(txtRetryCount.Text, out int retryCount) && retryCount >= 0)
                _timerConfig.RequestRetryCount = retryCount;
            if (int.TryParse(txtRetryDelayMs.Text, out int retryDelayMs) && retryDelayMs >= 0)
                _timerConfig.RequestRetryDelayMs = retryDelayMs;
            if (int.TryParse(txtTimeout.Text, out int timeoutMs) && timeoutMs >= 0)
                _timerConfig.RequestTimeoutMs = timeoutMs;

            _timerService.Start(_timerConfig, _mappings.ToList(), (message) =>
            {
                // “没有找到需要执行的映射”为正常情况，不高亮；仅对具体某条“不可访问”高亮
                bool highlight = message.Contains("不可访问", StringComparison.Ordinal)
                    && !message.Contains("没有找到需要执行的映射", StringComparison.Ordinal);
                AppendLog(message, highlight);
            },
            OnCurrentMappingChanged);

            // 启动倒计时计时器
            if (DateTime.TryParse(_timerConfig.NextExecutionTime, out DateTime nextTime))
            {
                _nextExecutionTime = nextTime;
                _countdownTimer?.Start();
            }

            // 运行中：按钮改为橙色 + “运行中 时:分:秒”，与停止状态区分明显
            btnStartTimer.Text = "运行中 " + DateTime.Now.ToString("HH:mm:ss");
            btnStartTimer.BackColor = AppTheme.AccentOrange;
            btnStartTimer.ForeColor = Color.White;
            btnStopTimer.BackColor = AppTheme.AccentRed;
            btnStopTimer.ForeColor = Color.White;

            UpdateTimerStatus();
            UpdateStatus("定时执行已启动", AppTheme.AccentGreen);
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            _timerService.Stop();
            _countdownTimer?.Stop();
            lblCountdown.Text = "下次执行: --";
            lblTotalExecutions.Text = "总执行次数: 0";
            lblCurrentIP.Text = "当前执行: 无";
            // 已停止：恢复绿色“启动定时”、停止按钮恢复为浅红/灰
            btnStartTimer.Text = "启动定时";
            btnStartTimer.BackColor = AppTheme.AccentGreen;
            btnStartTimer.ForeColor = Color.Black;
            btnStopTimer.BackColor = AppTheme.AccentRed;
            btnStopTimer.ForeColor = AppTheme.TextPrimary;
            UpdateTimerStatus();
            UpdateStatus("定时执行已停止", AppTheme.AccentOrange);
        }

        /// <summary>
        /// 当前执行的映射变化回调
        /// </summary>
        private void OnCurrentMappingChanged(IpBatMapping? mapping)
        {
            try
            {
                if (lblCurrentIP.InvokeRequired)
                {
                    lblCurrentIP.Invoke(new Action(() =>
                    {
                        if (mapping != null)
                        {
                            lblCurrentIP.Text = $"当前执行: {mapping.Url} (优先级{mapping.Priority})";
                            lblCurrentIP.ForeColor = AppTheme.AccentGreen;
                        }
                        else
                        {
                            lblCurrentIP.Text = "当前执行: 无";
                            lblCurrentIP.ForeColor = AppTheme.TextMuted;
                        }
                    }));
                }
                else
                {
                    if (mapping != null)
                    {
                        lblCurrentIP.Text = $"当前执行: {mapping.Url} (优先级{mapping.Priority})";
                        lblCurrentIP.ForeColor = AppTheme.AccentGreen;
                    }
                    else
                    {
                        lblCurrentIP.Text = "当前执行: 无";
                        lblCurrentIP.ForeColor = AppTheme.TextMuted;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnCurrentMappingChanged 错误: {ex.Message}");
            }
        }

        private void UpdateTimerStatus()
        {
            var (isRunning, config) = _timerService.GetStatus();
            if (isRunning)
            {
                if (config.MaxExecutionCount < 0)
                {
                    lblTimerStatus.Text = "状态: 运行中";
                }
                else
                {
                    lblTimerStatus.Text = $"状态: 运行中 ({config.ExecutedCount}/{config.MaxExecutionCount}次)";
                }
                lblTimerStatus.ForeColor = AppTheme.AccentGreen;
                // 保持运行中样式（若由启动点击已设置则不再覆盖文字）
                btnStartTimer.BackColor = AppTheme.AccentOrange;
                btnStartTimer.ForeColor = Color.White;
                if (!btnStartTimer.Text.StartsWith("运行中", StringComparison.Ordinal))
                    btnStartTimer.Text = "运行中 " + DateTime.Now.ToString("HH:mm:ss");
                btnStopTimer.BackColor = AppTheme.AccentRed;
                btnStopTimer.ForeColor = Color.White;
            }
            else
            {
                lblTimerStatus.Text = "状态: 未启动";
                lblTimerStatus.ForeColor = AppTheme.AccentRed;
                btnStartTimer.Text = "启动定时";
                btnStartTimer.BackColor = AppTheme.AccentGreen;
                btnStartTimer.ForeColor = Color.Black;
                btnStopTimer.BackColor = AppTheme.AccentRed;
                btnStopTimer.ForeColor = AppTheme.TextPrimary;
            }
        }

        private void SaveTimerConfig()
        {
            try
            {
                if (int.TryParse(txtRetryCount?.Text, out int retryCount) && retryCount >= 0)
                    _timerConfig.RequestRetryCount = retryCount;
                if (int.TryParse(txtRetryDelayMs?.Text, out int retryDelayMs) && retryDelayMs >= 0)
                    _timerConfig.RequestRetryDelayMs = retryDelayMs;
                if (int.TryParse(txtTimeout?.Text, out int timeoutMs) && timeoutMs >= 0)
                    _timerConfig.RequestTimeoutMs = timeoutMs;

                var timerConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "timer_config.json");
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                var json = JsonSerializer.Serialize(_timerConfig, options);
                File.WriteAllText(timerConfigPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存定时器配置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTimerConfig()
        {
            try
            {
                var timerConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "timer_config.json");
                if (File.Exists(timerConfigPath))
                {
                    var json = File.ReadAllText(timerConfigPath);
                    var config = JsonSerializer.Deserialize<TimerConfig>(json);
                    if (config != null)
                    {
                        _timerConfig = config;
                        chkTimerEnabled.Checked = _timerConfig.Enabled;
                        chkInfiniteExecution.Checked = _timerConfig.InfiniteExecution;
                        txtInterval.Text = _timerConfig.IntervalSeconds.ToString();
                        txtMaxCount.Text = _timerConfig.MaxExecutionCount.ToString();
                        txtMaxCount.Enabled = !_timerConfig.InfiniteExecution && _timerConfig.Enabled;
                        txtRetryCount.Text = _timerConfig.RequestRetryCount.ToString();
                        txtRetryDelayMs.Text = _timerConfig.RequestRetryDelayMs.ToString();
                        txtTimeout.Text = _timerConfig.RequestTimeoutMs.ToString();
                        UpdateTimerStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载定时器配置失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CountdownTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                var (isRunning, config) = _timerService.GetStatus();

                if (!isRunning)
                {
                    lblCountdown.Text = "下次执行: --";
                    return;
                }

                // 更新倒计时
                if (DateTime.TryParse(config.NextExecutionTime, out DateTime nextTime))
                {
                    var remaining = nextTime - DateTime.Now;

                    if (remaining.TotalSeconds <= 0)
                    {
                        lblCountdown.Text = "下次执行: 即将执行...";
                    }
                    else
                    {
                        var hours = remaining.Hours;
                        var minutes = remaining.Minutes;
                        var seconds = remaining.Seconds;

                        if (hours > 0)
                        {
                            lblCountdown.Text = $"下次执行: {hours}小时{minutes}分{seconds}秒";
                        }
                        else if (minutes > 0)
                        {
                            lblCountdown.Text = $"下次执行: {minutes}分{seconds}秒";
                        }
                        else
                        {
                            lblCountdown.Text = $"下次执行: {seconds}秒";
                        }
                    }

                    // 更新总执行次数
                    if (config.MaxExecutionCount < 0)
                    {
                        lblTotalExecutions.Text = $"总执行次数: {config.ExecutedCount} (一直执行)";
                    }
                    else
                    {
                        lblTotalExecutions.Text = $"总执行次数: {config.ExecutedCount}/{config.MaxExecutionCount}";
                    }
                }
            }
            catch (Exception ex)
            {
                // 忽略定时器更新时的错误，避免影响程序运行
                System.Diagnostics.Debug.WriteLine($"CountdownTimer_Tick 错误: {ex.Message}");
            }
        }
    }
}
