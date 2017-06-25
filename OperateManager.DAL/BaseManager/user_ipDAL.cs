using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 用户认证ip管理 user_ip DAL 
	 /// </summary>
	 public static class user_ipDAL 
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>();
            string sqlcount = "select count(1) from base_user_ip where 1=1 and userid=" + userid;
            string sqlstr = "select * from base_user_ip where 1=1 and userid=" + userid; 
			 sqlstr += " order by seqid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_user_ip>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_user_ip>(); 
		 }

		 /// <summary>
		 /// 用户认证ip管理 详情查询 
		 /// </summary>
		 public static base_user_ip GetInfo(int seqid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_user_ip  where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.GetDataInfo<base_user_ip>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 用户认证ip管理 添加数据 
		 /// </summary>
		 /// <param name="base_user_ip">要添加的用户认证ip管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_user_ip info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("ip_start", info.ip_start));
			 pars.Add(new DataParameter("ip_end", info.ip_end));
			 string sqlstr = "insert into base_user_ip (userid,ip_start,ip_end) values (@userid,@ip_start,@ip_end) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户认证ip管理 更新数据 
		 /// </summary>
		 /// <param name="base_user_ip">要更新的用户认证ip管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_user_ip info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("ip_start", info.ip_start));
			 pars.Add(new DataParameter("ip_end", info.ip_end));
			 string sqlstr = "update base_user_ip set userid=@userid,ip_start=@ip_start,ip_end=@ip_end where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid",info.seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户认证ip管理 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_user_ip where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户认证ip管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_user_ip where seqid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
