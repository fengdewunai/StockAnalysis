from Dal import Dal

dal = Dal()
def IsUp(datas, lastData = 10000):
    max1, max2, max3 = 0,0,0
    if len(datas) >= 3:
        #skipCount = len(datas) // 3
        max1 = datas[-1]
        max2 = datas[-2]
        max3 = datas[-3]
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

index = 1
stocks = dal.GetAllStock()
result = []
expectDate = '2019-05-07'
for stockCode,_ in stocks:   
    expectResult = False
    originDatas = dal.GetEveryDayDataByCode(stockCode)
    ##datas = [14, 16, 15, 18, 12, 11, 14, 12, 18, 15, 22, 17, 19, 21, 21, 19, 18, 21, 19, 23,24,21,19,20,23]
    datas = [ ClosePrice for StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover in originDatas if CurrentDate < expectDate]
    if len(datas) > 0 and datas[-1] < 20:
        topPoints = GetTopPoint(datas)
        isUp = IsUp(topPoints, datas[-1])
        if isUp:
            result.append(stockCode)
            nextDayPrice = [ ClosePrice for StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover in originDatas if CurrentDate == expectDate]
            if len(nextDayPrice) > 0 and nextDayPrice[0] > datas[-1]:
                expectResult = True

    print('已处理完{0}/{1}，结果：{2}，预测结果：{3}，已筛选：{4}'.format(index,len(stocks),isUp,expectResult,result))
    index += 1
print(result)
    