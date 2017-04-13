using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public class FormConfigPart1
    {
        public string TemplateTypeName { set; get; }
        public string TemplateTypeIcon { set; get; }
        public int TemplateTypeSort { set; get; }
        public List<FormConfigPart2> Templates { set; get; }
    }
}
