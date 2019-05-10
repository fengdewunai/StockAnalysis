using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;
using Newtonsoft.Json;
using RuanYun.Logger;

namespace Business
{
    public class ValidateBll
    {
        private static List<string> allResult = new List<string>();
        private static int expectDayRang = 3;
        public void Validate()
        {
            var startDate = DateTime.Now.AddDays(-80).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = string.Format("c:\\ascii_Validate_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            var allData = CommonBll.GetEverydayData(filePath, startDate,endDate);
            List<Task> tasks = new List<Task>();
            for (int j = 1; j <= 4; j++)
            {
                var typeId = j;
                Console.WriteLine("IsUp{0}方法开始", typeId);
                for (int i = 0; i <= 9; i++)
                {
                    var index = i;
                    tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-index - expectDayRang).ToString("yyyy-MM-dd"), index, typeId); }));
                }
                Task.WaitAll(tasks.ToArray());
                tasks.Clear();
            }
            Console.WriteLine("全部执行完成");
        }

        private void Analysis(List<EverydayData> allData, string startDate, string endDate, int reduceDay, int analysisTypeId)
        {
            var expectTrue = new List<string>();
            var expectFail = new List<string>();
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
                    var analysisData = dataList.Take(dataList.Count - expectDayRang).ToList();
                    var expectData = dataList.Skip(dataList.Count - expectDayRang).ToList();
                    var topList = upStockBll.GetTopPoint(analysisData);
                    var isUp = upStockBll.IsUp(topList, analysisData, analysisData.Last(), analysisData[analysisData.Count - 2], analysisData[analysisData.Count - 3], analysisTypeId);
                    if (isUp)
                    {
                        result.Add(group.Key);
                        if (expectData.Select(x => x.ClosePrice).Max() > analysisData.Last().ClosePrice)
                        {
                            expectTrue.Add(group.Key);
                        }
                        else
                        {
                            expectFail.Add(group.Key);
                        }
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
            Console.WriteLine(string.Format("日期：{0} 至 {1}，共找到{2}，正确{3}，错误{4},正确率：{5}", startDate, endDate, result.Count, expectTrue.Count, expectFail.Count, expectTrue.Count * 100 / result.Count), MessageType.Info);
        }
    }
}
