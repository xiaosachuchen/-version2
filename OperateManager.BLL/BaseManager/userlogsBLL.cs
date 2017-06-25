using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 用户行为日志管理 userlogs BLL 
	 /// </summary>
	 public static class userlogsBLL 
	 {
		 /// <summary>
		 /// 用户行为日志管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_userlogs> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_userlogs> list = userlogsDAL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 用户行为日志管理 详情查询 
		 /// </summary>
		 public static base_userlogs GetInfo(int logid, out string errmsg) 
		 {
			 base_userlogs info = userlogsDAL.GetInfo(logid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 用户行为日志管理 添加数据 
		 /// </summary>
		 /// <param name="base_userlogs">要添加的用户行为日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_userlogs info,out string errmsg) 
		 {
			 return userlogsDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 用户行为日志管理 更新数据 
		 /// </summary>
		 /// <param name="base_userlogs">要更新的用户行为日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_userlogs info,out string errmsg) 
		 {
			 return userlogsDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 用户行为日志管理 删除 
		 /// </summary>
		 public static bool Delete(int logid,out string errmsg) 
		 {
			 return userlogsDAL.Delete(logid, out errmsg); 
		 }

		 /// <summary>
		 /// 用户行为日志管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return userlogsDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
