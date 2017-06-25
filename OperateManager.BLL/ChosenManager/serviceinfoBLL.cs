using System;
using System.Collections.Generic;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.DAL.ChosenManager; 

namespace Microsoft.RIPSP.BLL.ChosenManager 
{
	 /// <summary>
	 /// 服务管理 serviceinfo BLL 
	 /// </summary>
	 public static class serviceinfoBLL 
	 {
		 /// <summary>
		 /// 服务管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_serviceinfo> GetPageList(string type, int offset, int limit, out int total, out string errmsg) 
		 {
			 List<chosen_serviceinfo> list = serviceinfoDAL.GetPageList(type,offset,limit,out total,out errmsg); 
			 return list;
		 }

		 /// <summary>
		 /// 服务管理 详情查询 
		 /// </summary>
		 public static chosen_serviceinfo GetInfo(string serviceno, out string errmsg) 
		 {
			 chosen_serviceinfo info = serviceinfoDAL.GetInfo(serviceno, out errmsg); 
			 return info;
		 }

		 /// <summary>
		 /// 服务管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_serviceinfo">要添加的服务管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_serviceinfo info,out string errmsg) 
		 {
			 return serviceinfoDAL.Add(info, out errmsg);
		 }

		 /// <summary>
		 /// 服务管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_serviceinfo">要更新的服务管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_serviceinfo info,out string errmsg) 
		 {
			 return serviceinfoDAL.Update(info, out errmsg);
		 }

		 /// <summary>
		 /// 服务管理 删除 
		 /// </summary>
		 public static bool Delete(string serviceno,out string errmsg) 
		 {
			 return serviceinfoDAL.Delete(serviceno, out errmsg); 
		 }

		 /// <summary>
		 /// 服务管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(string[] idArray,out string errmsg) 
		 {
			 return serviceinfoDAL.BatchDelete(idArray, out errmsg);
		 }

	 }
}
