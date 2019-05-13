using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Model;
using Newtonsoft.Json;
using RuanYun.Logger;

namespace StockAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var stockBll = new StockBll();

            var expectBll = new ExpectBll();
            expectBll.Expect();

            //var validateBll = new ValidateBll();
            //validateBll.Validate();


            //var datas = stockBll.GetEveryDayDataByCode("601155");
            //var upAnalysisStockBll = new UpAnalysisStockBll();
            //upAnalysisStockBll.GetContinueUpCount(datas);
            //Console.WriteLine("========================================");
            //var downAnalysisStockBll = new DownAnalysisStockBll();
            //downAnalysisStockBll.GetContinueDownCount(datas);
            //var continueUp = upAnalysisStockBll.GetCurrentContinueUpDay(datas);
            //var continueDown = downAnalysisStockBll.GetCurrentContinueDownDay(datas);
            //Console.WriteLine("上：{0}，下：{1}", continueUp, continueDown);

            Console.ReadLine();

        }
    }
}
