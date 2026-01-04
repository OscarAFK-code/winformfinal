from flask import Flask, jsonify, request
from flask_cors import CORS
import ccxt
import random

from news_scraper import fetch_google_news
from tech_analysis import get_kline_data
from arbitrage import get_arbitrage_data, get_common_pairs

try:
    from market_data import get_price_data, get_fear_and_greed_index
    from whale_watcher import get_whale_alerts
    MODULES_LOADED = True
    print("✅ 成功載入 market_data 與 whale_watcher 模組")
except ImportError as e:
    MODULES_LOADED = False
    print(f"⚠️ 模組載入失敗 ({e})，將使用 Demo 模擬數據模式")

app = Flask(__name__)
CORS(app)

exchange = ccxt.binance({'enableRateLimit': True})

# 路由

# 1. Dashboard
@app.route('/api/dashboard', methods=['GET'])
def get_dashboard():
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
        prices = _get_demo_prices()
        fng = {'value': 75, 'state': 'Greed'}
        whales = _get_demo_whales()

    return jsonify({
        'prices': prices,
        'fng': fng,
        'whales': whales
    })

# 2. News 
@app.route('/api/news')
def get_news():
    keyword = request.args.get('keyword', 'Bitcoin') 
    print(f"收到新聞搜尋請求: {keyword}")
    news_data = fetch_google_news(keyword, limit=15)

    return jsonify(news_data)

# 3. Kline 
@app.route('/api/kline', methods=['GET'])
def api_kline():
    symbol = request.args.get('symbol', 'BTC/USDT')
    interval = request.args.get('interval', '1d')
    indicator = request.args.get('indicator', 'MA')

    print(f"K線請求: {symbol} | {interval} | {indicator}")

    data = get_kline_data(symbol, interval, indicator)
    return jsonify(data)

# 4. Arbitrage Pairs
@app.route('/api/pairs')
def api_pairs():
    pairs = get_common_pairs()
    return jsonify(pairs)

# 5. Arbitrage Logic
@app.route('/api/arbitrage')
def api_arbitrage():
    symbol = request.args.get('symbol', 'BTC/USDT')
    print(f"監控價差: {symbol}")
    
    data = get_arbitrage_data(symbol)
    return jsonify(data)


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
    # 修改程式碼後 server 會自動重啟
    app.run(host='0.0.0.0', port=5000, debug=True)