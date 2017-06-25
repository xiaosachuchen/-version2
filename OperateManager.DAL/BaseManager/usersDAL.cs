using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using OperateManager.Models.Resourcedb;
using System.Text;

namespace Microsoft.RIPSP.DAL.BaseManager 
{
	 /// <summary>
	 /// 用户管理 users DAL 
	 /// </summary>
	 public static class usersDAL 
	 {
		 /// <summary>
		 /// 用户管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<base_users> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
			 List<DataParameter> pars = new List<DataParameter>(); 
			 string sqlcount = "select count(1) from base_users where 1=1 "; 
			 string sqlstr = "select * from base_users where 1=1 "; 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.value))
					 {
						 sqlcount = string.Format("{0} and {1} {2} @{1}", sqlcount, query.name, query.exp);
						sqlstr = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
				 }
			 }
			 sqlstr += " order by userid desc "; 
			 if (offset > -1)
			 {
				 sqlstr += " limit " + offset + "," + limit ;
				 total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<base_users>(sqlstr, out errmsg, pars.ToArray()); 
			 return new List<base_users>(); 
		 }
        /// <summary>
        /// 根据id数组查询名称
        /// </summary>
        /// <param name="queryarr"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_users> GetListInfo(List<string> queryarr, out string errmsg)
        {
            errmsg = null;
            string idstr = string.Join(",", queryarr);
            string sqlstr = "select userid,username from base_users where 1=1 and userid in(" + idstr + ")";
            return MySqlHelper.GetDataList<base_users>(sqlstr, out errmsg);
        }
        /// <summary>
        /// 用户管理 详情查询 
        /// </summary>
        public static base_users GetInfo(int userid, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from base_users  where userid=@userid "; 
			 pars.Add(new DataParameter("userid", userid)); 
			 return MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 用户管理 添加数据 
		 /// </summary>
		 /// <param name="base_users">要添加的用户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(base_users info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("username", info.username));
			 pars.Add(new DataParameter("passwd", info.passwd));
			 pars.Add(new DataParameter("nickname", info.nickname));
			 pars.Add(new DataParameter("realname", info.realname));
			 pars.Add(new DataParameter("usertype", info.usertype));
			 pars.Add(new DataParameter("userlevel", info.userlevel));
			 pars.Add(new DataParameter("balance", info.balance));
			 pars.Add(new DataParameter("score", info.score));
			 pars.Add(new DataParameter("photo", info.photo));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("sex", info.sex));
			 pars.Add(new DataParameter("birthday", info.birthday));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("sourcetype", info.sourcetype));
			 pars.Add(new DataParameter("sourceremarks", info.sourceremarks));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "insert into base_users (username,passwd,nickname,realname,usertype,userlevel,balance,score,photo,phone,email,country,areacode,sex,birthday,createdtime,sourcetype,sourceremarks,customerid,status) values (@username,@passwd,@nickname,@realname,@usertype,@userlevel,@balance,@score,@photo,@phone,@email,@country,@areacode,@sex,@birthday,@createdtime,@sourcetype,@sourceremarks,@customerid,@status) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户管理 更新数据 
		 /// </summary>
		 /// <param name="base_users">要更新的用户管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(base_users info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("username", info.username));
			 pars.Add(new DataParameter("passwd", info.passwd));
			 pars.Add(new DataParameter("nickname", info.nickname));
			 pars.Add(new DataParameter("realname", info.realname));
			 pars.Add(new DataParameter("usertype", info.usertype));
			 pars.Add(new DataParameter("userlevel", info.userlevel));
			 pars.Add(new DataParameter("balance", info.balance));
			 pars.Add(new DataParameter("score", info.score));
			 pars.Add(new DataParameter("photo", info.photo));
			 pars.Add(new DataParameter("phone", info.phone));
			 pars.Add(new DataParameter("email", info.email));
			 pars.Add(new DataParameter("country", info.country));
			 pars.Add(new DataParameter("areacode", info.areacode));
			 pars.Add(new DataParameter("sex", info.sex));
			 pars.Add(new DataParameter("birthday", info.birthday));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("sourcetype", info.sourcetype));
			 pars.Add(new DataParameter("sourceremarks", info.sourceremarks));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("status", info.status));
			 string sqlstr = "update base_users set username=@username,passwd=@passwd,nickname=@nickname,realname=@realname,usertype=@usertype,userlevel=@userlevel,balance=@balance,score=@score,photo=@photo,phone=@phone,email=@email,country=@country,areacode=@areacode,sex=@sex,birthday=@birthday,createdtime=@createdtime,sourcetype=@sourcetype,sourceremarks=@sourceremarks,customerid=@customerid,status=@status where userid=@userid "; 
			 pars.Add(new DataParameter("userid",info.userid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }
        public static bool UpdateCus(int? customerid,int? userid,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("customerid", customerid));
            pars.Add(new DataParameter("userid", userid));
            string sqlstr = "update base_users set customerid=@customerid where userid=@userid ";
            return MySqlHelper.ExecuteCommand(sqlstr,out errmsg,pars)>0;
          }
		 /// <summary>
		 /// 用户管理 删除 
		 /// </summary>
		 public static bool Delete(int userid,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from base_users where userid=@userid "; 
			 pars.Add(new DataParameter("userid", userid)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 用户管理 批量删除 
		 /// </summary>
		 public static bool BatchDelete(int[] idArray,out string errmsg) 
		 {
			 string idstr = string.Join(",", idArray);
			 string sqlstr = "delete from base_users where userid in ("+ idstr +") "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static base_users selectusername(string codes,string type, out string errmsg)
        {
            
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_users  where "+type+"='"+codes+"'";
            //pars.Add(new DataParameter(type, codes));
            return MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg);
        }
          /// <summary>
          /// 总用户数(个人）
          /// </summary>
          /// <param name="errmsg"></param>
          /// <returns></returns>
        public static int GetUsersCount(out string errmsg)
        {
            string sqlstr = "select count(*) from base_users where customerid=0";
            return (int)MySqlHelper.GetRecCount(sqlstr, out errmsg);
        }
        /// <summary>
        /// 指定时间段内用户（新注册用户）（个人）
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetNewUsersCount(string starttime,string endtime,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            sqlstr.Append("select count(*) from base_users where 1=1 and customerid=0 ");
            if(!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime>='@starttime' ");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime<='@endtime' ");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if (string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime>DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            return (int)MySqlHelper.GetRecCount(sqlstr.ToString(), out errmsg, pars.ToArray());

        }
        /// <summary>
        /// 指定时间段内用户（h活跃用户数）（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetActCount(string starttime, string endtime, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            sqlstr.Append(" SELECT COUNT(*) from (SELECT rescode,MAX(logid) as id,MAX(createdtime) as t,restype FROM base_userlogs GROUP BY rescode asc) as c where 1=1 and c.restype=0 ");
            if(!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t >='@starttime'");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if(!string.IsNullOrEmpty(endtime))
            {
                sqlstr.Append(" and c.t<='@endtime' ");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if(string.IsNullOrEmpty(endtime)|| string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t>DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            return (int)MySqlHelper.GetRecCount(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 用户流失人数（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserLoss(string starttime, string endtime, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            System.Text.StringBuilder sqluserids = new System.Text.StringBuilder();
            sqlstr.Append("SELECT b.userid from (SELECT * from (SELECT userid,MAX(createdtime) as t from chosen_orderinfo GROUP BY userid asc) as c WHERE 1=1 ");
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t >='@starttime'");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                sqlstr.Append(" and c.t<='@endtime' ");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if (string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t>DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            sqlstr.Append(") as b");
            List<base_users> list = MySqlHelper.GetDataList<base_users>(sqlstr.ToString(), out errmsg, pars.ToArray());
            sqluserids.Append("SELECT COUNT(*) from base_users where userid not in(");
            foreach (var item in list)
            {
                sqluserids.Append(item.userid + ",");
            }
            sqluserids.Remove(sqluserids.ToString().LastIndexOf(','), 1);
            sqluserids.Append(")");
            return (int)MySqlHelper.GetRecCount(sqluserids.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 指定时间内 新注册用户中的购买用户(用户留存数)（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserRete(string starttime, string endtime, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            sqlstr.Append("select userid from base_users where 1=1 ");
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime>='@starttime' ");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime<='@endtime' ");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if (string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime>DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            //新用户中的购买人数
            List<base_users> list = MySqlHelper.GetDataList<base_users>(sqlstr.ToString(), out errmsg, pars.ToArray());

            List<DataParameter> pars2 = new List<DataParameter>();
            System.Text.StringBuilder sqlstr2 = new System.Text.StringBuilder();
            sqlstr2.Append("select COUNT(*) from (select c.userid,MAX(createdtime) as t FROM (select * from chosen_orderinfo WHERE 1=1 ");
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr2.Append(" and createdtime>='@starttime' ");
                pars2.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime<='@endtime' ");
                pars2.Add(new DataParameter("endtime", endtime));
            }
            if (string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime > DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            sqlstr2.Append("and userid in(");
            foreach (var item in list)
            {
                sqlstr2.Append(item.userid + ",");
            }
            sqlstr2.Remove(sqlstr2.ToString().LastIndexOf(','), 1);
            sqlstr2.Append(")) as c GROUP BY c.createdtime asc) as b ");

            return (int)MySqlHelper.GetRecCount(sqlstr2.ToString(), out errmsg, pars2.ToArray());



        }
        /// <summary>
        /// 机构总用户
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetUserOrgCount(out string errmsg)
        {
            string sqlstr = "select count(*) from base_users where customerid > 0";
            return (int)MySqlHelper.GetRecCount(sqlstr, out errmsg);
        }
        /// <summary>
        /// 活跃用户（机构）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetOrgActCount(string starttime, string endtime, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            System.Text.StringBuilder sqlstr = new System.Text.StringBuilder();
            sqlstr.Append(" SELECT COUNT(*) from (SELECT rescode,MAX(logid) as id,MAX(createdtime) as t,restype FROM base_userlogs GROUP BY rescode asc) as c where 1=1 and c.restype!=0 ");
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t >='@starttime'");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                sqlstr.Append(" and c.t<='@endtime' ");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if (string.IsNullOrEmpty(endtime) || string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and c.t>DATE_SUB(CURDATE(), INTERVAL 3 MONTH)");
            }
            return (int)MySqlHelper.GetRecCount(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 机构用户
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stype"></param>
        /// <param name="areacode"></param>
        /// <param name="industry"></param>
        /// <param name="max_price"></param>
        /// <param name="min_price"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<OrgUsers> GetOrgUsersList(string starttime, string endtime, string stype, string areacode, string industry, string max_price, string min_price, int offset, int limit, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlstr = new StringBuilder();
            StringBuilder sqlcount = new StringBuilder();
            sqlstr.Append("SELECT * from(SELECT s.customerid,s.servicename,s.maxnum,c.areacode,c.industry,s.stype,s.starttime,s.endtime,ord.paytime,ord.m_price,c.customername from chosen_serviceinfo as s LEFT JOIN (SELECT * from chosen_orderinfo o ) as ord on ord.serviceno = s.serviceno and ord.customerid = s.customerid LEFT JOIN base_customers c ON c.customerid = s.customerid) as t where 1 = 1");
            sqlcount.Append("SELECT count(*) from(SELECT s.customerid,s.servicename,s.maxnum,c.areacode,c.industry,s.stype,s.starttime,s.endtime,ord.paytime,ord.m_price,c.customername from chosen_serviceinfo as s LEFT JOIN (SELECT * from chosen_orderinfo o ) as ord on ord.serviceno = s.serviceno and ord.customerid = s.customerid LEFT JOIN base_customers c ON c.customerid = s.customerid) as t where 1 = 1");
            if (!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and t.starttime>='@starttime'");
                sqlcount.Append(" and t.starttime>='@starttime'");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                sqlstr.Append(" and t.endtime>='@endtime'");
                sqlcount.Append(" and t.endtime>='@endtime'");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if (!string.IsNullOrEmpty(stype))
            {
                sqlstr.Append(" and t.stype>='@stype'");
                sqlcount.Append(" and t.stype>='@stype'");
                pars.Add(new DataParameter("stype", stype));
            }
            if (!string.IsNullOrEmpty(min_price))
            {
                sqlstr.Append(" and t.m_price>='@m_price'");
                sqlcount.Append(" and t.m_price>='@m_price'");
                pars.Add(new DataParameter("m_price", min_price));
            }
            if (!string.IsNullOrEmpty(max_price))
            {
                sqlstr.Append(" and t.m_price<='@m_price'");
                sqlcount.Append(" and t.m_price<='@m_price'");
                pars.Add(new DataParameter("m_price", max_price));
            }
            if (!string.IsNullOrEmpty(areacode))
            {
                sqlstr.AppendFormat(" and t.areacode like '%{0}%'", areacode);
                sqlcount.AppendFormat(" and t.areacode like '%{0}%'", areacode);
            }
            if (!string.IsNullOrEmpty(industry))
            {
                sqlstr.Append(" and t.industry='@industry'");
                sqlcount.Append(" and t.industry='@industry'");
                pars.Add(new DataParameter("industry", industry));
            }
            sqlstr.Append(" order by customerid desc ");
            if (offset > -1)
            {
                sqlstr.Append(" limit " + offset + "," + limit);
                total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray());
            }
            if (total > 0 || offset < 0)
                return MySqlHelper.GetDataList<OrgUsers>(sqlstr.ToString(), out errmsg, pars.ToArray());
            return new List<OrgUsers>();

        }
        /// <summary>
        /// 返回所有用户名
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_users> GetUsersListofName(out string errmsg)
        {
            string sql = "select userid,username from base_users";
            return MySqlHelper.GetDataList<base_users>(sql, out errmsg);
        }
    }
}

