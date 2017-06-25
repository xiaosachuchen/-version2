using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 机构管理 organization BLL 
	 /// </summary>
	 public static class organizationBLL 
	 {
		 /// <summary>
		 /// 机构管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_organization> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_organization> list = organizationDAL.GetPageList(queryarr,offset,limit,out total,out errmsg);
            return list;
		 }

		 /// <summary>
		 /// 机构管理 详情查询 
		 /// </summary>
		 public static base_organization GetInfo(int orgid, out string errmsg) 
		 {
			 base_organization info = organizationDAL.GetInfo(orgid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 机构管理 添加数据 
		 /// </summary>
		 /// <param name="base_organization">要添加的机构管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_organization info,out string errmsg) 
		 {
			 return organizationDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 机构管理 更新数据 
		 /// </summary>
		 /// <param name="base_organization">要更新的机构管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_organization info,out string errmsg) 
		 {
			 return organizationDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 机构管理 删除 
		 /// </summary>
		 public static bool Delete(int orgid,out string errmsg) 
		 {
			 return organizationDAL.Delete(orgid, out errmsg); 
		 }

		 /// <summary>
		 /// 机构管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return organizationDAL.BatchDelete(idArray, out errmsg);
		 }
        /// <summary>
        /// 检测机构重复
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_organization selectbyType(string codes, string type, out string errmsg)
        {
            return organizationDAL.selectbyType(codes, type, out errmsg);
        }
    }
}
