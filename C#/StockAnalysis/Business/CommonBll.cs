using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;
using Newtonsoft.Json;

namespace Business
{
    public class CommonBll
    {
        private static StockDal dal = new StockDal();
        public static List<EverydayData> GetEverydayData(string filePath, string startDate, string endDate)
        {
            var allData = new List<EverydayData>();
            if (!File.Exists(filePath))
            {
                allData = dal.GetEveryDayDataByDate(startDate, endDate);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(allData));
            }
            else
            {
                var fileStr = File.ReadAllText(filePath);
                allData = JsonConvert.DeserializeObject<List<EverydayData>>(fileStr);
            }
            return allData;
        }
    }
}
