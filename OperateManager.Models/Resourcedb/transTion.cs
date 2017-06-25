using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model
{
   public class transTion
    {
        public string Username { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? PayTime { get; set; }
        public string PayRestype { get; set; }
        public string CountNum { get; set; }
    }
}
