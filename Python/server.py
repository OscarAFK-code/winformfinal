from flask import Flask, jsonify, request
from flask_cors import CORS
import ccxt
import random

# --- 引用你的功能模組 ---
# 1. 確保這些檔案 (news_scraper.py, tech_analysis.py, arbitrage.py) 都在同一個資料夾
from news_scraper import fetch_google_news
from tech_analysis import get_kline_data
# 修正 import：合併寫在同一行，避免重複
from arbitrage import get_arbitrage_data, get_common_pairs

# --- 嘗試引用 Lab 1 的模組 ---
try:
    from market_data import get_price_data, get_fear_and_greed_index
    from whale_watcher import get_whale_alerts
    MODULES_LOADED = True
    print("✅ 成功載入 market_data 與 whale_watcher 模組")
except ImportError as e:
    MODULES_LOADED = False
    print(f"⚠️ 模組載入失敗 ({e})，將使用 Demo 模擬數據模式")

app = Flask(__name__)
CORS(app) # 允許 C# 呼叫

# 初始化交易所 (備用)
exchange = ccxt.binance({'enableRateLimit': True})

# ==================== 路由定義 ====================

# 1. Dashboard (儀表板數據)
@app.route('/api/dashboard', methods=['GET'])
def get_dashboard():
    # 獲取價格 (BTC, ETH, SOL)
    if MODULES_LOADED:
        try:
            df_price = get_price_data(coins=['BTC', 'ETH', 'SOL'])
            prices = df_price.to_dict(orient='records')
        except:
            prices = _get_demo_prices()
            
        try:
            fng = get_fear_and_greed_index()
        except:
            fng = {'value': 50, 'state': 'Neutral'}

        try:
            whales = get_whale_alerts(is_demo=False)
        except:
            whales = []
    else:
        # 如果模組壞了，用備用邏輯
        prices = _get_demo_prices()
        fng = {'value': 75, 'state': 'Greed'}
        whales = _get_demo_whales()

    return jsonify({
        'prices': prices,
        'fng': fng,
        'whales': whales
    })

# 2. News (新聞搜尋)
@app.route('/api/news')
def get_news():
    # 從網址參數取得關鍵字，例如 /api/news?keyword=ETH
    keyword = request.args.get('keyword', 'Bitcoin') 
    print(f"收到新聞搜尋請求: {keyword}")

    # 呼叫爬蟲
    news_data = fetch_google_news(keyword, limit=15)

    return jsonify(news_data)

# 3. K-Line (K線圖數據)
@app.route('/api/kline', methods=['GET'])
def api_kline():
    symbol = request.args.get('symbol', 'BTC/USDT')
    interval = request.args.get('interval', '1d')
    indicator = request.args.get('indicator', 'MA')

    print(f"K線請求: {symbol} | {interval} | {indicator}")

    data = get_kline_data(symbol, interval, indicator)
    return jsonify(data)

# 4. Arbitrage Pairs (取得共同幣種列表) - Lab 4 新增
@app.route('/api/pairs')
def api_pairs():
    pairs = get_common_pairs()
    return jsonify(pairs)

# 5. Arbitrage Logic (搬磚套利數據) - Lab 4 修改版
@app.route('/api/arbitrage')
def api_arbitrage():
    # 預設是 BTC/USDT，但如果有傳參數就用參數 (例如 ?symbol=ETH/USDT)
    symbol = request.args.get('symbol', 'BTC/USDT')
    print(f"監控價差: {symbol}")
    
    data = get_arbitrage_data(symbol)
    return jsonify(data)

# ==================== 輔助函式 (Demo Data) ====================

def _get_demo_prices():
    return [
        {'Coin': 'BTC', 'Price': 96000 + random.randint(-100, 100), 'Change24h%': 2.5},
        {'Coin': 'ETH', 'Price': 3600 + random.randint(-50, 50), 'Change24h%': -1.2},
        {'Coin': 'SOL', 'Price': 150 + random.randint(-5, 5), 'Change24h%': 5.8}
    ]

def _get_demo_whales():
    return [
        {'time': '12:00', 'symbol': 'BTC', 'amount': 100, 'value_usd': 9500000},
        {'time': '12:05', 'symbol': 'ETH', 'amount': 5000, 'value_usd': 15000000}
    ]

if __name__ == '__main__':
    print("🚀 Server 啟動中... 請執行 WinForms 程式")
    # debug=True 讓你在修改程式碼後 server 會自動重啟
    app.run(host='0.0.0.0', port=5000, debug=True)