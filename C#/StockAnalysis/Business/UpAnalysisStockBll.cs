using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace Business
{
    public class UpAnalysisStockBll
    {
        public static StockDal stockDal = new StockDal();

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

        public bool IsUp(List<EverydayData> topList, List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3, int typeId)
        {
            var pChangeAverage = topList.Skip(Math.Max(0, topList.Count - 3)).Sum(x => x.P_Change);
            double max1 = 0, max2 = 0, max3 = 0;
            switch (typeId)
            {
                case 1:
                {
                    double volume1 = 0, volume2 = 0, volume3 = 0;
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        volume1 = topList[topList.Count - 1].Volume;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        volume2 = topList[topList.Count - 2].Volume;
                        max3 = topList[topList.Count - 3].ClosePrice;
                        volume3 = topList[topList.Count - 3].Volume;
                    }
                    var topAverage = (max1 + max2 + max3) / 3;
                    if (CommonValidate(topList, datas, lastData1, lastData2, lastData3) && lastData1.ClosePrice > topAverage && lastData2.ClosePrice > topAverage && lastData3.ClosePrice < topAverage && lastData1.ClosePrice > lastData2.ClosePrice && lastData1.Volume > lastData2.Volume && pChangeAverage > 5 && pChangeAverage < 12)
                    {
                        return true;
                    }
                    return false;
                }
                case 2:
                {
                    double volume1 = 0, volume2 = 0, volume3 = 0;
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        volume1 = topList[topList.Count - 1].Volume;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        volume2 = topList[topList.Count - 2].Volume;
                        max3 = topList[topList.Count - 3].ClosePrice;
                        volume3 = topList[topList.Count - 3].Volume;
                    }
                    var topAverage = (max1 + max2 + max3) / 3;
                    if (CommonValidate(topList, datas, lastData1, lastData2, lastData3) && lastData1.ClosePrice > max1 && lastData2.ClosePrice < max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume && pChangeAverage > 5 && pChangeAverage < 15)
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

        private bool CommonValidate(List<EverydayData> topList, List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            return Math.Abs(lastData2.ClosePrice-lastData2.OpenPrice)/ lastData2.ClosePrice > 0.016 && lastData1.Volume > lastData2.Volume && lastData1.ClosePrice > lastData3.ClosePrice;
        }
    }
}
