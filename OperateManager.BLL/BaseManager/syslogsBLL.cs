﻿using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.BaseManager; 

namespace Microsoft.RIPSP.BLL.BaseManager 
{
	 /// <summary>
	 /// 系统日志管理 syslogs BLL 
	 /// </summary>
	 public static class syslogsBLL 
	 {
		 /// <summary>
		 /// 系统日志管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_syslogs> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<base_syslogs> list = syslogsDAL.GetPageList(queryarr, offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 系统日志管理 详情查询 
		 /// </summary>
		 public static base_syslogs GetInfo(int logid, out string errmsg) 
		 {
			 base_syslogs info = syslogsDAL.GetInfo(logid, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 系统日志管理 添加数据 
		 /// </summary>
		 /// <param name="base_syslogs">要添加的系统日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_syslogs info,out string errmsg) 
		 {
			 return syslogsDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 系统日志管理 更新数据 
		 /// </summary>
		 /// <param name="base_syslogs">要更新的系统日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_syslogs info,out string errmsg) 
		 {
			 return syslogsDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 系统日志管理 删除 
		 /// </summary>
		 public static bool Delete(int logid,out string errmsg) 
		 {
			 return syslogsDAL.Delete(logid, out errmsg); 
		 }

		 /// <summary>
		 /// 系统日志管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 return syslogsDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
