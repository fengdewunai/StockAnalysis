using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EverydayData
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        public string StockCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string CurrentDate { get; set; }

        /// <summary>
        /// 开盘价格
        /// </summary>
        public double OpenPrice { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public double HighPrice { get; set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        public double ClosePrice { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public double LowPrice { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 价格变动
        /// </summary>
        public double Price_Change { get; set; }

        /// <summary>
        /// 涨跌幅
        /// </summary>
        public double P_Change { get; set; }

        /// <summary>
        /// 5日均价
        /// </summary>
        public double Ma5 { get; set; }

        /// <summary>
        /// 10日均价
        /// </summary>
        public double Ma10 { get; set; }

        /// <summary>
        /// 20日均价
        /// </summary>
        public double Ma20 { get; set; }

        /// <summary>
        /// 5日均量
        /// </summary>
        public double V_Ma5 { get; set; }

        /// <summary>
        /// 10日均量
        /// </summary>
        public double V_Ma10 { get; set; }

        /// <summary>
        /// 20日均量
        /// </summary>
        public double V_Ma20 { get; set; }

        /// <summary>
        /// 换手率
        /// </summary>
        public double Turnover { get; set; }
    }
}
