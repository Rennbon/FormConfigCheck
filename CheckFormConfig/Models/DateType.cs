using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    /// <summary>
    /// 控件时间类型
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// 无默认值
        /// </summary>
        None = 1,
        /// <summary>
        /// 当天
        /// </summary>
        Current = 2,
        /// <summary>
        /// 下一天
        /// </summary>
        NextDay = 3,
        /// <summary>
        /// 指定日期
        /// </summary>
        yyyyMMddHHmmss = 4
    }
}
