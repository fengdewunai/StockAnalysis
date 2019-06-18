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

namespace Business
{
    public class ExpectBll
    {
        private List<string> allResult = new List<string>();
        Dictionary<int,List<string>> resultForType = new Dictionary<int, List<string>>();
        public void Expect()
        {
            var startDate = DateTime.Now.AddDays(-80).ToString("yyyy-MM-dd");
            var endDate = DateTime.Now.ToString("yyyy-MM-dd");
            var filePath = string.Format("c:\\ascii_Expect_{0}.txt",DateTime.Now.ToString("yyyyMMdd"));
            var allData = CommonBll.GetEverydayData(filePath, startDate, endDate);
            
            Console.WriteLine("UpAnalysisStockBll的IsUp方法开始");
            StartAnalysis(allData, startDate, 1, 2);
            
            Console.WriteLine();
            Console.WriteLine("DownAnalysisStockBll的IsUp方法开始");
            StartAnalysis(allData, startDate, 2, 3);

            Console.WriteLine("全部执行完成");
        }

        private void StartAnalysis(List<EverydayData> allData, string startDate, int bllTypeId, int analysisTypeCount)
        {
            List<Task> tasks = new List<Task>();
            for (int j = 1; j <= analysisTypeCount; j++)
            {
                var typeId = j;
                tasks.Add(Task.Factory.StartNew(() => { Analysis(allData, startDate, DateTime.Now.ToString("yyyy-MM-dd"), 0, typeId, bllTypeId); }));
            }
            Task.WaitAll(tasks.ToArray());
            var allResultGroup = allResult.GroupBy(x => x).Where(x => x.ToList().Count > 1).OrderBy(x => x.ToList().Count);
            foreach (var resultGroup in allResultGroup)
            {
                Console.WriteLine("code:{0},count:{1}", resultGroup.Key, resultGroup.ToList().Count);
            }

            foreach (var result in resultForType)
            {
                Console.WriteLine("类型：{0}", result.Key);
                Console.WriteLine("code:{0}", string.Join(",", result.Value));
            }
            allResult.Clear();
            resultForType.Clear();
        }

        private void Analysis(List<EverydayData> allData, string startDate, string endDate, int reduceDay, int analysisTypeId, int bllTypeId)
        {
            var result = new List<string>();
            var dataGroup = allData.GroupBy(x => x.StockCode);
            var index = 1;
            IAnalysisStockBll upStockBll = new UpAnalysisStockBll();
            switch (bllTypeId)
            {
                case 1:
                    upStockBll = new UpAnalysisStockBll();
                    break;
                case 2:
                    upStockBll = new DownAnalysisStockBll();
                    break;
            }
            foreach (var group in dataGroup)
            {
                try
                {
                    var groupList = group.ToList();
                    var dataList = groupList.Take(groupList.Count - reduceDay).OrderBy(x => x.CurrentDate).ToList();
                    var analysisData = dataList;
                    var topList = SocketHelper.GetTopPoint(analysisData);
                    var isUp = upStockBll.IsUp(topList, analysisData, analysisTypeId);
                    if (isUp)
                    {
                        result.Add(group.Key);
                    }
                }
                catch (Exception e)
                {
                }

                index++;
            }
            allResult.AddRange(result);
            if (resultForType.ContainsKey(analysisTypeId))
            {
                resultForType[analysisTypeId].AddRange(result);
            }
            else
            {
                resultForType.Add(analysisTypeId, result);
            }
        }
    }
}
