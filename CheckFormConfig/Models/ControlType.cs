using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    /// <summary>
    /// 表单控件类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 单行文本
        /// </summary>
        [Description("单行文本框")]
        Text = 1,
        /// <summary>
        /// 多行文本
        /// </summary>
        [Description("多行文本框")]
        TextArea = 2,
        /// <summary>
        /// 手机号
        /// </summary>
        [Description("手机")]
        MobilePhone = 3,
        /// <summary>
        /// 座机号
        /// </summary>
        [Description("座机")]
        Landline = 4,
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Description("邮件地址")]
        Email = 5,
        /// <summary>
        /// 数值
        /// </summary>
        [Description("数值")]
        Number = 6,
        /// <summary>
        /// 证件
        /// </summary>
        [Description("证件")]
        Certificates = 7,
        /// <summary>
        /// 金额
        /// </summary>
        [Description("金额")]
        Amount = 8,
        /// <summary>
        /// 单选框
        /// </summary>
        [Description("单选框")]
        Radio = 9,
        /// <summary>
        /// 多选框
        /// </summary>
        [Description("多选框")]
        CheckBox = 10,
        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("单选下拉菜单")]
        Select = 11,
        /// <summary>
        /// 人员单
        /// </summary>
        Personnel = 12,
        /// <summary>
        /// 人员多
        /// </summary>
        People = 13,
        /// <summary>
        /// 附件
        /// </summary>
        [Description("附件")]
        Attachment = 14,
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        Date = 15,
        /// <summary>
        /// 日期时间
        /// </summary>
        [Description("日期和时间")]
        DateTime = 16,
        /// <summary>
        /// 日期段
        /// </summary>
        DateSpan = 17,
        /// <summary>
        /// 日期时间段
        /// </summary>
        DateTimeSpan = 18,
        /// <summary>
        /// 地区(省)
        /// </summary>
        [Description("地区(省)")]
        District1 = 19,
        /// <summary>
        /// 公式
        /// </summary>
        Formula = 20,
        /// <summary>
        /// 关联到
        /// </summary>
        Relation = 21,
        /// <summary>
        /// 分割线(无编辑行为)
        /// </summary>
        [Description("分割线")]
        SplitLine = 22,
        /// <summary>
        /// 地区（省市）
        /// </summary>
        [Description("地区(省-市)")]
        District2 = 23,
        /// <summary>
        /// 地区（省市县）
        /// </summary>
        [Description("地区(省-市-县)")]
        District3 = 24,












        //////////////////////////////////////以下放只读控件////////////////////////////////////////////////////
        /// <summary>
        /// 申请人
        /// </summary>
        RO_Applicant = 10001,
        /// <summary>
        /// 申请日期
        /// </summary>
        RO_ApplyDate = 10002,
        /// <summary>
        /// 所属部门
        /// </summary>
        RO_Department = 10003,
        /// <summary>
        /// 职位
        /// </summary>
        RO_Position = 10004,
        /// <summary>
        /// 工作地点
        /// </summary>
        RO_WorkPlace = 10005,
        /// <summary>
        /// 公司
        /// </summary>
        RO_Company = 10006,
        /// <summary>
        /// 工作电话
        /// </summary>
        RO_WorkPhone = 10007,
        /// <summary>
        /// 移动电话
        /// </summary>
        RO_MobilePhone = 10008,
        /// <summary>
        /// 工号
        /// </summary>
        RO_JobNumber = 10009

    }
}

