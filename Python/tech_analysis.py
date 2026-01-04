import ccxt
import pandas as pd
import numpy as np

def get_kline_data(symbol="BTC/USDT", timeframe="1d", indicator="MA"):
    """
    抓取 K 線並計算指標，回傳給 C# 的 JSON 格式
    """
    try:
        # 1. 連線幣安
        exchange = ccxt.binance({'enableRateLimit': True})
        # 抓取 100 根 K 線
        ohlcv = exchange.fetch_ohlcv(symbol, timeframe, limit=100)
        
        # 轉成 DataFrame
        df = pd.DataFrame(ohlcv, columns=['time', 'open', 'high', 'low', 'close', 'vol'])
        # 時間轉字串 (讓 C# 好讀)
        df['time_str'] = pd.to_datetime(df['time'], unit='ms').dt.strftime('%m-%d %H:%M')
        
        # 2. 計算指標
        indicators = {}
        
        if indicator == "MA":
            # 計算 MA7 和 MA25
            df['MA7'] = df['close'].rolling(window=7).mean().fillna(0)
            df['MA25'] = df['close'].rolling(window=25).mean().fillna(0)
            indicators = {
                'type': 'MA',
                'line1': df['MA7'].tolist(), # 黃線
                'line2': df['MA25'].tolist() # 藍線
            }
            
        elif indicator == "Bollinger":
            # 計算布林通道 (20MA, 2std)
            df['MA20'] = df['close'].rolling(window=20).mean()
            df['std'] = df['close'].rolling(window=20).std()
            df['upper'] = (df['MA20'] + 2 * df['std']).fillna(0)
            df['lower'] = (df['MA20'] - 2 * df['std']).fillna(0)
            indicators = {
                'type': 'Bollinger',
                'line1': df['upper'].tolist(), # 上軌
                'line2': df['lower'].tolist()  # 下軌
            }

        # 3. 打包回傳資料
        return {
            'times': df['time_str'].tolist(),
            'open': df['open'].tolist(),
            'high': df['high'].tolist(),
            'low': df['low'].tolist(),
            'close': df['close'].tolist(),
            'indicators': indicators
        }
        
    except Exception as e:
        print(f"Tech Analysis Error: {e}")
        return {}
exchange = ccxt.binance({
    'enableRateLimit': True,
    'options': {'defaultType': 'spot'}
})
def get_kline_data(symbol="BTC/USDT", interval="1d", indicator="MA"):
    try:
        # 1. 抓取數據 (原本的程式碼)
        ohlcv = exchange.fetch_ohlcv(symbol, timeframe=interval, limit=100)
        df = pd.DataFrame(ohlcv, columns=['timestamp', 'open', 'high', 'low', 'close', 'volume'])
        df['timestamp'] = pd.to_datetime(df['timestamp'], unit='ms')

        # 2. 計算指標 (原本的程式碼)
        result_indicators = None
        
        if indicator == "MA":
            # 計算 MA
            df['MA7'] = df['close'].rolling(window=7).mean()
            df['MA25'] = df['close'].rolling(window=25).mean()
            
            # ★ 關鍵修改在這！======================================
            # 刪除含有 NaN (空值) 的行。
            # 這樣前 25 筆因為算不出 MA25 而變成 NaN 的資料就會被切掉。
            df.dropna(inplace=True) 
            # 重整索引 (這行保險起見加上，雖然 dropna 後轉 list 不影響)
            df.reset_index(drop=True, inplace=True)
            # ====================================================

            result_indicators = {
                "type": "MA",
                "line1": df['MA7'].tolist(),
                "line2": df['MA25'].tolist()
            }

        elif indicator == "Bollinger":
            # ... (布林通道邏輯) ...
            # 同樣要在計算完後加入 dropna
            df.dropna(inplace=True) 
            df.reset_index(drop=True, inplace=True)
            # ...

        # 3. 格式化回傳
        # 注意：因為上面 dropna 了，這裡回傳的 times, open, high... 也會變短，
        # 這樣 K 線和指標就會完美對齊，不會有 0 的情況了。
        return {
            "times": df['timestamp'].dt.strftime('%Y-%m-%d %H:%M').tolist(),
            "open": df['open'].tolist(),
            "high": df['high'].tolist(),
            "low": df['low'].tolist(),
            "close": df['close'].tolist(),
            "indicators": result_indicators
        }

    except Exception as e:
        print(f"KLine Error: {e}")
        return {"error": str(e)}