using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public class Control
    {
        /// <summary>
        /// 控件ID
        /// </summary>
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [BsonElement("_id")]
        public ObjectId ControlId { get; set; }
        /// <summary>
        /// 控件名
        /// </summary>
        [BsonElement("name")]
        public string ControlName { set; get; }
        /// <summary>
        /// 模板ID
        /// </summary>
        [BsonElement("tid")]
        public ObjectId TemplateId { set; get; }

        /// <summary>
        /// 是否进入任务筛选(任务用)
        /// </summary>
        [BsonElement("ifil")]
        public bool IsFilter { set; get; }
        /// <summary>
        /// 控件类型
        /// </summary>
        [BsonElement("type")]
        public ControlType Type { set; get; }
        /// <summary>
        /// 行号
        /// </summary>
        [BsonElement("row")]
        public int Row { set; get; }
        /// <summary>
        /// 列号
        /// </summary>
        [BsonElement("col")]
        public int Col { set; get; }
        /// <summary>
        /// 模板中控件自增数
        /// </summary>
        [BsonElement("idx")]
        public int Index { set; get; }
        /// <summary>
        /// 引导文字
        /// </summary>
        [BsonElement("hint")]
        public string Hint { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        [BsonElement("def")]
        public string Default { set; get; }
        /// <summary>
        /// 是否验证
        /// </summary>
        [BsonElement("vld")]
        public bool? Validate { set; get; }
        /// <summary>
        /// 小数点位数（mongo数值类型最小单位为int）
        /// </summary>
        [BsonElement("dot")]
        public int Dot { set; get; }
        /// <summary>
        /// 单位 (如：千克、吨、元)
        /// </summary>
        [BsonElement("unit")]
        public string Unit { set; get; }
        /// <summary>
        /// 默认成员
        /// </summary>
        [BsonElement("defmen")]
        public List<string> DefaultMen { set; get; }
        /// <summary>
        /// 数据源
        /// </summary>
        [BsonElement("dtsrc")]
        public string DataSource { set; get; }
        /// <summary>
        /// 选项列表
        /// </summary>
        [BsonElement("opts")]
        public List<ControlOptions> Options { set; get; }
        /// <summary>
        /// 是否打印隐藏（OA用）
        /// </summary>
        [BsonElement("prthide")]
        public bool? PrintHide { set; get; }
        /// <summary>
        /// 是否必填（OA用）
        /// </summary>
        [BsonElement("req")]
        public bool? Required { set; get; }

        /// <summary>
        /// 控件附属枚举1
        /// </summary>
        [BsonElement("enumdef")]
        public int EnumDefault { set; get; }


        [BsonElement("u")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { set; get; }

        [BsonElement("c")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { set; get; }


        #region 辅助属性--------不进数据库
        /// <summary>
        /// 任务这边保存的值
        /// </summary>
        [BsonIgnore]
        public object Value { set; get; }

        #endregion 辅助属性--------不进数据库

    }
    [BsonIgnoreExtraElements]
    public class ControlOptions
    {
        /// <summary>
        /// 选项key
        /// </summary>
        [BsonElement("k")]
        public string Key { set; get; }
        /// <summary>
        /// 选项value
        /// </summary>
        [BsonElement("v")]
        public string Value { set; get; }
        /// <summary>
        /// 筛选项排序
        /// </summary>
        [BsonElement("idx")]
        public int Index { set; get; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [BsonElement("isdel")]
        public bool IsDeleted { set; get; }

    }
}
