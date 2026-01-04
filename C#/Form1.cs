using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
// ★ 重要：必須引用圖表套件
using System.Windows.Forms.DataVisualization.Charting;

namespace final_project
{
    public partial class Form1 : Form
    {
        // 設定 Python Server 網址
        private const string API_URL = "http://127.0.0.1:5000/api";

        // ★ Lab 3: 宣告圖表變數
        private Chart chartKLine;

        public Form1()
        {
            InitializeComponent();

            // 初始化下拉選單內容 (防呆)
            InitializeCustomUI();

            // ★ Lab 3: 動態生成圖表
            InitChartDynamically();
        }

        // ==================== 資料模型 (Models) ====================
        public class PriceData
        {
            public string Coin { get; set; }
            public double Price { get; set; }
            [JsonProperty("Change24h%")]
            public double Change { get; set; }
        }

        public class FngData
        {
            public int value { get; set; }
            public string state { get; set; }
        }

        public class WhaleData
        {
            public string time { get; set; }
            public string symbol { get; set; }
            public double amount { get; set; }
            public double value_usd { get; set; }
        }

        public class NewsItem
        {
            public string sentiment { get; set; }
            public string title { get; set; }
            public string date { get; set; }
            public string link { get; set; }
        }

        public class DashboardResponse
        {
            public List<PriceData> prices { get; set; }
            public FngData fng { get; set; }
            public List<WhaleData> whales { get; set; }
        }

        // ★ Lab 4: 新增搬磚套利資料模型
        public class ArbitrageData
        {
            public string exchange_a { get; set; }
            public double price_a { get; set; }
            public string exchange_b { get; set; }
            public double price_b { get; set; }
            public double spread { get; set; }
            public double spread_pct { get; set; }
            public string status { get; set; }
        }

        // ==================== 初始化邏輯 ====================

        private void InitializeCustomUI()
        {
            // Lab 3 UI
            if (cmbCoin.Items.Count == 0)
            {
                cmbCoin.Items.AddRange(new string[] { "BTC/USDT", "ETH/USDT", "SOL/USDT" });
                cmbCoin.SelectedIndex = 0;
            }
            if (cmbTime.Items.Count == 0)
            {
                cmbTime.Items.AddRange(new string[] { "1d", "4h", "1h", "15m" });
                cmbTime.SelectedIndex = 0;
            }
            if (cmbIndicator.Items.Count == 0)
            {
                cmbIndicator.Items.AddRange(new string[] { "MA", "Bollinger" });
                cmbIndicator.SelectedIndex = 0;
            }

            // Lab 4 UI: 設定 ComboBox 為唯讀 (只能選不能打字)
            cmbArbCoin.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitChartDynamically()
        {
            if (chartKLine != null) return;

            chartKLine = new Chart();
            chartKLine.Parent = tabPage3;
            chartKLine.Dock = DockStyle.Bottom;
            chartKLine.Height = 850;
            chartKLine.Name = "chartKLine";

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.IsStartedFromZero = false;
            chartKLine.ChartAreas.Add(area);

            Legend legend = new Legend("MainLegend");
            chartKLine.Legends.Add(legend);

            Series priceSeries = new Series("Price");
            priceSeries.ChartType = SeriesChartType.Candlestick;
            priceSeries.ChartArea = "MainArea";
            priceSeries["PriceUpColor"] = "Green";
            priceSeries["PriceDownColor"] = "Red";
            priceSeries.Color = Color.Black;
            chartKLine.Series.Add(priceSeries);
        }

        // ==================== Lab 1: 市場儀表板邏輯 ====================
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    btnRefresh.Text = "更新中...";
                    btnRefresh.Enabled = false;

                    string json = await client.GetStringAsync($"{API_URL}/dashboard");
                    var data = JsonConvert.DeserializeObject<DashboardResponse>(json);

                    if (data.prices != null && data.prices.Count >= 3)
                    {
                        UpdatePriceLabel(lblBTC, data.prices[0]);
                        UpdatePriceLabel(lblETH, data.prices[1]);
                        UpdatePriceLabel(lblSOL, data.prices[2]);
                    }

                    if (data.fng != null)
                    {
                        lblFNG.Text = $"恐懼指數: {data.fng.value} ({data.fng.state})";
                        if (data.fng.state.Contains("Greed")) lblFNG.ForeColor = Color.Green;
                        else if (data.fng.state.Contains("Fear")) lblFNG.ForeColor = Color.Red;
                        else lblFNG.ForeColor = Color.Black;
                    }

                    if (data.whales != null && data.whales.Count > 0)
                    {
                        dgvWhales.DataSource = data.whales;
                    }
                    else
                    {
                        var emptyList = new List<WhaleData> {
                            new WhaleData { time = DateTime.Now.ToString("HH:mm"), symbol = "暫無異動", amount = 0, value_usd = 0 }
                        };
                        dgvWhales.DataSource = emptyList;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"連線錯誤: {ex.Message}");
                }
                finally
                {
                    btnRefresh.Text = "刷新數據";
                    btnRefresh.Enabled = true;
                }
            }
        }

        private void UpdatePriceLabel(Label lbl, PriceData coin)
        {
            lbl.Text = $"{coin.Coin}: ${coin.Price:N2} ({coin.Change:N2}%)";
            lbl.ForeColor = coin.Change >= 0 ? Color.Green : Color.Red;
        }

        // ==================== Lab 2: 新聞分析邏輯 ====================
        private async void btnSearchNews_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("請輸入關鍵字！");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    btnSearchNews.Text = "分析中...";
                    btnSearchNews.Enabled = false;

                    string url = $"{API_URL}/news?keyword={keyword}";
                    string json = await client.GetStringAsync(url);
                    var newsList = JsonConvert.DeserializeObject<List<NewsItem>>(json);

                    dgvNews.DataSource = newsList;
                    if (newsList.Count == 0) MessageBox.Show("找不到相關新聞。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"搜尋失敗: {ex.Message}");
                }
                finally
                {
                    btnSearchNews.Text = "開始分析";
                    btnSearchNews.Enabled = true;
                }
            }
        }

        private void dgvNews_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var news = dgvNews.Rows[e.RowIndex].DataBoundItem as NewsItem;
                if (news != null && !string.IsNullOrEmpty(news.link))
                {
                    try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = news.link, UseShellExecute = true }); }
                    catch { MessageBox.Show("無法開啟連結"); }
                }
            }
        }

        // ==================== Lab 3: 技術分析繪圖邏輯 ====================
        private async void btnDrawChart_Click_1(object sender, EventArgs e)
        {
            if (chartKLine == null) InitChartDynamically();

            string symbol = cmbCoin.Text;
            string interval = cmbTime.Text;
            string indicator = cmbIndicator.Text;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    btnDrawChart.Text = "繪製中...";
                    btnDrawChart.Enabled = false;

                    string url = $"{API_URL}/kline?symbol={symbol}&interval={interval}&indicator={indicator}";
                    string json = await client.GetStringAsync(url);
                    dynamic data = JsonConvert.DeserializeObject(json);

                    if (data == null || data.times == null)
                    {
                        MessageBox.Show("抓取失敗或無數據");
                        return;
                    }

                    chartKLine.Series["Price"].Points.Clear();
                    for (int i = chartKLine.Series.Count - 1; i >= 0; i--)
                    {
                        if (chartKLine.Series[i].Name != "Price")
                            chartKLine.Series.RemoveAt(i);
                    }

                    var seriesPrice = chartKLine.Series["Price"];
                    int count = data.times.Count;
                    for (int i = 0; i < count; i++)
                    {
                        try
                        {
                            seriesPrice.Points.AddXY(
                                (string)data.times[i],
                                (double)data.high[i],
                                (double)data.low[i],
                                (double)data.open[i],
                                (double)data.close[i]
                            );
                        }
                        catch { continue; }
                    }

                    dynamic ind = data.indicators;
                    if (ind != null)
                    {
                        if (ind.type == "MA")
                        {
                            AddLineSeries("MA7", Color.Orange, ind.line1, data.times);
                            AddLineSeries("MA25", Color.Blue, ind.line2, data.times);
                        }
                        else if (ind.type == "Bollinger")
                        {
                            AddLineSeries("Upper", Color.Purple, ind.line1, data.times);
                            AddLineSeries("Lower", Color.Purple, ind.line2, data.times);
                        }
                    }

                    // 強制重繪
                    chartKLine.Invalidate();
                    chartKLine.Update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"繪圖錯誤: {ex.Message}");
                }
                finally
                {
                    btnDrawChart.Text = "繪製圖表";
                    btnDrawChart.Enabled = true;
                }
            }
        }

        private void AddLineSeries(string name, Color color, dynamic values, dynamic times)
        {
            Series line = new Series(name);
            line.ChartType = SeriesChartType.Line;
            line.ChartArea = "MainArea";
            line.Color = color;
            line.BorderWidth = 2;

            for (int i = 0; i < values.Count; i++)
            {
                line.Points.AddXY((string)times[i], (double)values[i]);
            }
            chartKLine.Series.Add(line);
        }

        // ==================== Lab 4: 搬磚套利 (Binance vs OKX) ====================

        // 1. 載入幣種按鈕
        private async void btnLoadPairs_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    btnLoadPairs.Text = "掃描中...";
                    btnLoadPairs.Enabled = false;

                    string json = await client.GetStringAsync($"{API_URL}/pairs");
                    List<string> pairs = JsonConvert.DeserializeObject<List<string>>(json);

                    cmbArbCoin.Items.Clear();
                    if (pairs != null && pairs.Count > 0)
                    {
                        cmbArbCoin.Items.AddRange(pairs.ToArray());
                        cmbArbCoin.SelectedIndex = 0;
                        MessageBox.Show($"掃描完成！找到 {pairs.Count} 個共同幣種。");
                    }
                    else
                    {
                        MessageBox.Show("找不到共同幣種，或後端連線失敗。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"掃描錯誤: {ex.Message}");
                }
                finally
                {
                    btnLoadPairs.Text = "重新掃描幣種";
                    btnLoadPairs.Enabled = true;
                }
            }
        }

        // 2. 開啟/關閉 自動監控
        private void chkAutoMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoMonitor.Checked)
            {
                if (string.IsNullOrEmpty(cmbArbCoin.Text))
                {
                    MessageBox.Show("請先掃描並選擇一個幣種！");
                    chkAutoMonitor.Checked = false;
                    return;
                }
                // 啟動計時器
                timerMonitor.Start();
                FetchArbitrageData(); // 立即執行一次
            }
            else
            {
                // 停止計時器
                timerMonitor.Stop();
                lblSpread.Text = "監控已停止";
                lblSpread.ForeColor = Color.Black;
                this.Text = "加密貨幣戰情室";
            }
        }

        // 3. 計時器 Tick 事件 (需確認介面上的 Timer 有綁定此事件)
        private void timerMonitor_Tick(object sender, EventArgs e)
        {
            FetchArbitrageData();
        }

        // 4. 核心監控邏輯
        private async void FetchArbitrageData()
        {
            string selectedCoin = cmbArbCoin.Text;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"{API_URL}/arbitrage?symbol={selectedCoin}";
                    string json = await client.GetStringAsync(url);
                    var data = JsonConvert.DeserializeObject<ArbitrageData>(json);

                    if (data.status == "error")
                    {
                        lblSpread.Text = "讀取錯誤 (可能無此幣對)";
                        return;
                    }

                    // 更新介面
                    lblBinance.Text = $"{data.exchange_a}: ${data.price_a:N4}";
                    lblKraken.Text = $"{data.exchange_b}: ${data.price_b:N4}";

                    lblSpread.Text = $"價差: ${Math.Abs(data.spread):N4} ({Math.Abs(data.spread_pct):N2}%)";

                    // 警報判斷
                    double threshold = (double)numThreshold1.Value;
                    if (Math.Abs(data.spread) >= threshold)
                    {
                        lblSpread.ForeColor = Color.Red;
                        System.Media.SystemSounds.Beep.Play();
                        this.Text = $"【警報】{selectedCoin} 價差過大！";
                    }
                    else
                    {
                        lblSpread.ForeColor = Color.Green;
                        this.Text = $"監控中: {selectedCoin}";
                    }
                }
                catch (Exception ex)
                {
                    lblSpread.Text = "連線中斷...";
                }
            }
        }

        // 其他空白事件 (不需刪除，留著沒關係，避免報錯)
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        private void tabPage4_Click(object sender, EventArgs e) { }
    }
}