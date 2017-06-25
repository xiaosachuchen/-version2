using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 用户行为日志管理 userlogs DAL 
	 /// </summary>
	 public static class userlogsDAL 
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_userlogs where 1=1 "; 
			 string sqlstr = "select * from base_userlogs where 1=1 "; 
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
			 sqlstr += " order by logid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_userlogs>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_userlogs>(); 
		 }

		 /// <summary>
		 /// 用户行为日志管理 详情查询 
		 /// </summary>
		 public static base_userlogs GetInfo(int logid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_userlogs  where logid=@logid "; 
			 pars.Add(new DataParameter("logid", logid)); 
			 return MySqlHelper.GetDataInfo<base_userlogs>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 用户行为日志管理 添加数据 
		 /// </summary>
		 /// <param name="base_userlogs">要添加的用户行为日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_userlogs info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("logtype", info.logtype));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("logcontent", info.logcontent));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "insert into base_userlogs (logtype,restype,rescode,logcontent,creator,createdtime) values (@logtype,@restype,@rescode,@logcontent,@creator,@createdtime) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户行为日志管理 更新数据 
		 /// </summary>
		 /// <param name="base_userlogs">要更新的用户行为日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_userlogs info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("logtype", info.logtype));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rescode", info.rescode));
			 pars.Add(new DataParameter("logcontent", info.logcontent));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "update base_userlogs set logtype=@logtype,restype=@restype,rescode=@rescode,logcontent=@logcontent,creator=@creator,createdtime=@createdtime where logid=@logid "; 
			 pars.Add(new DataParameter("logid",info.logid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户行为日志管理 删除 
		 /// </summary>
		 public static bool Delete(int logid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_userlogs where logid=@logid "; 
			 pars.Add(new DataParameter("logid", logid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户行为日志管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_userlogs where logid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
