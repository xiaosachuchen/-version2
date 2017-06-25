using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Models
{
    public class EchartNode
    {
        public string name { get; set; }
        public string category { get; set; }
        public int value { get; set; }
        public string label { get; set; }
        public int symbolSize { get; set; }
    }
    public class EchartLink
    {
        public string name { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public int weight { get; set; }
    }
}