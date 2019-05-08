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

namespace StockAnalysis
{
    public class ExpectBll
    {
        private List<string> allResult = new List<string>();
        public void Expect()
        {
            var dal = new StockDal();
            var startDate = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = @"c:\ascii2.txt";
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
            List<Task> tasks = new List<Task>();
            var upStockBll = new GetUpStock();
            Console.WriteLine("IsUp1方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp1); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp1_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp1_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp2_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp2_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp3方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp3); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp3_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp3_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp4方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp4); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp4_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp4_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            var allResultGroup = allResult.GroupBy(x => x).OrderBy(x => x.ToList().Count);
            foreach (var resultGroup in allResultGroup)
            {
                Console.WriteLine("code:{0},count:{1}", resultGroup.Key, resultGroup.ToList().Count);
            }


            Console.WriteLine("全部执行完成");
            Console.ReadLine();
        }
        private void Analysis(List<EverydayData> allData, string startDate, string endDate, int reduceDay, Func<List<EverydayData>, EverydayData, EverydayData, EverydayData, bool> func)
        {
            var result = new List<string>();
            var dataGroup = allData.GroupBy(x => x.StockCode);
            var index = 1;
            var upStockBll = new GetUpStock();
            foreach (var group in dataGroup)
            {
                try
                {
                    var groupList = group.ToList();
                    var dataList = groupList.Take(groupList.Count - reduceDay).OrderBy(x => x.CurrentDate).ToList();
                    var analysisData = dataList;
                    var topList = upStockBll.GetTopPoint(analysisData);
                    var isUp = func(topList, analysisData.Last(), analysisData[analysisData.Count - 2], analysisData[analysisData.Count - 3]);
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
