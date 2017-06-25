﻿using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.ChosenManager 
{
	 /// <summary>
	 /// 收藏管理 favorites DAL 
	 /// </summary>
	 public static class favoritesDAL 
	 {
		 /// <summary>
		 /// 收藏管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_favorites> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from chosen_favorites where 1=1 "; 
			 string sqlstr = "select * from chosen_favorites where 1=1 "; 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
						sqlstr = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
						if (query.exp == "like")
							pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
						else
							pars.Add(new DataParameter(query.name, query.value));
					}
				 }
			 }
			 sqlstr += " order by seqid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<chosen_favorites>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<chosen_favorites>(); 
		 }

		 /// <summary>
		 /// 收藏管理 详情查询 
		 /// </summary>
		 public static chosen_favorites GetInfo(int seqid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from chosen_favorites  where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.GetDataInfo<chosen_favorites>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 收藏管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_favorites">要添加的收藏管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_favorites info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("resremark", info.resremark));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "insert into chosen_favorites (userid,restype,rescode,resremark,createdtime) values (@userid,@restype,@rescode,@resremark,@createdtime) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 收藏管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_favorites">要更新的收藏管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_favorites info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("resremark", info.resremark));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "update chosen_favorites set userid=@userid,restype=@restype,rescode=@rescode,resremark=@resremark,createdtime=@createdtime where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid",info.seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 收藏管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from chosen_favorites where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 收藏管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from chosen_favorites where seqid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
