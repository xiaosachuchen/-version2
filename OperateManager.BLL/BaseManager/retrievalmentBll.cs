using Microsoft.RIPSP.Model;
using OperateManager.DAL.BaseManager;
using OperateManager.Models.Resourcedb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.BLL.BaseManager
{
    public class retrievalmentBll
    {
        public static List<HotkeyLog> GetHotkeyLogList(string starttime,string endtime,string keyword,int offset,int limit,out int total, out string errmsg)
        {
            List<HotkeyLog> list = retrievalmentDAL.GetHotkeyLogList(starttime,endtime,keyword, offset, limit, out total, out errmsg);
            return list;
        }
        
    }
}
