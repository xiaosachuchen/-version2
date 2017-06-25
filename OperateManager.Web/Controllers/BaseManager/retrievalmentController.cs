using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;
using Microsoft.RIPSP.BaseController;
using OperateManager.Models.Resourcedb;
using OperateManager.BLL.BaseManager;

namespace OperateManager.Web.Controllers.BaseManager
{
    /// <summary>
    /// 检索管理
    /// </summary>
    public class retrievalmentController : BaseController
    {
        private string errmsg = null;
        //public object Get( bool WithNone=false)
        //{
        //    List<Options> list = shopinfBLL.GetShopInfoList(out errmsg);
        //    if (list == null)
        //    {
        //        list = new List<Options>();
        //    }
        //    if (WithNone)
        //    {
        //        Options options2 = new Options
        //        {
        //            id = "-1",
        //            text = "--请选择--"
        //        };
        //        list.Insert(0, options2);
        //    }

        //    return list;
        //}

        public object Get(int offset,int limit,string starttime,string endtime,string keyword)
        {
            int total;
            List<HotkeyLog> list = retrievalmentBll.GetHotkeyLogList(starttime,endtime,keyword,offset, limit,out total, out errmsg);
            List<base_users> userlist = usersBLL.GetUsersListofName(out errmsg);
            foreach (var item in list)
            {
                if(userlist.Find(a => a.userid == item.userid)==null)
                {
                    item.username = "";
                }else
                {
                    item.username = userlist.Find(a => a.userid == item.userid).username;
                }

            }
            return new { rows = list, total };
        }
    }
}