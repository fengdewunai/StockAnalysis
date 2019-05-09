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
            //var expectBll = new ExpectBll();
            //expectBll.Expect();
            var validateBll = new ValidateBll();
            validateBll.Validate();
        }
    }
}
