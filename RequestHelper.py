import requests

class RequestHelper:
    def __init__(self,url):
        self.headers = {'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36'}
        self.url = url

    def Get(self,params=''):
        r = requests.get(self.url, params=params) #像目标url地址发送get请求，返回一个response对象
        return r

    def GetText(self,params=''):
        r = requests.get(self.url, headers=self.headers, params=params) #像目标url地址发送get请求，返回一个response对象
        return r.text

    def Post(self,params=''):
        r = requests.post(self.url, data=params)
        return r

    def PostText(self,params=''):
        r = requests.post(self.url, data=params)
        return r.text