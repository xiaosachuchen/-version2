using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 用户认证ip管理 user_ip BLL 
	 /// </summary>
	 public static class user_ipBLL 
	 {
		 /// <summary>
		 /// 用户认证ip管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_user_ip> GetPageList(int userid, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_user_ip> list = user_ipDAL.GetPageList(userid, offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 用户认证ip管理 详情查询 
		 /// </summary>
		 public static base_user_ip GetInfo(int seqid, out string errmsg) 
		 {
			 base_user_ip info = user_ipDAL.GetInfo(seqid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 用户认证ip管理 添加数据 
		 /// </summary>
		 /// <param name="base_user_ip">要添加的用户认证ip管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_user_ip info,out string errmsg) 
		 {
			 return user_ipDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 用户认证ip管理 更新数据 
		 /// </summary>
		 /// <param name="base_user_ip">要更新的用户认证ip管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_user_ip info,out string errmsg) 
		 {
			 return user_ipDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 用户认证ip管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 return user_ipDAL.Delete(seqid, out errmsg); 
		 }

		 /// <summary>
		 /// 用户认证ip管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return user_ipDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
