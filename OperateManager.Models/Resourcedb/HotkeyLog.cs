using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
    public class HotkeyLog
    {
        public int seqid { get; set;}
        public string keyword { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public DateTime createdtime { get; set; }
    }
}
