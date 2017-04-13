using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    /// <summary>
    /// 表单控件关联类型
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// 项目
        /// </summary>
        Folder = 1,
        /// <summary>
        /// 任务
        /// </summary>
        Task = 2,
        /// <summary>
        /// 日程
        /// </summary>
        Calendar = 3
    }
}
