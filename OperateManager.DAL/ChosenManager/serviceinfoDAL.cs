using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model; 

namespace Microsoft.RIPSP.DAL.ChosenManager 
{
	 /// <summary>
	 /// 服务管理 serviceinfo DAL 
	 /// </summary>
	 public static class serviceinfoDAL 
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
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from chosen_serviceinfo where 1=1 and stype=@stype";
            //string sqlstr = "select o.*,customername from chosen_serviceinfo o,base_customers s where 1=1 and o.customerid=s.customerid"; 
            string sqlstr = "SELECT * FROM chosen_serviceinfo where 1=1 and stype=@stype";
            pars.Add(new DataParameter("stype", type));
            sqlstr += " order by serviceno desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<chosen_serviceinfo>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<chosen_serviceinfo>(); 
		 }

		 /// <summary>
		 /// 服务管理 详情查询 
		 /// </summary>
		 public static chosen_serviceinfo GetInfo(string serviceno, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from chosen_serviceinfo  where serviceno=@serviceno "; 
			 pars.Add(new DataParameter("serviceno", serviceno)); 
			 return MySqlHelper.GetDataInfo<chosen_serviceinfo>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 服务管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_serviceinfo">要添加的服务管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_serviceinfo info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("serviceno", info.serviceno));
             pars.Add(new DataParameter("servicename", info.servicename));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("stype", info.stype));
			 pars.Add(new DataParameter("starttime", info.starttime));
			 pars.Add(new DataParameter("endtime", info.endtime));
			 pars.Add(new DataParameter("maxnum", info.maxnum));
			 pars.Add(new DataParameter("leftnum", info.leftnum));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into chosen_serviceinfo (serviceno,servicename,customerid,stype,starttime,endtime,maxnum,leftnum,creator,createdtime,status) values (@serviceno,@servicename,@customerid,@stype,@starttime,@endtime,@maxnum,@leftnum,@creator,@createdtime,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_serviceinfo">要更新的服务管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_serviceinfo info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 //pars.Add(new DataParameter("serviceno", info.serviceno));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("stype", info.stype));
			 pars.Add(new DataParameter("starttime", info.starttime));
			 pars.Add(new DataParameter("endtime", info.endtime));
			 pars.Add(new DataParameter("maxnum", info.maxnum));
			 pars.Add(new DataParameter("leftnum", info.leftnum));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update chosen_serviceinfo set serviceno=@serviceno,customerid=@customerid,stype=@stype,starttime=@starttime,endtime=@endtime,maxnum=@maxnum,leftnum=@leftnum,creator=@creator,createdtime=@createdtime,status=@status where serviceno=@serviceno "; 
			 pars.Add(new DataParameter("serviceno",info.serviceno)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务管理 删除 
		 /// </summary>
		 public static bool Delete(string serviceno,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from chosen_serviceinfo where serviceno=@serviceno "; 
			 pars.Add(new DataParameter("serviceno", serviceno)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 服务管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(string[] idArray,out string errmsg) 
		 {
			 string idstr = "";
			 foreach (string s in idArray)
			 {
				 idstr += s + "','";
			 }
			 if (idstr.EndsWith("','"))
				 idstr = idstr.Substring(0, idstr.Length - 3);
			 string sqlstr = "delete from chosen_serviceinfo where serviceno in ('"+ idstr +"') "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

	 }
}
