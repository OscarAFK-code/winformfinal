import ccxt
import pandas as pd
import numpy as np

def get_kline_data(symbol="BTC/USDT", timeframe="1d", indicator="MA"):
    """
    抓取 K 線並計算指標，回傳給 C# 的 JSON 格式
    """
    try:
        exchange = ccxt.binance({'enableRateLimit': True})
        ohlcv = exchange.fetch_ohlcv(symbol, timeframe, limit=100)
        df = pd.DataFrame(ohlcv, columns=['time', 'open', 'high', 'low', 'close', 'vol'])
        df['time_str'] = pd.to_datetime(df['time'], unit='ms').dt.strftime('%m-%d %H:%M')

        indicators = {}
        
        if indicator == "MA":
            df['MA7'] = df['close'].rolling(window=7).mean().fillna(0)
            df['MA25'] = df['close'].rolling(window=25).mean().fillna(0)
            indicators = {
                'type': 'MA',
                'line1': df['MA7'].tolist(), 
                'line2': df['MA25'].tolist() 
            }
            
        elif indicator == "Bollinger":
            df['MA20'] = df['close'].rolling(window=20).mean()
            df['std'] = df['close'].rolling(window=20).std()
            df['upper'] = (df['MA20'] + 2 * df['std']).fillna(0)
            df['lower'] = (df['MA20'] - 2 * df['std']).fillna(0)
            indicators = {
                'type': 'Bollinger',
                'line1': df['upper'].tolist(), 
                'line2': df['lower'].tolist()  
            }

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
        ohlcv = exchange.fetch_ohlcv(symbol, timeframe=interval, limit=100)
        df = pd.DataFrame(ohlcv, columns=['timestamp', 'open', 'high', 'low', 'close', 'volume'])
        df['timestamp'] = pd.to_datetime(df['timestamp'], unit='ms')

        result_indicators = None
        
        if indicator == "MA":
            df['MA7'] = df['close'].rolling(window=7).mean()
            df['MA25'] = df['close'].rolling(window=25).mean()
            
            df.dropna(inplace=True) 
            
            df.reset_index(drop=True, inplace=True)

            result_indicators = {
                "type": "MA",
                "line1": df['MA7'].tolist(),
                "line2": df['MA25'].tolist()
            }

        elif indicator == "Bollinger":
            df.dropna(inplace=True) 
            df.reset_index(drop=True, inplace=True)
            # ...

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