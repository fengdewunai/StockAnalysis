using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    /// <summary>
    /// 上升类型
    /// </summary>
    public enum UpTypeEnum
    {
        /// <summary>
        /// 高于前两个峰值
        /// </summary>
        UpTwoTop = 1,

        /// <summary>
        /// 有上升窗口
        /// </summary>
        HasUpWindow = 2,
    }
}
