import hashlib
import requests
import time
 
# 百度翻译API配置信息
APP_ID = '20241018002179257'
SECRET_KEY = 'vhPEk3Gv6hvBc160VRWw'
 
# 定义百度翻译函数
def translate_to_chinese(text):
    url = "http://api.fanyi.baidu.com/api/trans/vip/translate"
    salt = str(time.time())
    sign = hashlib.md5((APP_ID + text + salt + SECRET_KEY).encode('utf-8')).hexdigest()
    params = {
        'q': text,
        'from': 'en',
        'to': 'zh',
        'appid': APP_ID,
        'salt': salt,
        'sign': sign
    }
    response = requests.get(url, params=params)
    result = response.json()
 
    # 添加错误处理和日志记录
    if 'trans_result' in result:
        return result['trans_result'][0]['dst']
    else:
        # 打印错误信息和完整的API响应
        print(f"翻译API响应错误: {result}")
        return text  # 如果翻译失败，返回原文
 
translated_text = translate_to_chinese(test_text)