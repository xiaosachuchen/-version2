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
    public class rolesController : BaseController.BaseController
    {
        

        /// <summary>
        /// 角色管理 列表查询 
        /// </summary>
        public object Get(int offset, int limit)
        {
            int total;
            List<QueryModel> queryarr = new List<QueryModel>();
            List<base_roles> list = rolesBLL.GetPageList(queryarr, offset, limit, out total, out errmsg);

            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }

            foreach (var item in list)
            {
                item.roletypename = BaseBLL.GlobalCommonBLL.GetDicName("RoleGroup", item.roletype.ToString());
            }

            return new { rows = list, total };
        }

        /// <summary>
        /// 角色管理 详情查询 
        /// </summary>
        public object Get(int id)
        {
            base_roles info = rolesBLL.GetInfo(id, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }
            return new { Rcode = 1, Rdata = info };
        }

        /// <summary>
        /// 角色管理 添加数据 
        /// </summary>
        public object Post([FromBody] base_roles info)
        {
            bool success = rolesBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加角色管理 {0},返回{1} {2}", info.rolename, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }

        /// <summary>
        /// 角色管理 更新数据 
        /// </summary>
        public object Put(int id, [FromBody] base_roles info)
        {
            info.roleid = id;
            bool success = rolesBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新字典管理 id为 {0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "更新失败" };
            return new { Rcode = 1, Rmsg = "更新成功" };
        }

        /// <summary>
        /// 角色管理 删除 
        /// </summary>
        public object Delete(int id)
        {
            bool success = rolesBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除角色管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

        /// <summary>
        /// 角色管理 批量删除 
        /// </summary>
        public object Delete(IdArrayModel IdArray)
        {
            if (IdArray == null)
            {
                return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
            }
            bool success = rolesBLL.BatchDelete(IdArray.IdArray, out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除角色管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "删除失败" };
            return new { Rcode = 1, Rmsg = "删除成功" };
        }

    }
}
