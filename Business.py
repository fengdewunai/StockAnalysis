from RequestHelper import RequestHelper
from SqlHelper import SqlHelper
import Business as bs
import tushare as ts
from pandas import Series,DataFrame
import pandas as pd
import os


sqlHelper = SqlHelper()
def InsertStockData(filePath):
    '''插入股票代码数据'''
    with open(filePath, mode='r', encoding='UTF-8') as file_to_read:
        while True:
            lines = file_to_read.readline() # 整行读取数据
            if not lines:
                break
                pass
            stockInfos =  [info for info in lines.split(')')]
            for stockInfo in stockInfos:
                oneStock = [ info for info in stockInfo.split('(')]
                if len(oneStock) < 2:
                    continue
                sqlHelper.Call('StockAnalysis_Stock_Save',oneStock[1],oneStock[0])
            print 
            pass
    pass

def GetAllStock():
    '''获取所有股票基本信息'''
    stocks = sqlHelper.Call('StockAnalysis_Stock_Read')
    return stocks

def GetStockData(stockCode):
    '''获取某只股票的数据'''
    stockData = sqlHelper.Call('StockAnalysis_EveryDayData_ReadByCode',stockCode)
    return stockData

def InsertStockData(stockCode, stockIndex, stockTotal):
    '''插入某只股票的数据'''
    existsData = GetStockData(stockCode)
    data=ts.get_hist_data(stockCode)
    if data is None:
        return
    index = 1
    for rowName in data.index.values:
        if len(existsData) > 0 :
            isExists = [d for d in existsData if d[1] == rowName]
            if len(isExists) > 0:
                print('第{0}/{1}只股票最新数据已插入完毕'.format(stockIndex,stockTotal))
                break
        sqlHelper.Call('StockAnalysis_EveryDayData_Save',stockCode,rowName,data.ix[rowName]['open'],
        data.ix[rowName]['high'],data.ix[rowName]['close'],data.ix[rowName]['low'],data.ix[rowName]['volume'],
        data.ix[rowName]['price_change'],data.ix[rowName]['p_change'],data.ix[rowName]['ma5'],data.ix[rowName]['ma10'],
        data.ix[rowName]['ma20'],data.ix[rowName]['v_ma5'],data.ix[rowName]['v_ma10'],data.ix[rowName]['v_ma20'],0)
        print('第{0}/{1}只股票，已插入数据数量:{2}/{3}'.format(stockIndex,stockTotal,index,len(data.index.values)))
        index += 1

def UpdateAllStockData():
    '''更新所有股票数据'''
    stocks = GetAllStock()
    index = 1
    for stockCode,_ in stocks:
        InsertStockData(stockCode,index,len(stocks))
        index += 1

def CreateStockDataCSV():
    '''生成所有股票数据的CSV文件,只第一次初始化使用'''
    filename = 'd:/bigfile{0}.csv'
    stocks = GetAllStock()
    index = 0
    fileIndex = 1
    for stockCode,_ in stocks:
        index += 1
        data=ts.get_hist_data(stockCode)
        if data is None:
            continue
        col_name = data.columns.tolist()
        col_name.insert(0,'StockCode')
        data = data.reindex(columns=col_name, fill_value=stockCode)
        if index%500 == 0:
            fileIndex +=1
        newFileName = filename.format(fileIndex)
        if os.path.exists(newFileName):
            data.to_csv(newFileName, mode='a', header=None)
        else:
            data.to_csv(newFileName)
        print("已插入第{0}只股票数据",index)