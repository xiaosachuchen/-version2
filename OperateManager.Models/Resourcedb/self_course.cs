using OperateManager.Models.ModelAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
   public  class self_course:self_object
    {
        [Ignore("", "")]
        [Key("","")]
        public int seqid { get; set; }
        public string title { get; set; }   
        public int teacher { get; set; }
        [Ignore("", "")]
        public string teachername { get; set; }
        public int coursetype { get; set; }
        public string thumbnail { get; set; }
        public decimal m_price { get; set; }
        [Ignore("", "")]
        public decimal s_prise { get; set; }
        public int grade { get; set; }
        public int subject { get; set; }
        public int version { get; set; }
        public int press { get; set; }
        public string abstracts { get; set; }
        [Ignore("", "")]
        public string time { get; set; }
        [Ignore("", "")]
        public int issue { get; set; }
        [Ignore("", "")]
        public string source { get; set; }
        public int hits { get; set; }
        public DateTime createdtime { get; set; }
        [Ignore("", "")]
        public int creatorid { get; set; }
        [Ignore("","")]
        public List<self_video> videos  { get; set; }
        [Ignore("", "")]
        public self_teacher self_teacher { get; set; }

    }
}
