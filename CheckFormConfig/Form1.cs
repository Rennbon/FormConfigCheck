using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckFormConfig.Models;
using MongoDB.Bson;

namespace CheckFormConfig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类 
            OpenFileDialog fileDialog = new OpenFileDialog();
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string extension = Path.GetExtension(fileDialog.FileName);
                //声明允许的后缀名 
                string[] str = new string[] { ".txt", ".json" };
                if (!str.Contains(extension))
                {
                    MessageBox.Show("文件格式不对，只支持txt和json");
                }
                else
                {
                    try
                    {
                        FormConfig fc = FormConfig.GetIntance(fileDialog.FileName);
                        int itemIndex = 0;
                        if (fc.Config == null)
                        {
                            throw new Exception("文档为空");
                        }
                        foreach (var item in fc.Config)
                        {
                            if (item.TemplateTypeName == "常用模板")
                            {

                            }
                            else
                            {
                                for (int i = 0; i < item.Templates.Count; i++)
                                {
                                    //验证templateId 和templateTypeName
                                    int realNum1 = GetTemplateIdNum22(item.TemplateTypeName,itemIndex-1);
                                    string realNum2 = (i + 1).ToString();
                                    if (realNum2.Length == 1)
                                    {
                                        realNum2 = "0" + realNum2;
                                    }
                                    ObjectId idIndex = new ObjectId();
                                    if (ObjectId.TryParse(item.Templates[i].TemplateId, out idIndex))
                                    {
                                        bool tidCheck = false;
                                        try
                                        {
                                            int num1 = Convert.ToInt32(item.Templates[i].TemplateId.Substring(21, 1));
                                            string num0 = item.Templates[i].TemplateId.Substring(0, 21);
                                            int num2 = Convert.ToInt32(item.Templates[i].TemplateId.Substring(22));
                                            if (num2 == i + 1 && realNum1 == num1 && num0 == "000000000000000000000")
                                            {
                                                tidCheck = true;
                                            }
                                        }
                                        finally
                                        {
                                            if (!tidCheck)
                                            {
                                                throw new Exception($"{item.TemplateTypeName}下，第{i + 1}个模板的TemplateId不正确,应该是：000000000000000000000{realNum1}{realNum2}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception($"{item.TemplateTypeName}下，第{i + 1}个模板的TemplateId不正确,应该是：000000000000000000000{realNum1}{realNum2}");
                                    }
                                    //验证controls
                                    bool controlFlag = false;
                                    try
                                    {
                                        var list = ReceiveControl.Transfer(item.Templates[i].Controls);
                                        for (int c = 0; c < list.Count; c++)
                                        {
                                            if (!CheckControl(list[c]))
                                            {
                                                controlFlag = true;
                                                throw new Exception($"{item.TemplateTypeName}下，TemplateId为{item.Templates[i].TemplateId}下的Controls中的第{c+1}个control错误！！！");
                                            }
                                        }    
                                    }
                                    catch(Exception ex)
                                    {
                                        if (controlFlag)
                                        {
                                            throw ex;
                                        }
                                        else
                                        {
                                            throw new Exception($"{item.TemplateTypeName}下，TemplateId为{item.Templates[i].TemplateId}下的Controls结构错误");
                                        }
                                    }
                                    //验证看板
                                    bool stageFlag = false;

                                    if (item.Templates[i].Stages != null && item.Templates[i].Stages.Count > 0)
                                    {
                                        stageFlag = true;
                                    }
                                    if (!stageFlag)
                                    {
                                        throw new Exception($"{item.TemplateTypeName}下，TemplateId为{item.Templates[i].TemplateId}下的Stages看板不能为空");
                                    }

                                }
                               
                            }
                            itemIndex++;
                        }
                        MessageBox.Show("初步通过，可以拿给开发了");
                    }
                    catch (Exception ex)
                    {
                        DialogResult result1 = MessageBox.Show(ex.Message);

                    }
                }
            }
        }



        private static int GetTemplateIdNum22(string templateTypeName, int index)
        {
            bool flag = false;
            switch (templateTypeName)
            {
                case "产品研发": if (index == 0) return 0; else flag = true;break;
                case "行政人事": if (index == 1) return 1; else flag = true; break;
                case "销售市场": if (index == 2) return 2; else flag = true; break;
                case "电商平台": if (index == 3) return 3; else flag = true; break;
                case "其他行业": if (index == 4) return 4; else flag = true; break;
            }
            if (flag)
            {
                throw new Exception($"依次常用模板，产品研发，行政人事，电商平台，其他行业，请检查顺序");
            }
            else
            {
                throw new Exception($"templateTypeName：{templateTypeName},如果是乱码，请在txt中选择gb2312啊，如果不是就是名字错了，只支持：常用模板，产品研发，行政人事，电商平台，其他行业");
            }          
        }
        public static bool CheckControls(List<Models.Control> list)
        {
            if (list == null && list.Count == 0)
            {
                return false;
            }
            foreach (var item in list)
            {
                if (!CheckControl(item))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 校验控件value
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool CheckControl(Models.Control model)
        {
            switch (model.Type)
            {
                case ControlType.Text: return Type1_Text(model);
                case ControlType.TextArea: return Type2_TextArea(model);
                case ControlType.MobilePhone: return Type3_MobilePhone(model);
                case ControlType.Landline: return Type4_Landline(model);
                case ControlType.Email: return Type5_Email(model);
                case ControlType.Number: return Type6_Number(model);
                case ControlType.Certificates: return Type7_Certificates(model);
                case ControlType.Amount: return Type8_Amount(model);
                case ControlType.Radio: return Type9_Radio(model);
                case ControlType.CheckBox: return Type10_CheckBox(model);
                case ControlType.Select: return Type11_Select(model);
                case ControlType.Personnel: return Type12_Personnel(model);
                case ControlType.People: return Type13_People(model);
                case ControlType.Attachment: return Type14_Attachment(model);
                case ControlType.Date: return Type15_Date(model);
                case ControlType.DateTime: return Type16_DateTime(model);
                case ControlType.DateSpan: return Type17_DateSpan(model);
                case ControlType.DateTimeSpan: return Type18_DateTimeSpan(model);
                case ControlType.District1: return Type19_District1(model);
                case ControlType.Formula: return Type20_Formula(model);
                case ControlType.Relation: return Type21_Relation(model);
                case ControlType.SplitLine: return Type22_SplitLine(model);
                case ControlType.District2: return Type23_District2(model);
                case ControlType.District3: return Type24_District3(model);
                case ControlType.RO_Applicant:
                case ControlType.RO_ApplyDate:
                case ControlType.RO_Company:
                case ControlType.RO_Department:
                case ControlType.RO_JobNumber:
                case ControlType.RO_MobilePhone:
                case ControlType.RO_Position:
                case ControlType.RO_WorkPhone:
                case ControlType.RO_WorkPlace: return CheckControlName(model);
                default: return false;
            }
        }
        #region ControlType 类型验证 1:1验证

        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type1_Text(Models.Control model)
        {
            return CheckDefault(model);
        }
        /// <summary>
        /// 验证文本域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type2_TextArea(Models.Control model)
        {
            return CheckDefault(model);
        }
        /// <summary>
        /// 验证手机
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type3_MobilePhone(Models.Control model)
        {
            return CheckDefault(model);
        }
        /// <summary>
        /// 验证座机
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type4_Landline(Models.Control model)
        {
            return CheckDefault(model);
        }
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type5_Email(Models.Control model)
        {
            return CheckDefault(model);
        }
        /// <summary>
        /// 验证数值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type6_Number(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (string.IsNullOrEmpty(model.Unit))
                return false;
            return true;

        }
        /// <summary>
        /// 验证证件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type7_Certificates(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (!System.Enum.IsDefined(typeof(CertType), model.EnumDefault))
                return false;
            return true;
        }
        /// <summary>
        /// 验证金额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type8_Amount(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (string.IsNullOrEmpty(model.Unit))
                return false;
            return true;
        }
        /// <summary>
        /// 验证单选按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type9_Radio(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (model.Options == null || model.Options.Count == 0)
                return false;
            //默认值

            OptionsOrderBy(model);
            double defaultTemp = 0;
            if (!string.IsNullOrEmpty(model.Default))
            {
                if (!double.TryParse(model.Default, out defaultTemp))
                {
                    return false;
                }
            }
            bool tempFlag = false;
            int i = 0;
            foreach (var item in model.Options)
            {
                double keyTemp = 0;
                if (!double.TryParse(item.Key, out keyTemp))
                {
                    return false;
                }
                if (Math.Pow(2, i) != keyTemp)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.Value))
                {
                    return false;
                }
                if (defaultTemp != 0)
                {
                    if (keyTemp == defaultTemp)
                    {
                        tempFlag = true;
                    }
                }
                i++;
            }
            if (defaultTemp == 0)
            {
                tempFlag = true;
            }
            if (!tempFlag)
                return false;
            return true;
        }
        /// <summary>
        /// 验证多选按钮
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type10_CheckBox(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (model.Options == null || model.Options.Count == 0)
                return false;
            OptionsOrderBy(model);
            int i = 0;
            double sum = 0;
            foreach (var item in model.Options)
            {
                double keyTemp = 0;
                if (!double.TryParse(item.Key, out keyTemp))
                {
                    return false;
                }
                if (Math.Pow(2, i) != keyTemp)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.Value))
                {
                    return false;
                }
                sum += keyTemp;
                i++;
            }
            //验证默认值是否符合条件
            if (!string.IsNullOrEmpty(model.Default))
            {
                double defaultTemp = 0;
                if (!double.TryParse(model.Default, out defaultTemp))
                {
                    return false;
                }
                if (defaultTemp < 0 || defaultTemp > sum)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 验证下拉框
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type11_Select(Models.Control model)
        {
            if (!CheckDefault(model))
                return false;
            if (model.Options == null || model.Options.Count == 0)
                return false;
            OptionsOrderBy(model);
            //默认值
            double defaultTemp = 0;
            if (!string.IsNullOrEmpty(model.Default))
            {
                if (!double.TryParse(model.Default, out defaultTemp))
                {
                    return false;
                }
            }
            bool tempFlag = false;
            int i = 0;
            foreach (var item in model.Options)
            {
                double keyTemp = 0;
                if (!double.TryParse(item.Key, out keyTemp))
                {
                    return false;
                }
                if (Math.Pow(2, i) != keyTemp)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.Value))
                {
                    return false;
                }
                if (defaultTemp != 0)
                {
                    if (keyTemp == defaultTemp)
                    {
                        tempFlag = true;
                    }
                }
                i++;
            }
            if (defaultTemp == 0)
            {
                tempFlag = true;
            }
            if (!tempFlag)
                return false;
            return true;
        }
        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        private static bool Type12_Personnel(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.DefaultMen != null && model.DefaultMen.Count > 0)
            {
                Guid guid = Guid.Empty;
                if (!Guid.TryParse(model.DefaultMen.First(), out guid))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证人员单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type13_People(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.DefaultMen != null && model.DefaultMen.Count > 0)
            {
                Guid guid = Guid.Empty;
                for (int i = 0; i < model.DefaultMen.Count; i++)
                {
                    if (!Guid.TryParse(model.DefaultMen[i], out guid))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 验证人员多
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type14_Attachment(Models.Control model)
        {
            return CheckControlName(model);
        }
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type15_Date(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.EnumDefault != 0)
            {
                if (!System.Enum.IsDefined(typeof(DateType), model.EnumDefault))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证日期时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type16_DateTime(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.EnumDefault != 0)
            {
                if (!System.Enum.IsDefined(typeof(DateType), model.EnumDefault))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证日期段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type17_DateSpan(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.EnumDefault != 0)
            {
                if (!System.Enum.IsDefined(typeof(DateSpanType), model.EnumDefault))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证日期时间段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type18_DateTimeSpan(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            if (model.EnumDefault != 0)
            {
                if (!System.Enum.IsDefined(typeof(DateSpanType), model.EnumDefault))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 地区（省）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type19_District1(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            return true;
        }

        /// <summary>
        /// 地区公式（该功能暂时不做）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type20_Formula(Models.Control model)
        {
            return false;
        }
        /// <summary>
        /// 关联到 （该功能暂时不做）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type21_Relation(Models.Control model)
        {
            return false;
        }
        /// <summary>
        /// 分割线
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type22_SplitLine(Models.Control model)
        {
            if (!System.Enum.IsDefined(typeof(lineType), model.EnumDefault))
                return false;
            return true;
        }
        /// <summary>
        /// 地区（省市）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type23_District2(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            return true;
        }
        /// <summary>
        /// 地区（省市县）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool Type24_District3(Models.Control model)
        {
            if (!CheckControlName(model))
                return false;
            return true;
        }


        #endregion ControlType 类型验证 1:1验证
        #region 固定验证项

        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        private static bool CheckDefault(Models.Control model)
        {
            if (string.IsNullOrEmpty(model.ControlName))
                return false;
            if (string.IsNullOrEmpty(model.Hint))
                return false;
            return true;
        }

        private static bool CheckControlName(Models.Control model)
        {
            if (string.IsNullOrEmpty(model.ControlName))
                return false;
            return true;
        }
        #endregion 固定验证项
        private static void OptionsOrderBy(Models.Control model)
        {
            Dictionary<int, ControlOptions> dic = new Dictionary<int, ControlOptions>();
            model.Options.ForEach(o =>
            {
                dic.Add(Convert.ToInt32(o.Key), o);
            });
            model.Options = dic.OrderBy(o => o.Key).Select(o => o.Value).ToList();
        }
    }
}
