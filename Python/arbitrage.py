import ccxt


binance = ccxt.binance()
okx = ccxt.okx()

def get_common_pairs():
    """
    掃描兩間交易所，找出共同都有的 USDT 交易對
    """
    try:
        # 1. 載入市場資料
        print("正在掃描 Binance 與 OKX 市場...")
        binance_markets = binance.load_markets()
        okx_markets = okx.load_markets()

        # 2. 比較主流幣
        b_symbols = set([s for s in binance_markets.keys() if s.endswith('/USDT')])
        o_symbols = set([s for s in okx_markets.keys() if s.endswith('/USDT')])

        # 3. 取交集
        common = list(b_symbols.intersection(o_symbols))
        common.sort()
        
        print(f"找到 {len(common)} 個共同幣種")
        return common
    except Exception as e:
        print(f"Fetch Symbols Error: {e}")
        return ["BTC/USDT", "ETH/USDT"] 

def get_arbitrage_data(symbol="BTC/USDT"):
    """
    比較 Binance 和 OKX 的價格
    """
    try:
        
        t1 = binance.fetch_ticker(symbol)
        t2 = okx.fetch_ticker(symbol)
        
        price_a = t1['last']
        price_b = t2['last']
        
        spread = price_a - price_b
        spread_pct = (spread / price_a) * 100
        
        return {
            "exchange_a": "Binance",
            "price_a": price_a,
            "exchange_b": "OKX",
            "price_b": price_b,
            "symbol": symbol,
            "spread": spread,
            "spread_pct": spread_pct,
            "status": "success"
        }
        
    except Exception as e:
        print(f"Arbitrage Error ({symbol}): {e}")
        return {
            "status": "error", 
            "message": str(e),
            "exchange_a": "Binance", "price_a": 0,
            "exchange_b": "OKX", "price_b": 0,
            "spread": 0, "spread_pct": 0
        }