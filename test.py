
import Business as bs


# 插入股票代码
# bs.InsertStockData("data/上证股票代码.txt")
# bs.InsertStockData("data/深证股票代码.txt")
# sqlHelper = SqlHelper()

bs.UpdateAllStockData()
# bs.InsertStockData('000001',1,1)

# stocks = bs.GetAllStock()
# for StockCode,_ in stocks:
#     print(StockCode)

# data=ts.get_hist_data('sh')
# print(data.ix[0])

# for x in sorted(data.index.values, reverse=False):
#     print(x, data.ix[x])
    