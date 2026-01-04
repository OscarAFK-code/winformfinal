加密貨幣市場監控

一款整合即時市場監控、新聞情緒分析、技術圖表與跨交易所搬磚偵測的桌面應用程式。

本專案由兩個部分組成：
1. Server (後端)：使用 Python Flask 建置，負責資料爬蟲、數據運算與技術分析。
2. Client (前端)：使用 C# WinForms 建置，提供使用者互動介面與數據顯示。

## 功能特色

- 市場儀表板：顯示 BTC、ETH 等主流幣種即時價格、24小時漲跌幅與恐懼貪婪指數。
- 鯨魚監控：即時追蹤以太坊鏈上的大額交易資金流向。
- 新聞情緒分析：爬取 Google News 並針對標題關鍵字進行自動化情緒評分 (利多/利空)。
- 技術分析圖表：互動式 K 線圖，支援切換時區與疊加 MA 移動平均線、布林通道指標。
- 搬磚套利監控：即時比對 Binance 與 OKX 交易所價差，並在超過設定門檻時發出警報。


### 1. 下載專案 (Clone)
git clone

2. Python Server 設定
pip install -r requirements.txt

3. C# Client 設定
開啟 Client 資料夾。
雙擊 final_project.sln 檔案以在 Visual Studio 中開啟專案。

使用說明
執行本程式時，必須先啟動後端伺服器，再開啟前端視窗。

步驟 1：啟動伺服器
在 Server 資料夾中開啟終端機，執行：
python server.py
當看到 Flask 伺服器運行於 http://127.0.0.1:5000 的訊息即代表啟動成功。

步驟 2：啟動客戶端
在 Visual Studio 中點擊開始應用程式。或者直接執行 Debug 資料夾下的 .exe 執行檔。
