from SqlHelper import SqlHelper

class Dal:
    def __init__(self):
        # 获取游标
        self.sqlHelper = SqlHelper()

    def SaveStock(self, stockCode, stockName):
        return self.sqlHelper.Call('StockAnalysis_Stock_Save',stockCode,stockName)

    def GetAllStock(self):
        return self.sqlHelper.Call('StockAnalysis_Stock_Read')

    def GetEveryDayDataByCode(self,stockCode):
        return self.sqlHelper.Call('StockAnalysis_EveryDayData_ReadByCode',stockCode)

    def SaveEveryDayData(self,StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover):
        return self.sqlHelper.Call('StockAnalysis_EveryDayData_Save',StockCode,CurrentDate,OpenPrice,HighPrice,ClosePrice,LowPrice,Volume,Price_Change,P_Change,Ma5,Ma10,Ma20,V_Ma5,V_Ma10,V_Ma20,Turnover)

    