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
    class ValidateBll
    {
        private static List<string> allResult = new List<string>();
        public static void Validate()
        {
            var dal = new StockDal();
            var startDate = DateTime.Now.AddDays(-200).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = @"c:\ascii.txt";
            var allData = new List<EverydayData>();
            if (!File.Exists(filePath))
            {
                allData = dal.GetEveryDayDataByDate(startDate, endDate);
                File.WriteAllText(@"c:\ascii.txt", JsonConvert.SerializeObject(allData));
            }
            else
            {
                var fileStr = File.ReadAllText(filePath);
                allData = JsonConvert.DeserializeObject<List<EverydayData>>(fileStr);
            }
            List<Task> tasks = new List<Task>();
            var upStockBll = new UpAnalysisStockBll();
            Console.WriteLine("IsUp1方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp1); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp1); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp1_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp1_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp1_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp2_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp2_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp2_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp3方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp3); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp3); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp3_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp3_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp3_2); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp4方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp4); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp4); }));
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            Console.WriteLine("IsUp4_2方法开始");
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), 0, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), 1, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), 2, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), 3, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), 4, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), 5, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), 6, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), 7, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd"), 8, upStockBll.IsUp4_2); }));
            tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.AddDays(-9).ToString("yyyy-MM-dd"), 9, upStockBll.IsUp4_2); }));
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
        private static void Analysis(List<EverydayData> allData, string startDate, string endDate, int reduceDay, Func<List<EverydayData>, EverydayData, EverydayData, EverydayData, bool> func)
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
                    var analysisData = dataList.Take(dataList.Count - 10).ToList();
                    var expectData = dataList.Skip(dataList.Count - 10).ToList();
                    var topList = upStockBll.GetTopPoint(analysisData);
                    var isUp = func(topList, analysisData.Last(), analysisData[analysisData.Count - 2], analysisData[analysisData.Count - 3]);
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
