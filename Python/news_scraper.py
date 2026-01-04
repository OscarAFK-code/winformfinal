import requests
from bs4 import BeautifulSoup
import urllib.parse

def analyze_sentiment(title):
    """
    簡易情緒分析：根據標題關鍵字給分
    """
    # 1. 定義關鍵字庫
    positive_keywords = [
        "上漲", "大漲", "飆升", "突破", "新高", "牛市", "看好", 
        "獲利", "反彈", "加倉", "抄底", "增持", "多單", 
        "Bull", "Surge", "High", "Record", "Jump", "Gain", "Profit"
    ]

    negative_keywords = [
        "下跌", "大跌", "暴跌", "崩盤", "新低", "熊市", "看空", 
        "虧損", "回調", "減倉", "拋售", "死叉", "空單",
        "Bear", "Drop", "Crash", "Low", "Loss", "Sell", "Plunge"
    ]

    score = 0
    title_lower = title.lower()

    for k in positive_keywords:
        if k.lower() in title_lower:
            score += 1
            
    for k in negative_keywords:
        if k.lower() in title_lower:
            score -= 1
            
    if score > 0: return "利多 📈"
    if score < 0: return "利空 📉"
    return "中立 😐"

def fetch_google_news(keyword="Bitcoin", limit=10):
    """
    爬取 Google News RSS 並回傳 List[Dict]
    """
    # 根據關鍵字是否有中文字元，決定搜尋語言
    is_chinese = any(u'\u4e00' <= c <= u'\u9fff' for c in keyword)
    
    if is_chinese:
        params = "hl=zh-TW&gl=TW&ceid=TW:zh-Hant"
    else:
        params = "hl=en-US&gl=US&ceid=US:en"
        
    # URL 編碼關鍵字
    encoded_keyword = urllib.parse.quote(keyword)
    rss_url = f"https://news.google.com/rss/search?q={encoded_keyword}&{params}"
    
    headers = {
        "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36"
    }

    news_list = []
    try:
        response = requests.get(rss_url, headers=headers, timeout=10)
        if response.status_code == 200:
            soup = BeautifulSoup(response.content, features="xml")
            items = soup.find_all("item")
            
            for item in items[:limit]:
                title = item.title.text
                link = item.link.text
                pub_date = item.pubDate.text
                
                # 進行情緒分析
                sentiment = analyze_sentiment(title)
                
                news_list.append({
                    "title": title,
                    "link": link,
                    "date": pub_date,
                    "sentiment": sentiment
                })
    except Exception as e:
        print(f"News Scraper Error: {e}")
        
    return news_list