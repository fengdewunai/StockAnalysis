using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ValidateResultModel
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string StockCode { get; set; }

        /// <summary>
        /// 判断结果
        /// </summary>
        public bool ValidateResult { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }

        public static ValidateResultModel Get(string stockCode, bool validateResult, string startDate, string endDate)
        {
            return new ValidateResultModel()
            {
                StockCode = stockCode,
                ValidateResult = validateResult,
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
