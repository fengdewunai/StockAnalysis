using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DataAccess
{
    public class StockDal : DataAccessBase
    {
        /// <summary>
        /// 保存ExamSet
        /// </summary>
        public List<Stock> GetAllStock()
        {
            return CurrentConnectStringContext.StoredProcedure("StockAnalysis_Stock_Read").QueryMany<Stock>();
        }

        public List<EverydayData> GetEveryDayDataByCode(string stockCode)
        {
            return CurrentConnectStringContext.StoredProcedure("StockAnalysis_EveryDayData_ReadByCode")
                .Parameter("v_StockCode", stockCode)
                .QueryMany<EverydayData>();
        }

        public List<EverydayData> GetEveryDayDataByDate(string startDate, string endDate)
        {
            return CurrentConnectStringContext.StoredProcedure("StockAnalysis_EveryDayData_ReadByDate")
                .Parameter("v_StartDate", startDate)
                .Parameter("v_EndDate", endDate)
                .QueryMany<EverydayData>();
        }

        public List<EverydayData> GetAllEveryDayData()
        {
            return CurrentConnectStringContext.StoredProcedure("StockAnalysis_EveryDayData_ReadAll").QueryMany<EverydayData>();
        }
    }
}
