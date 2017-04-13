using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public class FormConfig
    {
        private FormConfig() { }
        private static FormConfig Instance;
        private List<FormConfigPart1> _config;
        public List<FormConfigPart1> Config { get { return _config; } }
        public static FormConfig GetIntance(string filePath)
        {
            Instance = new FormConfig();
            using (StreamReader sr = new StreamReader(filePath,Encoding.GetEncoding("gb2312")))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    //构建Json.net的读取流  
                    JsonReader reader = new JsonTextReader(sr);
                    //对读取出的Json.net的reader流进行反序列化，并装载到模型中  
                    Instance._config = serializer.Deserialize<List<FormConfigPart1>>(reader);
                }
                catch (Exception ex)
                {
                    throw new Exception("要按照基本法来，这边json的姿势很有问题");
                }
            }
            return Instance;
        }

    }
}
