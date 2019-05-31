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
    public class ExpectBll
    {
        private List<string> allResult = new List<string>();
        public void Expect()
        {
            var startDate = DateTime.Now.AddDays(-80).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = string.Format("c:\\ascii_Expect_{0}.txt",DateTime.Now.ToString("yyyyMMdd"));
            var allData = CommonBll.GetEverydayData(filePath, startDate, endDate);
            List<Task> tasks = new List<Task>();
            for (int j = 1; j <= 4; j++)
            {
                var typeId = j;
                tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.ToString("yyyy-MM-dd"), 0, typeId); }));
            }
            Task.WaitAll(tasks.ToArray());
            var allResultGroup = allResult.GroupBy(x => x).OrderBy(x => x.ToList().Count);
            foreach (var resultGroup in allResultGroup)
            {
                Console.WriteLine("code:{0},count:{1}", resultGroup.Key, resultGroup.ToList().Count);
            }
            Console.WriteLine("全部执行完成");
        }
        private void Analysis(List<EverydayData> allData, string startDate, string endDate, int reduceDay, int analysisTypeId)
        {
            var result = new List<string>();
            var dataGroup = allData.GroupBy(x => x.StockCode);
            var index = 1;
            var upStockBll = new UpAnalysisStockBll();
            foreach (var group in dataGroup)
            {
                try
                {
                    var groupList = group.ToList();
                    var dataList = groupList.Take(groupList.Count - reduceDay).OrderBy(x => x.CurrentDate).ToList();
                    var analysisData = dataList;
                    var topList = upStockBll.GetTopPoint(analysisData);
                    var isUp = upStockBll.IsUp(topList, analysisData, analysisTypeId);
                    if (isUp)
                    {
                        result.Add(group.Key);
                    }
                    //Console.WriteLine("已处理完{0}/{1}，结果：{2}，正确：{3}，错误：{4}，已筛选：{5}", index, dataGroup.Count(), isUp, expectTrue.Count, expectFail.Count, result.Count);
                }
                catch (Exception e)
                {
                    //Console.WriteLine("已处理完{0}/{1}，结果：处理出错", index, dataGroup.Count());
                }

                //Log.Write(string.Format("已处理完{0}/{1}，结果：{2}，正确：{3}，错误：{4}，已筛选：{5}", index, dataGroup.Count(), isUp, expectTrue.Count, expectFail.Count, JsonConvert.SerializeObject(result)), MessageType.Info);
                index++;
            }
            allResult.AddRange(result);
        }
    }
}
