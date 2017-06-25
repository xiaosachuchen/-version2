using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.BaseManager
{
    /// <summary>
    /// 管理员管理配置
    /// </summary>
    public class mgdbmenuController : BaseController.BaseController
    {
        private string errmsg = null;
        /// <summary>
        /// 管理员管理配置菜单
        /// </summary>
        public object Put(int id,[FromBody]IdArrayModel IdArray)
        {
            bool success = managersBLL.AddMgdbmenu(id, IdArray.IdArray, out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(1, string.Format("更新管理员管理配置菜单 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }
        

    }
}
