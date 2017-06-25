using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 管理员管理 managers BLL 
	 /// </summary>
	 public static class managersBLL 
	 {
		 /// <summary>
		 /// 管理员管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_managers> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_managers> list = managersDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 管理员管理 详情查询 
		 /// </summary>
		 public static base_managers GetInfo(int userid, out string errmsg) 
		 {
			 base_managers info = managersDAL.GetInfo(userid, out errmsg); 
			 return info;
		 }
         
		 /// <summary>
		 /// 管理员管理 添加数据 
		 /// </summary>
		 /// <param name="base_managers">要添加的管理员管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_managers info,out string errmsg) 
		 {
			 return managersDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 管理员管理 更新数据 
		 /// </summary>
		 /// <param name="base_managers">要更新的管理员管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_managers info,out string errmsg) 
		 {
			 return managersDAL.Update(info, out errmsg);
		 }
        /// <summary>
        /// 管理员管理 更新数据机构id
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        public static bool UpdateOrg(int? orgid,int? userid,out string errsmg)
        {
            return managersDAL.UpdateOrg(orgid,userid, out errsmg);
        }
         /// <summary>
         /// 管理员管理 删除 
         /// </summary>
        public static bool Delete(int userid,out string errmsg) 
		 {
			 return managersDAL.Delete(userid, out errmsg); 
		 }

		 /// <summary>
		 /// 管理员管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return managersDAL.BatchDelete(idArray, out errmsg);
		 }
        public static base_managers GetInfoVerify(string parameter, string type, out string errmsg)
        {
            return managersDAL.GetInfoVerify(parameter,type,out errmsg);
        }
        /// <summary>
        /// 查询配置资源
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<db_datalibrarys> GetDatalibraryslist(out string errmsg)
        {
            return managersDAL.GetDatalibraryslist(out errmsg);
        }
        /// <summary>
        /// 查询配置资源库
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_mgdb> Getmgdblist(int userid,out string errmsg)
        {
            return managersDAL.Getmgdblist(userid, out errmsg);
        }
        /// <summary>
        /// 管理员配置菜单编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddMgdbmenu(int id, int[] idArray, out string errmsg)
        {
            return managersDAL.AddMgdbmenu(id, idArray, out errmsg);
        }
        /// <summary>
        /// 查询角色配置资源
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_mgrole> Getmgdbrole(int userid, out string errmsg)
        {
            return managersDAL.Getmgdbrole(userid, out errmsg);
        }
        /// <summary>
        /// 管理员配置角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idArray"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool AddMgdbroles(int id, IdArrayModel ArrayModel, out string errmsg)
        {
            return managersDAL.AddMgdbroles(id, ArrayModel, out errmsg);
        }
    }
}
