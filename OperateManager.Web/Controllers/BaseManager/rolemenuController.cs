using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.BaseManager
{
    /// <summary>
    /// 角色管理 roles Controller 
    /// </summary>
    public class rolemenuController : BaseController.BaseController
    {
        private string errmsg = null;
        /// <summary>
        /// 角色管理配置菜单
        /// </summary>
        public object Put(int id,[FromBody]IdArrayModel IdArray)
        {
            bool success = rolesBLL.AddRoleMenu(id, IdArray.IdArray, out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(1, string.Format("更新角色管理配置菜单 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }
        

    }
}
