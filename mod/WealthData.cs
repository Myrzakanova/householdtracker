using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhealthDistributionSample
{
    public class WealthData
    {
        public string Date { get; set; }

        public double Value { get; set; }
        public double Bottom50 { get; set; }
        public double Next40 { get; set; }
        public double Top9 { get; set; }
        public double Top1 { get; set; }
        public double TopPoint1 { get; set; }

        public WealthData(string date, double value)
        {
            Date = date;
            Value = value;
        }
    }
}
