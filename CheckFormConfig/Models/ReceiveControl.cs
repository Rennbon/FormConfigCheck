using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public class ReceiveControl
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public string ControlId { get; set; }
        /// <summary>
        /// 模板名
        /// </summary>
        public string ControlName { set; get; }
        /// <summary>
        /// 是否进入任务筛选
        /// </summary>
        public bool IsFilter { set; get; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType Type { set; get; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Row { set; get; }
        /// <summary>
        /// 列号
        /// </summary>
        public int Col { set; get; }
        /// <summary>
        /// 引导文字
        /// </summary>
        public string Hint { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { set; get; }
        /// <summary>
        /// 是否验证
        /// </summary>
        public bool? Validate { set; get; }
        /// <summary>
        /// 小数点位数（mongo数值类型最小单位为int）
        /// </summary>
        public int Dot { set; get; }
        /// <summary>
        /// 单位 (如：千克、吨、元)
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// 控件附属枚举，目前控件就一个附属枚举，以后有2个的话会拓展一个EnumDefault字段
        /// </summary>
        public int EnumDefault { set; get; }
        /// <summary>
        /// 默认成员
        /// </summary>
        public List<string> DefaultMen { set; get; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { set; get; }
        /// <summary>
        /// 选项列表
        /// </summary>
        public List<ControlOptions> Options { set; get; }
        /// <summary>
        /// 是否打印隐藏（OA用）
        /// </summary>
        public bool? PrintHide { set; get; }
        /// <summary>
        /// 是否必填（OA用）
        /// </summary>
        public bool? Required { set; get; }

        public static List<Control> Transfer(List<ReceiveControl> models)
        {
            if (models == null) return new List<Control>();
            List<Control> reList = new List<Control>();
            foreach (var item in models)
            {
                reList.Add(Transfer(item));
            }
            return reList;
        }
        public static Control Transfer(ReceiveControl model)
        {
            if (model == null) return null;
            Control re = new Control();
            re.Col = model.Col;
            re.ControlId = string.IsNullOrEmpty(model.ControlId) ? ObjectId.Empty : ObjectId.Parse(model.ControlId);
            re.ControlName = model.ControlName;
            re.DataSource = model.DataSource;
            re.Default = model.Default;
            re.DefaultMen = model.DefaultMen;
            re.Dot = model.Dot;
            re.Hint = model.Hint;
            re.IsFilter = model.IsFilter;
            re.Options = model.Options;
            re.PrintHide = model.PrintHide;
            re.EnumDefault = model.EnumDefault;
            re.Required = model.Required;
            re.Row = model.Row;
            re.Type = model.Type;
            re.Unit = model.Unit;
            re.Validate = model.Validate;
            return re;
        }
    }
}
