using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Business
{
    public class UpAnalysisStockBll
    {
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
            var pChangeAverage = topList.Skip(Math.Max(0, topList.Count - 7)).Sum(x => x.P_Change);
            double max1 = 0, max2 = 0, max3 = 0;
            switch (typeId)
            {
                case 1:
                    
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        max3 = topList[topList.Count - 3].ClosePrice;
                    }
                    if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
                    {
                        return true;
                    }
                    return false;
                case 2:
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        max3 = topList[topList.Count - 3].ClosePrice;
                    }
                    if (max1 >= max2 && max1 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
                    {
                        return true;
                    }
                    return false;
                case 3:
                    if (topList.Count >= 3)
                    {
                        max1 = topList[topList.Count - 1].ClosePrice;
                        max2 = topList[topList.Count - 2].ClosePrice;
                        max3 = topList[topList.Count - 3].ClosePrice;
                    }

                    if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
                    {
                        return true;
                    }
                    return false;
                case 4:
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
                    if (CommonValidate(pChangeAverage) && lastData1.ClosePrice > topAverage && lastData2.ClosePrice < topAverage && lastData1.Volume > lastData2.Volume && pChangeAverage > 5 && pChangeAverage<12)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
            
        }

        private bool CommonValidate(double pChangeAverage)
        {
            return pChangeAverage > 10 && pChangeAverage < 15;
        }
    }
}
