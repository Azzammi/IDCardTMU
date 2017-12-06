using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCardTMUCSharp.Model
{
    public class cardModel
    {
        public string name { get; set; }
        public int nik { get; set; }
        public string companyName { get; set; }

        public Color nameForeColor { get; set; }
        public Color nikForeColor { get ; set; }
        public Color companyNameForeColor { get; set; }
        public Color listRulesForeColor { get; set; }
        public Color background { get; set; }
    }
}
