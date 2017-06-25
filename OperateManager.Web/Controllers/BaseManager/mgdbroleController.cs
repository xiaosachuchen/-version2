using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;

namespace Microsoft.RIPSP.Controller.BaseManager
{
    /// <summary>
    /// 管理员管理配置角色
    /// </summary>
    public class mgdbroleController : BaseController.BaseController
    {
        private string errmsg = null;
        /// <summary>
        /// 管理员管理配置角色
        /// </summary>
        public object Put(int id,[FromBody]IdArrayModel ArrayModel)
        {
            bool success = managersBLL.AddMgdbroles(id, ArrayModel, out errmsg);
            string idstr = string.Join(",", ArrayModel.IdArray);
            WriteSysLog(1, string.Format("更新管理员管理配置角色 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(int id)
        {
            List<int> list = new List<int>();  
            List<base_mgrole> mgrole = managersBLL.Getmgdbrole(id, out errmsg);
            List<base_roles> data = rolesBLL.GetRoles();
            List<Options> oplist = new List<Options>();
            if((mgrole!=null)&&(mgrole.Count>0))
            {
                foreach (var item in mgrole)
                {
                    list.Add(item.roleid.Value);
                }
            }
            foreach (var item in data)
            {
                Options op = new Options
                {
                    id = item.roleid.ToString(),
                    text = item.rolename.ToString(),
                    selected = Array.IndexOf(list.ToArray(), item.roleid) >= 0
                };
                oplist.Add(op);
            }
            return new { result = oplist };
        }
    }
}
