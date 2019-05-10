using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace Business
{
    public class DownAnalysisStockBll
    {
        public static StockDal stockDal = new StockDal();

        public void GetContinueDownCount(List<EverydayData> everydayDatas)
        {
            var result = new Dictionary<int, int>();
            var datas = everydayDatas.Where(x => Convert.ToDateTime(x.CurrentDate) > DateTime.Now.AddDays(-300)).ToList();
            var continueCount = 0;
            for (int i = 1; i < datas.Count; i++)
            {
                if (datas[i].ClosePrice < datas[i - 1].ClosePrice)
                {
                    continueCount++;
                }
                else
                {
                    if (continueCount > 0)
                    {
                        if (result.ContainsKey(continueCount))
                        {
                            result[continueCount] = result[continueCount] + 1;
                        }
                        else
                        {
                            result.Add(continueCount, 1);
                        }
                    }
                    continueCount = 0;
                }
            }
            foreach (var dic in result.OrderBy(x => x.Key))
            {
                Console.WriteLine("{0}：{1}", dic.Key, dic.Value);
            }
        }

        public int GetCurrentContinueDownDay(List<EverydayData> everydayDatas)
        {
            var result = 0;
            var datas = everydayDatas.OrderByDescending(x => x.CurrentDate).ToList();
            for (int i = 1; i < datas.Count; i++)
            {
                if (datas[i].ClosePrice < datas[i - 1].ClosePrice)
                {
                    result++;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}
