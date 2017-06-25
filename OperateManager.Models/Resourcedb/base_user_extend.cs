using Microsoft.RIPSP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
    public class base_user_extend: base_users
    {
        public int grade { get; set; }
        public int period { get; set; }
        public string classname { get; set; }
        public string school { get; set; }
    }
}
