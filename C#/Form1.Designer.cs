namespace final_project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            textBox1 = new TextBox();
            dgvWhales = new DataGridView();
            lblSOL = new Label();
            lblETH = new Label();
            lblFNG = new Label();
            lblBTC = new Label();
            btnRefresh = new Button();
            tabPage2 = new TabPage();
            dgvNews = new DataGridView();
            btnSearchNews = new Button();
            txtSearch = new TextBox();
            label1 = new Label();
            tabPage3 = new TabPage();
            btnDrawChart = new Button();
            cmbIndicator = new ComboBox();
            cmbTime = new ComboBox();
            cmbCoin = new ComboBox();
            tabPage4 = new TabPage();
            numThreshold1 = new NumericUpDown();
            chkAutoMonitor = new CheckBox();
            numThreshold = new NumericUpDown();
            label3 = new Label();
            lblSpread = new Label();
            lblKraken = new Label();
            lblBinance = new Label();
            btnLoadPairs = new Button();
            cmbArbCoin = new ComboBox();
            label2 = new Label();
            lblTitle = new Label();
            timerMonitor = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWhales).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNews).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(8);
            tabControl1.Name = "tabControl1";
            tabControl1.RightToLeftLayout = true;
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(2057, 1140);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(dgvWhales);
            tabPage1.Controls.Add(lblSOL);
            tabPage1.Controls.Add(lblETH);
            tabPage1.Controls.Add(lblFNG);
            tabPage1.Controls.Add(lblBTC);
            tabPage1.Controls.Add(btnRefresh);
            tabPage1.Location = new Point(10, 55);
            tabPage1.Margin = new Padding(8);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(8);
            tabPage1.Size = new Size(2037, 1075);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "市場總覽";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            textBox1.Location = new Point(838, 471);
            textBox1.Margin = new Padding(8);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(359, 58);
            textBox1.TabIndex = 7;
            textBox1.Text = "鯨魚警報";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dgvWhales
            // 
            dgvWhales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWhales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWhales.Location = new Point(838, 557);
            dgvWhales.Margin = new Padding(8);
            dgvWhales.Name = "dgvWhales";
            dgvWhales.RowHeadersWidth = 102;
            dgvWhales.Size = new Size(1139, 443);
            dgvWhales.TabIndex = 6;
            // 
            // lblSOL
            // 
            lblSOL.AutoSize = true;
            lblSOL.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblSOL.Location = new Point(219, 347);
            lblSOL.Margin = new Padding(8, 0, 8, 0);
            lblSOL.Name = "lblSOL";
            lblSOL.Size = new Size(317, 61);
            lblSOL.TabIndex = 5;
            lblSOL.Text = "SOL: 載入中...";
            // 
            // lblETH
            // 
            lblETH.AutoSize = true;
            lblETH.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblETH.Location = new Point(219, 238);
            lblETH.Margin = new Padding(8, 0, 8, 0);
            lblETH.Name = "lblETH";
            lblETH.Size = new Size(317, 61);
            lblETH.TabIndex = 3;
            lblETH.Text = "ETH: 載入中...";
            // 
            // lblFNG
            // 
            lblFNG.AutoSize = true;
            lblFNG.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblFNG.ForeColor = SystemColors.Highlight;
            lblFNG.Location = new Point(838, 195);
            lblFNG.Margin = new Padding(8, 0, 8, 0);
            lblFNG.Name = "lblFNG";
            lblFNG.Size = new Size(506, 102);
            lblFNG.TabIndex = 2;
            lblFNG.Text = "恐懼指數: ---";
            // 
            // lblBTC
            // 
            lblBTC.AutoSize = true;
            lblBTC.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblBTC.Location = new Point(219, 129);
            lblBTC.Margin = new Padding(8, 0, 8, 0);
            lblBTC.Name = "lblBTC";
            lblBTC.Size = new Size(315, 61);
            lblBTC.TabIndex = 1;
            lblBTC.Text = "BTC: 載入中...";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(219, 659);
            btnRefresh.Margin = new Padding(8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(252, 63);
            btnRefresh.TabIndex = 0;
            btnRefresh.Text = "刷新數據";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvNews);
            tabPage2.Controls.Add(btnSearchNews);
            tabPage2.Controls.Add(txtSearch);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(10, 55);
            tabPage2.Margin = new Padding(8);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(8);
            tabPage2.Size = new Size(2037, 1075);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "新聞輿情分析";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvNews
            // 
            dgvNews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNews.Dock = DockStyle.Bottom;
            dgvNews.Location = new Point(8, 317);
            dgvNews.Margin = new Padding(8);
            dgvNews.Name = "dgvNews";
            dgvNews.RowHeadersWidth = 102;
            dgvNews.Size = new Size(2021, 750);
            dgvNews.TabIndex = 3;
            dgvNews.CellContentClick += dgvNews_CellContentClick;
            // 
            // btnSearchNews
            // 
            btnSearchNews.Location = new Point(1425, 94);
            btnSearchNews.Margin = new Padding(8);
            btnSearchNews.Name = "btnSearchNews";
            btnSearchNews.Size = new Size(265, 86);
            btnSearchNews.TabIndex = 2;
            btnSearchNews.Text = "開始分析";
            btnSearchNews.UseVisualStyleBackColor = true;
            btnSearchNews.Click += btnSearchNews_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(473, 99);
            txtSearch.Margin = new Padding(8);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(526, 46);
            txtSearch.TabIndex = 1;
            txtSearch.Text = "Bitcoin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label1.Location = new Point(144, 91);
            label1.Margin = new Padding(8, 0, 8, 0);
            label1.Name = "label1";
            label1.Size = new Size(306, 67);
            label1.TabIndex = 0;
            label1.Text = "搜尋關鍵字:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnDrawChart);
            tabPage3.Controls.Add(cmbIndicator);
            tabPage3.Controls.Add(cmbTime);
            tabPage3.Controls.Add(cmbCoin);
            tabPage3.Location = new Point(10, 55);
            tabPage3.Margin = new Padding(8);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(8);
            tabPage3.Size = new Size(2037, 1075);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "技術分析室";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDrawChart
            // 
            btnDrawChart.Location = new Point(1458, 58);
            btnDrawChart.Margin = new Padding(8);
            btnDrawChart.Name = "btnDrawChart";
            btnDrawChart.Size = new Size(244, 101);
            btnDrawChart.TabIndex = 3;
            btnDrawChart.Text = "繪製圖表";
            btnDrawChart.UseVisualStyleBackColor = true;
            btnDrawChart.Click += btnDrawChart_Click_1;
            // 
            // cmbIndicator
            // 
            cmbIndicator.FormattingEnabled = true;
            cmbIndicator.Items.AddRange(new object[] { "MA", "Bollinger" });
            cmbIndicator.Location = new Point(869, 71);
            cmbIndicator.Margin = new Padding(8);
            cmbIndicator.Name = "cmbIndicator";
            cmbIndicator.Size = new Size(346, 46);
            cmbIndicator.TabIndex = 2;
            cmbIndicator.Text = "MA";
            // 
            // cmbTime
            // 
            cmbTime.FormattingEnabled = true;
            cmbTime.Items.AddRange(new object[] { "1d", "4h", "1h", "15m" });
            cmbTime.Location = new Point(489, 79);
            cmbTime.Margin = new Padding(8);
            cmbTime.Name = "cmbTime";
            cmbTime.Size = new Size(251, 46);
            cmbTime.TabIndex = 1;
            cmbTime.Text = "1d";
            // 
            // cmbCoin
            // 
            cmbCoin.FormattingEnabled = true;
            cmbCoin.Items.AddRange(new object[] { "BTC/USDT", "ETH/USDT", "SOL/USDT" });
            cmbCoin.Location = new Point(72, 84);
            cmbCoin.Margin = new Padding(8);
            cmbCoin.Name = "cmbCoin";
            cmbCoin.Size = new Size(346, 46);
            cmbCoin.TabIndex = 0;
            cmbCoin.Text = "BTC/USDT";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(numThreshold1);
            tabPage4.Controls.Add(chkAutoMonitor);
            tabPage4.Controls.Add(numThreshold);
            tabPage4.Controls.Add(label3);
            tabPage4.Controls.Add(lblSpread);
            tabPage4.Controls.Add(lblKraken);
            tabPage4.Controls.Add(lblBinance);
            tabPage4.Controls.Add(btnLoadPairs);
            tabPage4.Controls.Add(cmbArbCoin);
            tabPage4.Controls.Add(label2);
            tabPage4.Controls.Add(lblTitle);
            tabPage4.Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            tabPage4.Location = new Point(10, 55);
            tabPage4.Margin = new Padding(8);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(8);
            tabPage4.Size = new Size(2037, 1075);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "搬磚套利";
            tabPage4.UseVisualStyleBackColor = true;
            tabPage4.Click += tabPage4_Click;
            // 
            // numThreshold1
            // 
            numThreshold1.DecimalPlaces = 4;
            numThreshold1.Location = new Point(435, 837);
            numThreshold1.Name = "numThreshold1";
            numThreshold1.Size = new Size(452, 46);
            numThreshold1.TabIndex = 10;
            numThreshold1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // chkAutoMonitor
            // 
            chkAutoMonitor.Appearance = Appearance.Button;
            chkAutoMonitor.AutoSize = true;
            chkAutoMonitor.Location = new Point(1044, 837);
            chkAutoMonitor.Name = "chkAutoMonitor";
            chkAutoMonitor.Size = new Size(285, 48);
            chkAutoMonitor.TabIndex = 9;
            chkAutoMonitor.Text = "開啟自動監控 (5秒)";
            chkAutoMonitor.UseVisualStyleBackColor = true;
            chkAutoMonitor.CheckedChanged += chkAutoMonitor_CheckedChanged;
            // 
            // numThreshold
            // 
            numThreshold.Location = new Point(1113, 2100);
            numThreshold.Margin = new Padding(8);
            numThreshold.Name = "numThreshold";
            numThreshold.Size = new Size(1165, 46);
            numThreshold.TabIndex = 8;
            numThreshold.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(155, 837);
            label3.Name = "label3";
            label3.Size = new Size(239, 38);
            label3.TabIndex = 7;
            label3.Text = "警報門檻 (USD):";
            // 
            // lblSpread
            // 
            lblSpread.AutoSize = true;
            lblSpread.Font = new Font("Microsoft JhengHei UI", 15.9000006F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblSpread.Location = new Point(506, 590);
            lblSpread.Name = "lblSpread";
            lblSpread.Size = new Size(352, 68);
            lblSpread.TabIndex = 6;
            lblSpread.Text = "價差: $0 (0%)";
            // 
            // lblKraken
            // 
            lblKraken.AutoSize = true;
            lblKraken.Font = new Font("Microsoft JhengHei UI", 14.1F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblKraken.ForeColor = Color.BlueViolet;
            lblKraken.Location = new Point(874, 383);
            lblKraken.Margin = new Padding(8, 0, 8, 0);
            lblKraken.Name = "lblKraken";
            lblKraken.Size = new Size(199, 60);
            lblKraken.TabIndex = 5;
            lblKraken.Text = "OKX: $0";
            // 
            // lblBinance
            // 
            lblBinance.AutoSize = true;
            lblBinance.Font = new Font("Microsoft JhengHei UI", 14.1F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblBinance.ForeColor = Color.Blue;
            lblBinance.Location = new Point(211, 383);
            lblBinance.Margin = new Padding(8, 0, 8, 0);
            lblBinance.Name = "lblBinance";
            lblBinance.Size = new Size(276, 60);
            lblBinance.TabIndex = 4;
            lblBinance.Text = "Binance: $0";
            // 
            // btnLoadPairs
            // 
            btnLoadPairs.Location = new Point(918, 175);
            btnLoadPairs.Margin = new Padding(8);
            btnLoadPairs.Name = "btnLoadPairs";
            btnLoadPairs.Size = new Size(345, 58);
            btnLoadPairs.TabIndex = 3;
            btnLoadPairs.Text = "重新掃描幣種";
            btnLoadPairs.UseVisualStyleBackColor = true;
            btnLoadPairs.Click += btnLoadPairs_Click;
            // 
            // cmbArbCoin
            // 
            cmbArbCoin.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArbCoin.FormattingEnabled = true;
            cmbArbCoin.Location = new Point(339, 175);
            cmbArbCoin.Margin = new Padding(8);
            cmbArbCoin.Name = "cmbArbCoin";
            cmbArbCoin.Size = new Size(379, 46);
            cmbArbCoin.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 195);
            label2.Margin = new Padding(8, 0, 8, 0);
            label2.Name = "label2";
            label2.Size = new Size(145, 38);
            label2.TabIndex = 1;
            label2.Text = "選擇幣種:";
            // 
            // lblTitle
            // 
            lblTitle.AccessibleRole = AccessibleRole.None;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lblTitle.Location = new Point(69, 66);
            lblTitle.Margin = new Padding(8, 0, 8, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(883, 67);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "跨交易所價差監控 (Binance vs OKX)";
            lblTitle.Click += lblTitle_Click;
            // 
            // timerMonitor
            // 
            timerMonitor.Interval = 5000;
            timerMonitor.Tick += timerMonitor_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(18F, 38F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2057, 1140);
            Controls.Add(tabControl1);
            Margin = new Padding(8);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWhales).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNews).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThreshold1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label lblFNG;
        private Label lblBTC;
        private Button btnRefresh;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label lblSOL;
        private Label lblETH;
        private DataGridView dgvWhales;
        private TextBox textBox1;
        private DataGridView dgvNews;
        private Button btnSearchNews;
        private TextBox txtSearch;
        private Label label1;
        private Button btnDrawChart;
        private ComboBox cmbIndicator;
        private ComboBox cmbTime;
        private ComboBox cmbCoin;
        private Label lblTitle;
        private Button btnLoadPairs;
        private ComboBox cmbArbCoin;
        private Label label2;
        private Label lblKraken;
        private Label lblBinance;
        private NumericUpDown numThreshold;
        private Label label3;
        private Label lblSpread;
        private CheckBox chkAutoMonitor;
        private NumericUpDown numThreshold1;
        private System.Windows.Forms.Timer timerMonitor;
    }
}
