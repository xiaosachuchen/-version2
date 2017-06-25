using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 角色管理 roles BLL 
	 /// </summary>
	 public static class rolesBLL 
	 {
		 /// <summary>
		 /// 角色管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_roles> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_roles> list = rolesDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 角色管理 详情查询 
		 /// </summary>
		 public static base_roles GetInfo(int roleid, out string errmsg) 
		 {
			 base_roles info = rolesDAL.GetInfo(roleid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 角色管理 添加数据 
		 /// </summary>
		 /// <param name="base_roles">要添加的角色管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_roles info,out string errmsg) 
		 {
			 return rolesDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 角色管理 更新数据 
		 /// </summary>
		 /// <param name="base_roles">要更新的角色管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_roles info,out string errmsg) 
		 {
			 return rolesDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 角色管理 删除 
		 /// </summary>
		 public static bool Delete(int roleid,out string errmsg) 
		 {
			 return rolesDAL.Delete(roleid, out errmsg); 
		 }

		 /// <summary>
		 /// 角色管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return rolesDAL.BatchDelete(idArray, out errmsg);
		 }
        /// <summary>
        /// 角色菜单配权管理 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddRoleMenu(int id, int[] idArray, out string errmsg)
        {
            return rolesDAL.AddRoleMenu(id, idArray, out errmsg);
        }
        /// <summary>
        /// 管理员配置角色获取角色
        /// </summary>
        /// <returns></returns>
        public static List<base_roles> GetRoles()
        {
            return rolesDAL.GetRoles();
        }
    }
}
