using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using DataAccess;
using Model;
using Model.Enum;

namespace Business
{
    public class DownAnalysisStockBll : IAnalysisStockBll
    {
        public static StockDal stockDal = new StockDal();

        public bool IsUp(List<EverydayData> topList, List<EverydayData> datas, int typeId)
        {
            var dataRange = Math.Max(0, datas.Count - 60);
            var pChangeAverage = datas.Skip(dataRange).Sum(x => x.P_Change);
            var loweastPrice = datas.Skip(dataRange).Min(x => x.ClosePrice);
            var lastData1 = datas[datas.Count - 1];
            var lastData2 = datas[datas.Count - 2];
            var lastData3 = datas[datas.Count - 3];
            switch (typeId)
            {
                case (int) DownTypeEnum.StarLine:
                {
                    if (loweastPrice < lastData2.ClosePrice || pChangeAverage > -20)
                    {
                        return false;
                    }
                    var topPrice = lastData1.ClosePrice > lastData1.OpenPrice ? lastData1.OpenPrice : lastData1.ClosePrice;
                    var changePrice = lastData1.HighPrice - lastData1.LowPrice;
                    if (Math.Abs((lastData1.ClosePrice - lastData1.OpenPrice) / changePrice) < 0.4
                        && (lastData1.HighPrice - topPrice) / changePrice < 0.4
                    )
                    {
                        return true;
                    }
                    break;
                 }
                case (int)DownTypeEnum.ContinueDown:
                {
                    if (GetCurrentContinueDownDay(datas) >= 5)
                    {
                        return true;
                    }
                    break;
                }
                case (int) DownTypeEnum.StableThenUp:
                {
                    if (topList.Count < 5)
                    {
                        return false;
                    }
                    var preTopList = topList.Take(topList.Count-2);
                    var preMaxTop = preTopList.Max(x => x.ClosePrice);
                    var preMinTop = preTopList.Min(x => x.ClosePrice);
                    var lastMaxTop = topList.Skip(topList.Count - 3).Max(x => x.ClosePrice);
                    if ((preMaxTop - preMinTop) / preMinTop < 0.1 && (lastMaxTop - preMaxTop)/preMaxTop > 0.1)
                    {
                        return true;
                    }
                    break;
                }

            }
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="everydayDatas"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="everydayDatas"></param>
        /// <returns></returns>
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
