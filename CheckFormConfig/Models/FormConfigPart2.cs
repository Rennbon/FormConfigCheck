using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckFormConfig.Models
{
    public class FormConfigPart2
    {
        public string TemplateId { set; get; }
        public string TemplateName { set; get; }
        public int Sort { set; get; }
        public bool WhetherToUse { set; get; }
        public string Title { set; get; }
        public string Color { set; get; }
        public string Icon { set; get; }
        public List<FormConfigPart3> Stages { set; get; }
        public List<ReceiveControl> Controls { set; get; }
    }
}
