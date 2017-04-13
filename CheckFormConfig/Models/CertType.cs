using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    /// <summary>
    /// 表单证件类型
    /// </summary>
    public enum CertType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        IDCard = 1,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        Passport = 2,

        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Description("港澳通行证")]
        HKMacauLaissezPasser = 3,
        /// <summary>
        /// 台湾通行证
        /// </summary>
        [Description("台湾通行证")]
        MTP = 4
    }
}
