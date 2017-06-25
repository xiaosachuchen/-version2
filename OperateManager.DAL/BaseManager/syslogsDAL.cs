using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 系统日志管理 syslogs DAL 
	 /// </summary>
	 public static class syslogsDAL 
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();
            sqlcount.Append("select count(1) from base_syslogs where 1=1 ");
			sqlstr.Append("select * from base_syslogs where 1=1 "); 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.exp))
					 {
                        if(string.Equals(query.exp, "开始时间"))
                        {
                            sqlcount.Append(" and createdtime>=@startTime");
                            sqlstr.Append(" and createdtime>=@startTime");
                            pars.Add(new DataParameter("startTime", query.value));
                        }
                        else if(string.Equals(query.exp, "结束时间"))
                        {
                            sqlcount.Append(" and createdtime<@endTime");
                            sqlstr.Append(" and createdtime<@endTime");
                            pars.Add(new DataParameter("endTime", query.value));
                        }
                        else
                        {
                            sqlcount.AppendFormat(" and {0}=@{0}", query.name);
                            sqlstr.AppendFormat(" and {0}=@{0}", query.name);
                            pars.Add(new DataParameter(string.Format("{0}", query.name), query.value));
                        }
                    }
				 }
			 }
            sqlstr.Append(" order by logid desc ");
			 if (offset > -1)
			 {
				 sqlstr.Append(" limit " + offset + "," + limit);
				  total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_syslogs>(sqlstr.ToString(), out errmsg, pars.ToArray()); 
			 return new List<base_syslogs>(); 
		 }

		 /// <summary>
		 /// 系统日志管理 详情查询 
		 /// </summary>
		 public static base_syslogs GetInfo(int logid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_syslogs  where logid=@logid "; 
			 pars.Add(new DataParameter("logid", logid)); 
			 return MySqlHelper.GetDataInfo<base_syslogs>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 系统日志管理 添加数据 
		 /// </summary>
		 /// <param name="base_syslogs">要添加的系统日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_syslogs info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("logmenu", info.logmenu));
			 pars.Add(new DataParameter("logfunname", info.logfunname));
			 pars.Add(new DataParameter("logtype", info.logtype));
			 pars.Add(new DataParameter("logcontent", info.logcontent));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "insert into base_syslogs (logmenu,logfunname,logtype,logcontent,creator,createdtime) values (@logmenu,@logfunname,@logtype,@logcontent,@creator,@createdtime) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 系统日志管理 更新数据 
		 /// </summary>
		 /// <param name="base_syslogs">要更新的系统日志管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_syslogs info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("logmenu", info.logmenu));
			 pars.Add(new DataParameter("logfunname", info.logfunname));
			 pars.Add(new DataParameter("logtype", info.logtype));
			 pars.Add(new DataParameter("logcontent", info.logcontent));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "update base_syslogs set logmenu=@logmenu,logfunname=@logfunname,logtype=@logtype,logcontent=@logcontent,creator=@creator,createdtime=@createdtime where logid=@logid "; 
			 pars.Add(new DataParameter("logid",info.logid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 系统日志管理 删除 
		 /// </summary>
		 public static bool Delete(int logid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_syslogs where logid=@logid "; 
			 pars.Add(new DataParameter("logid", logid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 系统日志管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_syslogs where logid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
