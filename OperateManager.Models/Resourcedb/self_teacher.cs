using OperateManager.Models.ModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
    public  class self_teacher:self_object
    {
        [Key("", "")]
        [Ignore("", "")]
        public int seqid { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public string grade { get; set; }
        public string subject { get; set; }
        public string abstracts { get; set; }
        [Ignore("", "")]
        public string source { get; set; }
        public int hits { get; set; }
        public DateTime createdtime { get; set; }
        [Ignore("", "")]
        public int creatorid { get; set; }
        public int downloads { get; set; }
        public string keywords { get; set; }
    }
}
