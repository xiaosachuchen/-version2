using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.ChosenManager 
{
	 /// <summary>
	 /// 服务内容表 servicecont DAL 
	 /// </summary>
	 public static class servicecontDAL 
	 {
		 /// <summary>
		 /// 服务内容表 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_servicecont> GetPageList(string serviceno, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from chosen_servicecont where 1=1 AND serviceno=@serviceno"; 
			 string sqlstr = "SELECT * FROM chosen_servicecont WHERE 1=1 AND serviceno=@serviceno";
            pars.Add(new DataParameter("serviceno", serviceno));
            sqlstr += " order by seqid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<chosen_servicecont>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<chosen_servicecont>(); 
		 }

		 /// <summary>
		 /// 服务内容表 详情查询 
		 /// </summary>
		 public static chosen_servicecont GetInfo(int seqid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from chosen_servicecont  where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.GetDataInfo<chosen_servicecont>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 服务内容表 添加数据 
		 /// </summary>
		 /// <param name="chosen_servicecont">要添加的服务内容表对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_servicecont info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("serviceno", info.serviceno));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rulename", info.rulename));
			 pars.Add(new DataParameter("rulevalue", info.rulevalue));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "insert into chosen_servicecont (serviceno,restype,rulename,rulevalue,creator,createdtime) values (@serviceno,@restype,@rulename,@rulevalue,@creator,@createdtime) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务内容表 更新数据 
		 /// </summary>
		 /// <param name="chosen_servicecont">要更新的服务内容表对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_servicecont info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("seqid", info.seqid));
			 pars.Add(new DataParameter("serviceno", info.serviceno));
			 pars.Add(new DataParameter("restype", info.restype));
			 pars.Add(new DataParameter("rulename", info.rulename));
			 pars.Add(new DataParameter("rulevalue", info.rulevalue));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 string sqlstr = "update chosen_servicecont set seqid=@seqid,serviceno=@serviceno,restype=@restype,rulename=@rulename,rulevalue=@rulevalue,creator=@creator,createdtime=@createdtime where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid",info.seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务内容表 删除 
		 /// </summary>
		 public static bool Delete(int seqid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from chosen_servicecont where seqid=@seqid "; 
			 pars.Add(new DataParameter("seqid", seqid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务内容表 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from chosen_servicecont where seqid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
