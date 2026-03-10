namespace SelectMaster
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSelectBat = new SelectMaster.RoundedButton();
            this.btnSave = new SelectMaster.RoundedButton();
            this.btnExport = new SelectMaster.RoundedButton();
            this.btnImport = new SelectMaster.RoundedButton();
            this.btnExecute = new SelectMaster.RoundedButton();
            this.btnTest = new SelectMaster.RoundedButton();
            this.btnDelete = new SelectMaster.RoundedButton();
            this.btnAdd = new SelectMaster.RoundedButton();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.chkInfiniteExecution = new System.Windows.Forms.CheckBox();
            this.lblCountdown = new SelectMaster.SingleLineLabel();
            this.lblTotalExecutions = new SelectMaster.SingleLineLabel();
            this.lblCurrentIP = new SelectMaster.SingleLineLabel();
            this.tableLayoutPanelTimer = new System.Windows.Forms.TableLayoutPanel();
            this.lblInterval = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblMaxCount = new System.Windows.Forms.Label();
            this.txtMaxCount = new System.Windows.Forms.TextBox();
            this.lblTimes = new System.Windows.Forms.Label();
            this.btnStartTimer = new SelectMaster.RoundedButton();
            this.btnStopTimer = new SelectMaster.RoundedButton();
            this.lblTimerStatus = new System.Windows.Forms.Label();
            this.lblRetry = new System.Windows.Forms.Label();
            this.txtRetryCount = new System.Windows.Forms.TextBox();
            this.lblRetryTimes = new System.Windows.Forms.Label();
            this.lblRetryDelay = new System.Windows.Forms.Label();
            this.txtRetryDelayMs = new System.Windows.Forms.TextBox();
            this.lblRetryMs = new System.Windows.Forms.Label();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.panelRetryRow = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTimerRow = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTimerButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxMapping = new System.Windows.Forms.GroupBox();
            this.groupBoxTimer = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelLog = new System.Windows.Forms.TableLayoutPanel();
            this.panelLogContainer = new System.Windows.Forms.Panel();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.groupBoxMapping.SuspendLayout();
            this.groupBoxTimer.SuspendLayout();
            this.panelRetryRow.SuspendLayout();
            this.panelTimerRow.SuspendLayout();
            this.panelTimerButtons.SuspendLayout();
            this.tableLayoutPanelLog.SuspendLayout();
            this.panelLogContainer.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.tableLayoutPanelTimer.SuspendLayout();
            this.SuspendLayout();

            // panelHeader - 标题栏
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(808, 40);
            this.panelHeader.TabIndex = 0;

            // label1 - 标题
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址与Bat文件映射管理";

            // groupBoxMapping - 映射列表与操作（先加底部按钮栏，再加表格 Fill）
            this.groupBoxMapping.Controls.Add(this.panelButtons);
            this.groupBoxMapping.Controls.Add(this.dataGridView);
            this.groupBoxMapping.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMapping.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxMapping.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMapping.Name = "groupBoxMapping";
            this.groupBoxMapping.Padding = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this.groupBoxMapping.Size = new System.Drawing.Size(808, 268);
            this.groupBoxMapping.TabIndex = 1;
            this.groupBoxMapping.TabStop = false;
            this.groupBoxMapping.Text = "映射列表与操作";

            // dataGridView
            this.dataGridView.AllowUserToResizeColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(8, 22);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.Size = new System.Drawing.Size(792, 188);
            this.dataGridView.TabIndex = 0;

            // panelButtons
            this.panelButtons.Controls.Add(this.tableLayoutPanelButtons);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(8, 220);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(792, 48);
            this.panelButtons.TabIndex = 1;

            // tableLayoutPanelButtons - 8 列等宽，按钮大小统一
            this.tableLayoutPanelButtons.ColumnCount = 8;
            for (int i = 0; i < 8; i++)
                this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnExecute, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnTest, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnImport, 2, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnExport, 3, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnAdd, 4, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnDelete, 5, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnSave, 6, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnSelectBat, 7, 0);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(792, 48);
            this.tableLayoutPanelButtons.TabIndex = 0;
            
            // btnDelete
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnDelete.Location = new System.Drawing.Point(466, 3);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 39);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            
            // btnAdd
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnAdd.Location = new System.Drawing.Point(369, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 39);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            
            // btnSelectBat
            this.btnSelectBat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectBat.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnSelectBat.Location = new System.Drawing.Point(721, 3);
            this.btnSelectBat.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectBat.Name = "btnSelectBat";
            this.btnSelectBat.Size = new System.Drawing.Size(53, 39);
            this.btnSelectBat.TabIndex = 7;
            this.btnSelectBat.Text = "选择Bat";
            this.btnSelectBat.UseVisualStyleBackColor = true;
            this.btnSelectBat.Click += new System.EventHandler(this.btnSelectBat_Click);
            
            // btnSave
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnSave.Location = new System.Drawing.Point(663, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 39);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnExport
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnExport.Location = new System.Drawing.Point(272, 3);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(93, 39);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出配置";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // btnImport
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnImport.Location = new System.Drawing.Point(175, 3);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(93, 39);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "导入配置";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            
            // btnExecute
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExecute.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExecute.Location = new System.Drawing.Point(3, 3);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(2);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(168, 39);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "执行检查";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            
            // btnTest
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTest.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnTest.Location = new System.Drawing.Point(177, 3);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(168, 39);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "测试当前地址";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            
            // groupBoxTimer - 定时任务（含请求重试一行，设置最小高度防止被日志区遮挡）
            this.groupBoxTimer.Controls.Add(this.tableLayoutPanelTimer);
            this.groupBoxTimer.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTimer.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxTimer.Location = new System.Drawing.Point(0, 268);
            this.groupBoxTimer.MinimumSize = new System.Drawing.Size(0, 122);
            this.groupBoxTimer.Name = "groupBoxTimer";
            this.groupBoxTimer.Padding = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this.groupBoxTimer.Size = new System.Drawing.Size(808, 122);
            this.groupBoxTimer.TabIndex = 2;
            this.groupBoxTimer.TabStop = false;
            this.groupBoxTimer.Text = "定时任务";

            // tableLayoutPanelTimer - 超时放在请求重试行（第3行），第二行无超时
            this.tableLayoutPanelTimer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            this.tableLayoutPanelTimer.ColumnCount = 10;
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));   // 下次执行
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));  // 总执行次数（含「1000000 (一直执行)」完整显示）
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanelTimer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTimer.Controls.Add(this.lblCountdown, 0, 0);
            this.tableLayoutPanelTimer.Controls.Add(this.lblTotalExecutions, 1, 0);
            this.tableLayoutPanelTimer.Controls.Add(this.lblCurrentIP, 2, 0);
            this.tableLayoutPanelTimer.Controls.Add(this.panelTimerRow, 0, 1);
            this.tableLayoutPanelTimer.Controls.Add(this.panelRetryRow, 0, 2);
            this.tableLayoutPanelTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTimer.Location = new System.Drawing.Point(8, 22);
            this.tableLayoutPanelTimer.Name = "tableLayoutPanelTimer";
            this.tableLayoutPanelTimer.RowCount = 3;
            this.tableLayoutPanelTimer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelTimer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelTimer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelTimer.Size = new System.Drawing.Size(792, 82);
            this.tableLayoutPanelTimer.TabIndex = 0;

            // lblCountdown - 单行省略号，不换行
            this.lblCountdown.AutoSize = false;
            this.lblCountdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountdown.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCountdown.ForeColor = System.Drawing.Color.Blue;
            this.lblCountdown.Location = new System.Drawing.Point(3, 3);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(114, 24);
            this.lblCountdown.TabIndex = 11;
            this.lblCountdown.Text = "下次执行: --";
            this.lblCountdown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCountdown.AutoEllipsis = true;
            this.lblCountdown.AutoSize = false;

            // lblTotalExecutions - 填满列宽，168px 内不换行、不省略（1000000 以内）
            this.lblTotalExecutions.AutoSize = false;
            this.lblTotalExecutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalExecutions.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblTotalExecutions.Location = new System.Drawing.Point(133, 3);
            this.lblTotalExecutions.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalExecutions.Name = "lblTotalExecutions";
            this.lblTotalExecutions.Size = new System.Drawing.Size(300, 24);
            this.lblTotalExecutions.TabIndex = 12;
            this.lblTotalExecutions.Text = "总执行次数: 0";
            this.lblTotalExecutions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalExecutions.AutoEllipsis = true;

            // lblCurrentIP - 长 URL 单行显示，超出用省略号
            this.lblCurrentIP.AutoSize = false;
            this.lblCurrentIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentIP.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblCurrentIP.Location = new System.Drawing.Point(203, 3);
            this.lblCurrentIP.Name = "lblCurrentIP";
            this.lblCurrentIP.Size = new System.Drawing.Size(150, 24);
            this.lblCurrentIP.TabIndex = 13;
            this.lblCurrentIP.Text = "当前执行: 无";
            this.lblCurrentIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCurrentIP.AutoEllipsis = true;

            // panelTimerRow - 第二行整行：启用、执行间隔、最多、一直执行、启动/停止，统一间距
            this.panelTimerRow.AutoSize = true;
            this.panelTimerRow.Controls.Add(this.chkTimerEnabled);
            this.panelTimerRow.Controls.Add(this.lblInterval);
            this.panelTimerRow.Controls.Add(this.txtInterval);
            this.panelTimerRow.Controls.Add(this.lblMinutes);
            this.panelTimerRow.Controls.Add(this.lblMaxCount);
            this.panelTimerRow.Controls.Add(this.txtMaxCount);
            this.panelTimerRow.Controls.Add(this.lblTimes);
            this.panelTimerRow.Controls.Add(this.chkInfiniteExecution);
            this.panelTimerRow.Controls.Add(this.panelTimerButtons);
            this.panelTimerRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTimerRow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelTimerRow.Location = new System.Drawing.Point(3, 33);
            this.panelTimerRow.Margin = new System.Windows.Forms.Padding(0);
            this.panelTimerRow.Name = "panelTimerRow";
            this.panelTimerRow.Padding = new System.Windows.Forms.Padding(0);
            this.panelTimerRow.Size = new System.Drawing.Size(786, 26);
            this.panelTimerRow.TabIndex = 23;
            this.panelTimerRow.WrapContents = false;

            // chkTimerEnabled
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.chkTimerEnabled.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(54, 21);
            this.chkTimerEnabled.TabIndex = 0;
            this.chkTimerEnabled.Text = "启用";
            this.chkTimerEnabled.UseVisualStyleBackColor = true;
            this.chkTimerEnabled.CheckedChanged += new System.EventHandler(this.chkTimerEnabled_CheckedChanged);

            // lblInterval
            this.lblInterval.AutoSize = true;
            this.lblInterval.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblInterval.Margin = new System.Windows.Forms.Padding(0, 4, 6, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(68, 17);
            this.lblInterval.TabIndex = 1;
            this.lblInterval.Text = "执行间隔:";

            // txtInterval
            this.txtInterval.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtInterval.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(34, 23);
            this.txtInterval.TabIndex = 2;
            this.txtInterval.Text = "60";
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // lblMinutes
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblMinutes.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(14, 17);
            this.lblMinutes.TabIndex = 3;
            this.lblMinutes.Text = "秒";

            // lblMaxCount
            this.lblMaxCount.AutoSize = true;
            this.lblMaxCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblMaxCount.Margin = new System.Windows.Forms.Padding(0, 4, 6, 0);
            this.lblMaxCount.Name = "lblMaxCount";
            this.lblMaxCount.Size = new System.Drawing.Size(44, 17);
            this.lblMaxCount.TabIndex = 4;
            this.lblMaxCount.Text = "最多:";

            // txtMaxCount
            this.txtMaxCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtMaxCount.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtMaxCount.Name = "txtMaxCount";
            this.txtMaxCount.Size = new System.Drawing.Size(28, 23);
            this.txtMaxCount.TabIndex = 5;
            this.txtMaxCount.Text = "-1";
            this.txtMaxCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // lblTimes
            this.lblTimes.AutoSize = true;
            this.lblTimes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblTimes.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(14, 17);
            this.lblTimes.TabIndex = 6;
            this.lblTimes.Text = "次";

            // chkInfiniteExecution
            this.chkInfiniteExecution.AutoSize = true;
            this.chkInfiniteExecution.Checked = true;
            this.chkInfiniteExecution.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.chkInfiniteExecution.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.chkInfiniteExecution.Name = "chkInfiniteExecution";
            this.chkInfiniteExecution.Size = new System.Drawing.Size(76, 21);
            this.chkInfiniteExecution.TabIndex = 10;
            this.chkInfiniteExecution.Text = "一直执行";
            this.chkInfiniteExecution.UseVisualStyleBackColor = true;
            this.chkInfiniteExecution.CheckedChanged += new System.EventHandler(this.chkInfiniteExecution_CheckedChanged);

            // panelTimerButtons - 启动/停止紧挨，间隔 6px
            this.panelTimerButtons.AutoSize = true;
            this.panelTimerButtons.Controls.Add(this.btnStartTimer);
            this.panelTimerButtons.Controls.Add(this.btnStopTimer);
            this.panelTimerButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelTimerButtons.Location = new System.Drawing.Point(0, 0);
            this.panelTimerButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelTimerButtons.Name = "panelTimerButtons";
            this.panelTimerButtons.Padding = new System.Windows.Forms.Padding(0);
            this.panelTimerButtons.Size = new System.Drawing.Size(162, 32);
            this.panelTimerButtons.TabIndex = 24;
            this.panelTimerButtons.WrapContents = false;

            // btnStartTimer
            this.btnStartTimer.BackColor = System.Drawing.Color.LightGreen;
            this.btnStartTimer.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnStartTimer.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnStartTimer.MinimumSize = new System.Drawing.Size(76, 32);
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.Size = new System.Drawing.Size(76, 32);
            this.btnStartTimer.TabIndex = 7;
            this.btnStartTimer.Text = "启动定时";
            this.btnStartTimer.UseVisualStyleBackColor = false;
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);

            // btnStopTimer
            this.btnStopTimer.BackColor = System.Drawing.Color.LightCoral;
            this.btnStopTimer.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnStopTimer.Margin = new System.Windows.Forms.Padding(0);
            this.btnStopTimer.MinimumSize = new System.Drawing.Size(76, 32);
            this.btnStopTimer.Name = "btnStopTimer";
            this.btnStopTimer.Size = new System.Drawing.Size(76, 32);
            this.btnStopTimer.TabIndex = 8;
            this.btnStopTimer.Text = "停止定时";
            this.btnStopTimer.UseVisualStyleBackColor = false;
            this.btnStopTimer.Click += new System.EventHandler(this.btnStopTimer_Click);

            // panelRetryRow - 整行：请求重试 + 次 + 间隔 + ms + 超时(ms)，单行、统一间距
            this.panelRetryRow.AutoSize = true;
            this.panelRetryRow.Controls.Add(this.lblRetry);
            this.panelRetryRow.Controls.Add(this.txtRetryCount);
            this.panelRetryRow.Controls.Add(this.lblRetryTimes);
            this.panelRetryRow.Controls.Add(this.lblRetryDelay);
            this.panelRetryRow.Controls.Add(this.txtRetryDelayMs);
            this.panelRetryRow.Controls.Add(this.lblRetryMs);
            this.panelRetryRow.Controls.Add(this.lblTimeout);
            this.panelRetryRow.Controls.Add(this.txtTimeout);
            this.panelRetryRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRetryRow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelRetryRow.Location = new System.Drawing.Point(3, 61);
            this.panelRetryRow.Margin = new System.Windows.Forms.Padding(0);
            this.panelRetryRow.Name = "panelRetryRow";
            this.panelRetryRow.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panelRetryRow.Size = new System.Drawing.Size(786, 26);
            this.panelRetryRow.TabIndex = 22;
            this.panelRetryRow.WrapContents = false;

            // lblRetry
            this.lblRetry.AutoSize = true;
            this.lblRetry.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblRetry.Margin = new System.Windows.Forms.Padding(0, 4, 6, 0);
            this.lblRetry.Name = "lblRetry";
            this.lblRetry.Size = new System.Drawing.Size(68, 17);
            this.lblRetry.TabIndex = 14;
            this.lblRetry.Text = "请求重试:";

            // txtRetryCount
            this.txtRetryCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtRetryCount.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtRetryCount.Name = "txtRetryCount";
            this.txtRetryCount.Size = new System.Drawing.Size(28, 23);
            this.txtRetryCount.TabIndex = 15;
            this.txtRetryCount.Text = "2";
            this.txtRetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // lblRetryTimes
            this.lblRetryTimes.AutoSize = true;
            this.lblRetryTimes.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblRetryTimes.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblRetryTimes.Name = "lblRetryTimes";
            this.lblRetryTimes.Size = new System.Drawing.Size(14, 17);
            this.lblRetryTimes.TabIndex = 16;
            this.lblRetryTimes.Text = "次";

            // lblRetryDelay
            this.lblRetryDelay.AutoSize = true;
            this.lblRetryDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblRetryDelay.Margin = new System.Windows.Forms.Padding(0, 4, 6, 0);
            this.lblRetryDelay.Name = "lblRetryDelay";
            this.lblRetryDelay.Size = new System.Drawing.Size(32, 17);
            this.lblRetryDelay.TabIndex = 17;
            this.lblRetryDelay.Text = "间隔";

            // txtRetryDelayMs
            this.txtRetryDelayMs.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtRetryDelayMs.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtRetryDelayMs.Name = "txtRetryDelayMs";
            this.txtRetryDelayMs.Size = new System.Drawing.Size(38, 23);
            this.txtRetryDelayMs.TabIndex = 18;
            this.txtRetryDelayMs.Text = "500";
            this.txtRetryDelayMs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // lblRetryMs
            this.lblRetryMs.AutoSize = true;
            this.lblRetryMs.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblRetryMs.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.lblRetryMs.Name = "lblRetryMs";
            this.lblRetryMs.Size = new System.Drawing.Size(20, 17);
            this.lblRetryMs.TabIndex = 19;
            this.lblRetryMs.Text = "ms";

            // lblTimeout
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblTimeout.Margin = new System.Windows.Forms.Padding(0, 4, 6, 0);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(54, 17);
            this.lblTimeout.TabIndex = 20;
            this.lblTimeout.Text = "超时(ms)";

            // txtTimeout
            this.txtTimeout.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtTimeout.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(44, 23);
            this.txtTimeout.TabIndex = 21;
            this.txtTimeout.Text = "5000";
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            
            // panelLogContainer - 包一层 Panel，底部留出空间给滚动条下箭头，避免被裁切
            this.panelLogContainer.Controls.Add(this.groupBoxLog);
            this.panelLogContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogContainer.Location = new System.Drawing.Point(0, 386);
            this.panelLogContainer.Name = "panelLogContainer";
            this.panelLogContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelLogContainer.Size = new System.Drawing.Size(808, 188);

            // groupBoxLog - 执行日志（Dock Fill 占剩余空间，最小高度保证日志完整显示）
            this.groupBoxLog.Controls.Add(this.tableLayoutPanelLog);
            this.groupBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxLog.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLog.MinimumSize = new System.Drawing.Size(0, 240);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Padding = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this.groupBoxLog.Size = new System.Drawing.Size(808, 168);
            this.groupBoxLog.TabIndex = 3;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "执行日志";

            // tableLayoutPanelLog - 两行布局
            this.tableLayoutPanelLog.ColumnCount = 1;
            this.tableLayoutPanelLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLog.Controls.Add(this.lblLog, 0, 0);
            this.tableLayoutPanelLog.Controls.Add(this.txtLog, 0, 1);
            this.tableLayoutPanelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLog.Location = new System.Drawing.Point(8, 22);
            this.tableLayoutPanelLog.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLog.Name = "tableLayoutPanelLog";
            this.tableLayoutPanelLog.Padding = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLog.RowCount = 2;
            this.tableLayoutPanelLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLog.Size = new System.Drawing.Size(792, 164);
            this.tableLayoutPanelLog.TabIndex = 0;

            // lblLog - 第一行固定高度，与下方文本框有间距
            this.lblLog.AutoSize = true;
            this.lblLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLog.Margin = new System.Windows.Forms.Padding(0, 2, 0, 8);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(68, 17);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "运行日志";

            // txtLog - 第二行 Fill，与上方留出间距
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(792, 136);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";

            // lblStatus - 状态栏
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 4, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(808, 24);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "就绪";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 620);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelLogContainer);
            this.Controls.Add(this.groupBoxTimer);
            this.Controls.Add(this.groupBoxMapping);
            this.Controls.Add(this.panelHeader);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectMaster - IP地址与Bat文件管理";
            this.tableLayoutPanelTimer.ResumeLayout(false);
            this.tableLayoutPanelTimer.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.groupBoxMapping.ResumeLayout(false);
            this.groupBoxTimer.ResumeLayout(false);
            this.panelTimerRow.ResumeLayout(false);
            this.panelTimerRow.PerformLayout();
            this.panelTimerButtons.ResumeLayout(false);
            this.panelRetryRow.ResumeLayout(false);
            this.panelRetryRow.PerformLayout();
            this.tableLayoutPanelLog.ResumeLayout(false);
            this.tableLayoutPanelLog.PerformLayout();
            this.panelLogContainer.ResumeLayout(false);
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxMapping;
        private System.Windows.Forms.GroupBox groupBoxTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLog;
        private System.Windows.Forms.Panel panelLogContainer;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panelButtons;
        private SelectMaster.RoundedButton btnDelete;
        private SelectMaster.RoundedButton btnAdd;
        private SelectMaster.RoundedButton btnSelectBat;
        private SelectMaster.RoundedButton btnSave;
        private SelectMaster.RoundedButton btnExport;
        private SelectMaster.RoundedButton btnImport;
        private SelectMaster.RoundedButton btnExecute;
        private SelectMaster.RoundedButton btnTest;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.CheckBox chkInfiniteExecution;
        private SelectMaster.SingleLineLabel lblCountdown;
        private SelectMaster.SingleLineLabel lblTotalExecutions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTimer;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Label lblMaxCount;
        private System.Windows.Forms.TextBox txtMaxCount;
        private System.Windows.Forms.Label lblTimes;
        private SelectMaster.RoundedButton btnStartTimer;
        private SelectMaster.RoundedButton btnStopTimer;
        private System.Windows.Forms.Label lblTimerStatus;
        private SelectMaster.SingleLineLabel lblCurrentIP;
        private System.Windows.Forms.Label lblRetry;
        private System.Windows.Forms.TextBox txtRetryCount;
        private System.Windows.Forms.Label lblRetryTimes;
        private System.Windows.Forms.Label lblRetryDelay;
        private System.Windows.Forms.TextBox txtRetryDelayMs;
        private System.Windows.Forms.Label lblRetryMs;
        private System.Windows.Forms.FlowLayoutPanel panelTimerRow;
        private System.Windows.Forms.FlowLayoutPanel panelTimerButtons;
        private System.Windows.Forms.FlowLayoutPanel panelRetryRow;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
    }
}
