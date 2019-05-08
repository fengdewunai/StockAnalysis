using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace StockAnalysis
{
    public class GetUpStock
    {
        public List<EverydayData> GetTopPoint(List<EverydayData> datas)
        {
            var result = new List<EverydayData>();
            var newDatas = datas.Skip(Math.Max(0, datas.Count() - 30)).ToList();
            for (int i = 1; i < newDatas.Count - 1; i++)
            {
                if(newDatas[i].ClosePrice > newDatas[i - 1].ClosePrice && newDatas[i].ClosePrice > newDatas[i + 1].ClosePrice)
                {
                    result.Add(newDatas[i]);
                }
            }
            return result;
        }

        public bool IsUp1(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 =0,max2=0,max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count-1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp1_2(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice < max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp2(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max1 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp2_2(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max1 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice < max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp3(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp3_2(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (max1 >= max2 && max2 > max3 && lastData1.ClosePrice > max1 && lastData2.ClosePrice < max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp4(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            double volume1 = 0, volume2 = 0, volume3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                volume1 = datas[datas.Count - 1].Volume;
                max2 = datas[datas.Count - 2].ClosePrice;
                volume2 = datas[datas.Count - 2].Volume;
                max3 = datas[datas.Count - 3].ClosePrice;
                volume3 = datas[datas.Count - 3].Volume;
            }
            if (lastData1.ClosePrice > max1 && lastData2.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }

        public bool IsUp4_2(List<EverydayData> datas, EverydayData lastData1, EverydayData lastData2, EverydayData lastData3)
        {
            double max1 = 0, max2 = 0, max3 = 0;
            if (datas.Count >= 3)
            {
                max1 = datas[datas.Count - 1].ClosePrice;
                max2 = datas[datas.Count - 2].ClosePrice;
                max3 = datas[datas.Count - 3].ClosePrice;
            }
            if (lastData1.ClosePrice > max1 && lastData2.ClosePrice > max1 && lastData3.ClosePrice < max1 && lastData1.Volume > lastData2.Volume)
            {
                return true;
            }
            return false;
        }
    }
}
