using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace Business
{
    public class StockBll
    {
        public static StockDal stockDal = new StockDal();

        public List<Stock> GetAllStock()
        {
            return stockDal.GetAllStock();
        }

        public List<EverydayData> GetEveryDayDataByCode(string stockCode)
        {
            return stockDal.GetEveryDayDataByCode(stockCode);
        }

        public List<EverydayData> GetEveryDayDataByDate(string startDate, string endDate)
        {
            return stockDal.GetEveryDayDataByDate(startDate, endDate);
            ;
        }

        public List<EverydayData> GetAllEveryDayData()
        {
            return stockDal.GetAllEveryDayData();
            ;
        }
    }
}
