using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;
using Model.Enum;

namespace Business
{
    /// <summary>
    /// 上升分析
    /// </summary>
    public class UpAnalysisStockBll
    {
        public static StockDal stockDal = new StockDal();

        /// <summary>
        /// 获取峰值集合
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public List<EverydayData> GetTopPoint(List<EverydayData> datas)
        {
            var result = new List<EverydayData>();
            var newDatas = datas.Skip(Math.Max(0, datas.Count() - 30)).ToList();
            for (int i = 1; i < newDatas.Count - 1; i++)
            {
                if (newDatas[i].ClosePrice > newDatas[i - 1].ClosePrice && newDatas[i].ClosePrice > newDatas[i + 1].ClosePrice)
                {
                    result.Add(newDatas[i]);
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否上升
        /// </summary>
        /// <param name="topList"></param>
        /// <param name="datas"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public bool IsUp(List<EverydayData> topList, List<EverydayData> datas, int typeId)
        {
            var pChangeAverage = datas.Skip(Math.Max(0, datas.Count - 10)).Sum(x => x.P_Change);
            double max1 = 0, max2 = 0, max3 = 0;
            var lastData1 = datas[datas.Count - 1];
            var lastData2 = datas[datas.Count - 2];
            var lastData3 = datas[datas.Count - 3];
            switch (typeId)
            {
                case (int)UpTypeEnum.UpTwoTop:
                {
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        max3 = topList[topList.Count - 3].ClosePrice;
                    }
                    var topAverage = (max1 + max2 + max3) / 3;
                    if (CommonValidate(topList, datas, lastData1, lastData2, lastData3) 
                        && lastData1.ClosePrice > lastData2.ClosePrice && lastData1.ClosePrice > lastData3.ClosePrice 
                        && lastData1.ClosePrice > max1 && lastData1.ClosePrice > max2 
                        && pChangeAverage > 5 && pChangeAverage < 15)
                    {
                        return true;
                    }
                    return false;
                }
                case (int)UpTypeEnum.HasUpWindow:
                {
                    double volume1 = 0, volume2 = 0, volume3 = 0;
                    if (CommonValidate(topList, datas, lastData1, lastData2, lastData3) 
                        && lastData1.LowPrice > lastData2.HighPrice && lastData1.P_Change > 0)
                    {
                        return true;
                    }
                    return false;
                }
                case 3:
                    return false;
                case 4:
                {
                    return false;
                }
                    
                default:
                    return false;
            }
            
        }

        /// <summary>
        /// 历史连续上升的天数集合
        /// </summary>
        /// <param name="everydayDatas"></param>
        public void GetContinueUpCount(List<EverydayData> everydayDatas)
        {
            var result = new Dictionary<int, int>();
            var datas = everydayDatas.Where(x=>Convert.ToDateTime(x.CurrentDate) > DateTime.Now.AddDays(-300)).OrderBy(x=>x.CurrentDate).ToList();
            var continueCount = 0;
            for (int i = 1; i < datas.Count; i++)
            {
                if (datas[i].ClosePrice > datas[i - 1].ClosePrice)
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
                Console.WriteLine("{0}：{1}",dic.Key,dic.Value);
            }
        }

        /// <summary>
        /// 到目前为止连续上升的天数
        /// </summary>
        /// <param name="everydayDatas"></param>
        /// <returns></returns>
        public int GetCurrentContinueUpDay(List<EverydayData> everydayDatas)
        {
            var result = 0;
            var datas = everydayDatas.OrderByDescending(x => x.CurrentDate).ToList();
            for (int i = 1; i < datas.Count; i++)
            {
                if (datas[i].ClosePrice > datas[i - 1].ClosePrice)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topList">峰值集合</param>
        /// <param name="datas">数据集合</param>
        /// <param name="lastData1">最后一天数据</param>
        /// <param name="lastData2">倒数第二天数据</param>
        /// <param name="lastData3">倒数第三条数据</param>
        /// <returns></returns>
        private bool CommonValidate(List<EverydayData> topList, List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            var upContinueDays = GetCurrentContinueUpDay(datas);
            return Math.Abs(lastData2.ClosePrice-lastData2.OpenPrice)/ lastData2.ClosePrice > 0.016  && upContinueDays <=4 && topList.Max(x=>x.Volume) > 100000;
        }
    }
}
