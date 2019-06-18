using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Common.Helper;
using DataAccess;
using Model;
using Newtonsoft.Json;
using RuanYun.Logger;

namespace Business
{
    public class ValidateBll
    {
        private static List<ValidateResultModel> allResult = new List<ValidateResultModel>();
        private static int expectDayRang = 3;
        public void Validate()
        {
            var startDate = DateTime.Now.AddDays(-80).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = string.Format("c:\\ascii_Validate_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            var allData = CommonBll.GetEverydayData(filePath, startDate,endDate);
            Console.WriteLine("UpAnalysisStockBll的IsUp方法开始");
            StartAnalysis(allData, startDate, 1, 2);
            Console.WriteLine();
            Console.WriteLine("DownAnalysisStockBll的IsUp方法开始");
            StartAnalysis(allData, startDate, 2, 3);

            //Console.WriteLine("多次选出的正确概率");
            //var group = allResult.GroupBy(x => new {x.StockCode, x.EndDate}).Where(x => x.ToList().Count > 1);
            //foreach (var data in group)
            //{
            //    var list = data.ToList();
            //    Console.WriteLine(string.Format("日期：{0} 至 {1}，共找到{2}，正确{3}，错误{4},正确率：{5}", list[0].StartDate, data.Key.EndDate, list.Count, list.Count(x=>x.ValidateResult), list.Count(x => !x.ValidateResult), list.Count == 0 ? 0 : list.Count(x => x.ValidateResult) * 100 / list.Count), MessageType.Info);
            //}

            Console.WriteLine("全部执行完成");
        }

        private void StartAnalysis(List<EverydayData> allData, string startDate, int bllTypeId, int analysisTypeCount)
        {
            List<Task> tasks = new List<Task>();
            for (int j = 1; j <= analysisTypeCount; j++)
            {
                var typeId = j;
                Console.WriteLine("IsUp{0}方法开始", typeId);
                for (int i = 0; i <= 9; i++)
                {
                    var index = i;
                    tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, index, typeId, bllTypeId); }));
                }
                Task.WaitAll(tasks.ToArray());
                tasks.Clear();
            }
        }

        private void Analysis(List<EverydayData> allData, string startDate, int reduceDay, int analysisTypeId, int bllTypeId)
        {
            var expectTrue = new List<string>();
            var expectFail = new List<string>();
            var result = new List<ValidateResultModel>();
            var dataGroup = allData.GroupBy(x => x.StockCode);
            var index = 1;
            IAnalysisStockBll upStockBll = new DownAnalysisStockBll();
            switch (bllTypeId)
            {
                case 1:
                    upStockBll = new UpAnalysisStockBll();
                    break;
                case 2:
                    upStockBll = new DownAnalysisStockBll();
                    break;
            }
            var endDate = "";
            foreach (var group in dataGroup)
            {
                try
                {
                    var groupList = group.ToList();
                    var dataList = groupList.Take(groupList.Count - reduceDay).OrderBy(x => x.CurrentDate).ToList();
                    var analysisData = dataList.Take(dataList.Count - expectDayRang).ToList();
                    var expectData = dataList.Skip(dataList.Count - expectDayRang).ToList();
                    var topList = SocketHelper.GetTopPoint(analysisData);
                    endDate = analysisData.Last().CurrentDate;
                    var isUp = upStockBll.IsUp(topList, analysisData, analysisTypeId);
                    if (isUp)
                    {
                        
                        if (expectData.Select(x => x.ClosePrice).Max() > analysisData.Last().ClosePrice)
                        {
                            expectTrue.Add(group.Key);
                            result.Add(ValidateResultModel.Get(group.Key, true, startDate, endDate));
                        }
                        else
                        {
                            expectFail.Add(group.Key);
                            result.Add(ValidateResultModel.Get(group.Key, false, startDate, endDate));
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
            Console.WriteLine(string.Format("日期：{0} 至 {1}，共找到{2}，正确{3}，错误{4},正确率：{5}", startDate, endDate, result.Count, expectTrue.Count, expectFail.Count, result.Count == 0 ? 0 : expectTrue.Count * 100 / result.Count), MessageType.Info);
        }
    }
}
