using OperateManager.Models.ModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
    public class self_video:self_object
    {
        [Key("","")]
        [Ignore("","")]
        public int seqid { get; set; }
        public string title { get; set; }
        public int teacher { get; set; }
        public int courseid { get; set; }
        [Ignore("", "")]
        public string time { get; set; }
        public string thumbnail { get; set; }
        public decimal m_price { get; set; }
        [Ignore("","")]
        public decimal s_price { get; set; }
        public string fullpath { get; set; }
        public int grade { get; set; }
        public int subject { get; set; }
        public string version { get; set; }
        public int press { get; set; }
        public string abstracts { get; set; }
        public int isuse { get; set; }
        [Ignore("", "")]
        public string source { get; set; }
        public int hits { get; set; }
        public DateTime createdtime { get; set; }
        [Ignore("", "")]
        public int creatorid { get; set; }

        public int downloads { get; set; }
        [Ignore("", "")]
        public self_teacher teacherobject { get; set; }
        [Ignore("", "")]
        public self_course courseobject { get; set; }
    }
}
