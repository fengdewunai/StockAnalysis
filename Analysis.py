from Dal import Dal

dal = Dal()
def IsUp(datas, lastData = 10000):
    max1, max2, max3 = 0,0,0
    if len(datas) >= 3:
        skipCount = len(datas) // 3
        max1 = max(datas[:skipCount])
        max2 = max(datas[skipCount:skipCount*2])
        max3 = max(datas[skipCount*2:])
        if max3 >= max2 and max3 > max1 and lastData > max1 and lastData > max2:
            return True
    return False

def GetTopPoint(datas):
    result = []
    newDatas = datas[-30:]
    for i in range(1,len(newDatas)-1):
        if newDatas[i] > newDatas[i-1] and newDatas[i] > newDatas[i+1]:
            result.append(newDatas[i])
    return result

originDatas = dal.GetEveryDayDataByCode("600410")
##datas = [14, 16, 15, 18, 12, 11, 14, 12, 18, 15, 22, 17, 19, 21, 21, 19, 18, 21, 19, 23,24,21,19,20,23]
datas = [ ClosePrice for StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover in originDatas]
topPoints = GetTopPoint(datas)
isUp = IsUp(topPoints, datas[-1])
print(datas[-1])
print(isUp)