using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public enum DateSpanType
    {
        /// <summary>
        /// 无默认值
        /// </summary>
        None = 1,

        /// <summary>
        /// 本周
        /// </summary>
        ThisWeek = 2,
        /// <summary>
        /// 最近7天
        /// </summary>
        Last7Days = 3,
        /// <summary>
        /// 本月
        /// </summary>
        ThisMonth = 4,
        /// <summary>
        /// 指定日期
        /// </summary>
        yyyyMMddHHmmss = 5
    }
}