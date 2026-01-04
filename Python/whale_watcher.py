import requests
import pandas as pd
from datetime import datetime
import time
import random
import json

# 設定區
ETHERSCAN_API_KEY = "ETUAVQGCEJS6Z755JGQ2K9C1GSEHTGHK2Z" 
BTC_THRESHOLD = 5000000  
ETH_THRESHOLD = 2000000  

BTC_PRICE_FIXED = 96000
ETH_PRICE_FIXED = 3600

def get_whale_alerts(is_demo=False):
    """ 主入口函數：嚴格模式 """
    if is_demo:
        return generate_fake_whales()
    
    whales = []
    try:
        whales.extend(get_btc_whales_real())
    except Exception as e:
        print(f"⚠️ BTC Watcher Error: {e}")

    try:
        whales.extend(get_eth_whales_real())
    except Exception as e:
        print(f"⚠️ ETH Watcher Error: {e}")

    whales.sort(key=lambda x: x['time'], reverse=True)
    return whales[:50]

def get_btc_whales_real():
    """ [真實模式] 從 Blockchain.com 抓取 """
    url = "https://blockchain.info/unconfirmed-transactions?format=json"
    whales = []
    try:
        response = requests.get(url, timeout=5)
        if response.status_code != 200:
            return []
            
        data = response.json()
        
        for tx in data.get('txs', [])[:30]: 
            total_satoshi = sum([out.get('value', 0) for out in tx.get('out', [])])
            amount_btc = total_satoshi / 100000000
            value_usd = amount_btc * BTC_PRICE_FIXED
            
            if value_usd >= BTC_THRESHOLD:
                whales.append({
                    "time": datetime.now().strftime("%H:%M:%S"),
                    "symbol": "BTC",
                    "amount": round(amount_btc, 2),
                    "value_usd": round(value_usd / 1000000, 2), # 百萬美元
                    "from": "Blockchain",
                    "link": f"https://www.blockchain.com/btc/tx/{tx.get('hash')}"
                })
    except Exception as e:
        print(f"DEBUG BTC: {e}")
    
    return whales

def get_eth_whales_real():
    """ [真實模式] 從 Etherscan V2 抓取 """
    url = "https://api.etherscan.io/v2/api"
    params = {
        "chainid": "1", 
        "module": "proxy",
        "action": "eth_getBlockByNumber",
        "tag": "latest",
        "boolean": "true",
        "apikey": ETHERSCAN_API_KEY
    }
    
    whales = []
    try:
        response = requests.get(url, params=params, timeout=5)
        data = response.json()
        
        if "result" not in data or not isinstance(data["result"], dict):
            error_msg = str(data.get("result", ""))
            print(f"DEBUG ETH: API 回傳異常: {error_msg}")

            return [{
                "time": datetime.now().strftime("%H:%M:%S"),
                "symbol": "系統通知",
                "amount": 0,
                "value_usd": 0,
                "from": "API多次被拒絕", 
                "link": "#"
            }]

        transactions = data["result"].get("transactions", [])
        
        for tx in transactions:
            try:
                value_wei = int(tx['value'], 16)
                if value_wei == 0: continue
                
                amount_eth = value_wei / 10**18
                value_usd = amount_eth * ETH_PRICE_FIXED
                
                if value_usd >= ETH_THRESHOLD:
                    whales.append({
                        "time": datetime.now().strftime("%H:%M:%S"),
                        "symbol": "ETH",
                        "amount": round(amount_eth, 2),
                        "value_usd": round(value_usd / 1000000, 2),
                        "from": tx['from'][:8] + "...",
                        "link": f"https://etherscan.io/tx/{tx['hash']}"
                    })
            except:
                continue
    except Exception as e:
        print(f"DEBUG ETH: {e}")
        return []
        
    return whales

def generate_fake_whales():
    """ 這裡已清空，不再生成假資料 """
    return []