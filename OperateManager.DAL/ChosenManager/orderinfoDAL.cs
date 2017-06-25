using System;
using System.Collections.Generic;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using System.Text;
using System.Data;

namespace Microsoft.RIPSP.DAL.ChosenManager 
{
	 /// <summary>
	 /// 订单管理 orderinfo DAL 
	 /// </summary>
	 public static class orderinfoDAL 
	 {
		 /// <summary>
		 /// 订单管理 列表查询 
		 /// </summary>
		 /// <param name="queryarr">查询条件</param>
		 /// <param name="offset">起始条数</param>
		 /// <param name="limit">获取条数</param>
		 /// <param name="total">返回总记录数</param>
		 public static List<chosen_orderinfo> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg) 
		 {
			 errmsg = null;
			 total = 0;
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();
            List<DataParameter> pars = new List<DataParameter>(); 
			 sqlcount.Append("select count(1) from chosen_orderinfo o where 1=1 ");
			 sqlstr.Append ("select o.*,customername,servicename from chosen_orderinfo o ,base_customers s,chosen_serviceinfo sf  where 1=1 and o.serviceno=sf.serviceno and o.customerid=s.customerid "); 
			 if (queryarr.Count > 0)
			 {
				 foreach (QueryModel query in queryarr)
				 {
					 if (!string.IsNullOrEmpty(query.exp))
					 {
                        if (string.Equals(query.exp, "服务编号"))
                        {
                            sqlcount.Append(" and o.serviceno=@serviceno");
                            sqlstr.Append(" and o.serviceno=@serviceno");
                            pars.Add(new DataParameter("serviceno", query.value));
                        }
                        else if (string.Equals(query.exp, "客户编号"))
                        {
                            sqlcount.Append(" and o.customerid=@customerid");
                            sqlstr.Append(" and o.customerid=@customerid");
                            pars.Add(new DataParameter("customerid", query.value));
                        }
                        else if(string.Equals(query.exp, "状态"))
                        {
                            sqlcount.Append(" and o.status=@status");
                            sqlstr.Append(" and o.status=@status");
                            pars.Add(new DataParameter("status", query.value));
                        }
                        
					 }
				 }
			 }
			 sqlstr.Append(" order by orderno desc "); 
			 if (offset > -1)
			 {
				 sqlstr.Append( " limit " + offset + "," + limit);
				 total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray()); 
			 }
			 if(total > 0 || offset<0 )
				 return MySqlHelper.GetDataList<chosen_orderinfo>(sqlstr.ToString(), out errmsg, pars.ToArray()); 
			 return new List<chosen_orderinfo>(); 
		 }

		 /// <summary>
		 /// 订单管理 详情查询 
		 /// </summary>
		 public static chosen_orderinfo GetInfo(string orderno, out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "select * from chosen_orderinfo  where orderno=@orderno "; 
			 pars.Add(new DataParameter("orderno", orderno)); 
			 return MySqlHelper.GetDataInfo<chosen_orderinfo>(sqlstr, out errmsg,pars.ToArray()); 
		 }

		 /// <summary>
		 /// 订单管理 添加数据 
		 /// </summary>
		 /// <param name="chosen_orderinfo">要添加的订单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Add(chosen_orderinfo info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 pars.Add(new DataParameter("orderno", info.orderno));
			 pars.Add(new DataParameter("ordername", info.ordername));
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("serviceno", info.serviceno));
			 pars.Add(new DataParameter("feetype", info.feetype));
			 pars.Add(new DataParameter("m_price", info.m_price));
			 pars.Add(new DataParameter("s_price", info.s_price));
			 pars.Add(new DataParameter("addressid", info.addressid));
			 pars.Add(new DataParameter("ctype", info.ctype));
			 pars.Add(new DataParameter("paytype", info.paytype));
			 pars.Add(new DataParameter("paybank", info.paybank));
			 pars.Add(new DataParameter("payno", info.payno));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("paytime", info.paytime));
			 pars.Add(new DataParameter("payresulttime", info.payresulttime));
			 pars.Add(new DataParameter("deliverytime", info.deliverytime));
			 pars.Add(new DataParameter("deliveryoktime", info.deliveryoktime));
			 pars.Add(new DataParameter("logisticaltype", info.logisticaltype));
			 pars.Add(new DataParameter("logisticalcode", info.logisticalcode));
			 pars.Add(new DataParameter("status", info.status));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("terminaltype", info.terminaltype));
			 string sqlstr = "insert into chosen_orderinfo (orderno,ordername,userid,customerid,serviceno,feetype,m_price,s_price,addressid,ctype,paytype,paybank,payno,createdtime,paytime,payresulttime,deliverytime,deliveryoktime,logisticaltype,logisticalcode,status,creator,terminaltype) values (@orderno,@ordername,@userid,@customerid,@serviceno,@feetype,@m_price,@s_price,@addressid,@ctype,@paytype,@paybank,@payno,@createdtime,@paytime,@payresulttime,@deliverytime,@deliveryoktime,@logisticaltype,@logisticalcode,@status,@creator,@terminaltype) "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单管理 更新数据 
		 /// </summary>
		 /// <param name="chosen_orderinfo">要更新的订单管理对象</param> 
		 /// <param name="errmsg">错误信息</param>
		 /// <returns>返回对象</returns>
		 public static bool Update(chosen_orderinfo info,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 //pars.Add(new DataParameter("orderno", info.orderno));
			 pars.Add(new DataParameter("ordername", info.ordername));
			 pars.Add(new DataParameter("userid", info.userid));
			 pars.Add(new DataParameter("customerid", info.customerid));
			 pars.Add(new DataParameter("serviceno", info.serviceno));
			 pars.Add(new DataParameter("feetype", info.feetype));
			 pars.Add(new DataParameter("m_price", info.m_price));
			 pars.Add(new DataParameter("s_price", info.s_price));
			 pars.Add(new DataParameter("addressid", info.addressid));
			 pars.Add(new DataParameter("ctype", info.ctype));
			 pars.Add(new DataParameter("paytype", info.paytype));
			 pars.Add(new DataParameter("paybank", info.paybank));
			 pars.Add(new DataParameter("payno", info.payno));
			 pars.Add(new DataParameter("createdtime", info.createdtime));
			 pars.Add(new DataParameter("paytime", info.paytime));
			 pars.Add(new DataParameter("payresulttime", info.payresulttime));
			 pars.Add(new DataParameter("deliverytime", info.deliverytime));
			 pars.Add(new DataParameter("deliveryoktime", info.deliveryoktime));
			 pars.Add(new DataParameter("logisticaltype", info.logisticaltype));
			 pars.Add(new DataParameter("logisticalcode", info.logisticalcode));
			 pars.Add(new DataParameter("status", info.status));
			 pars.Add(new DataParameter("creator", info.creator));
			 pars.Add(new DataParameter("terminaltype", info.terminaltype));
			 string sqlstr = "update chosen_orderinfo set orderno=@orderno,ordername=@ordername,userid=@userid,customerid=@customerid,serviceno=@serviceno,feetype=@feetype,m_price=@m_price,s_price=@s_price,addressid=@addressid,ctype=@ctype,paytype=@paytype,paybank=@paybank,payno=@payno,createdtime=@createdtime,paytime=@paytime,payresulttime=@payresulttime,deliverytime=@deliverytime,deliveryoktime=@deliveryoktime,logisticaltype=@logisticaltype,logisticalcode=@logisticalcode,status=@status,creator=@creator,terminaltype=@terminaltype where orderno=@orderno "; 
			 pars.Add(new DataParameter("orderno",info.orderno)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单管理 删除 
		 /// </summary>
		 public static bool Delete(string orderno,out string errmsg) 
		 {
			 List<DataParameter> pars = new List<DataParameter>();
			 string sqlstr = "delete from chosen_orderinfo where orderno=@orderno "; 
			 pars.Add(new DataParameter("orderno", orderno)); 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
		 }

		 /// <summary>
		 /// 订单管理 批量删除 
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
			 string sqlstr = "delete from chosen_orderinfo where orderno in ('"+ idstr +"') "; 
			 return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
		 }
        #region
        /// <summary>
        /// 交易管理 机构
        /// </summary>
        public static DataTable GetInfoEchar_org(string timetype,string starDate,string endDate,out string errmsg)
        {
            string sqlstr = "";
            //年
            if (timetype == "1")
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid>0 and p.orderno=o.orderno) a GROUP BY a.times", starDate,endDate);
            }
            //月
            else if (timetype == "2")
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid>0 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            //日
            else if (timetype == "0") {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m-%d') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid>0 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            //日
            else
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m-%d') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid>0 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 交易管理 个人
        /// </summary>
        public static DataTable GetInfoEchar_user(string timetype, string starDate, string endDate, out string errmsg)
        {
            string sqlstr = "";
            //年
            if (timetype == "1")
            {
   
                   sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid<1 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            //月
            else if (timetype == "2")
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid<1 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            //周
            else if (timetype == "3")
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m-%d') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid<1 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            //日
            else
            {
                sqlstr = string.Format("SELECT a.times,sum(nums)as nums FROM (SELECT  o.orderno,DATE_FORMAT(o.createdtime,'%Y-%m-%d') times,p.prices * p.shopnum as nums from chosen_orderinfo o,chosen_ordershop p where o.createdtime>='{0}' and createdtime<='{1}' and customerid<1 and p.orderno=o.orderno) a GROUP BY a.times", starDate, endDate);
            }
            return MySqlHelper.GetDataTable(sqlstr,out errmsg);
        }
        #endregion
        //        SELECT u.userid, u.username, u.createdtime, b.paytime, p.nums, p.shopname from base_users u left JOIN(
        //SELECT MIN(paytime)as paytime,userid,orderno FROM chosen_orderinfo c GROUP BY c.userid) as b LEFT JOIN
        //(SELECT prices*shopnum as nums, shopname, orderno FROM chosen_ordershop p)as p ON b.orderno=p.orderno
        //  ON u.userid=b.userid

        #region 潜在客户转化率
        public static DataTable GetInfoFractional(out string errmsg)
        {
        
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT u.userid, u.username, u.createdtime, b.paytime, p.nums, p.shopname from base_users u left JOIN(");
            sqlstr.Append(" SELECT MIN(paytime)as paytime,userid,orderno FROM chosen_orderinfo c GROUP BY c.userid) as b LEFT JOIN");
            sqlstr.Append(" (SELECT prices*shopnum as nums, shopname, orderno FROM chosen_ordershop p)as p ON b.orderno=p.orderno");
            sqlstr.Append(" ON u.userid=b.userid");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        public static List<transTion> GetFracList(int offset, int limit, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            StringBuilder sqlcount = new StringBuilder();
            StringBuilder sqlstr = new StringBuilder();
            sqlcount.Append("SELECT COUNT(1) FROM base_users");
 
            sqlstr.Append("SELECT u.userid, u.username, u.createdtime, b.paytime, p.nums as countnum, p.shopname as payrestype from base_users u left JOIN(");
            sqlstr.Append(" SELECT MIN(paytime)as paytime,userid,orderno FROM chosen_orderinfo c GROUP BY c.userid) as b LEFT JOIN");
            sqlstr.Append(" (SELECT prices*shopnum as nums, shopname, orderno FROM chosen_ordershop p)as p ON b.orderno=p.orderno");
            sqlstr.Append(" ON u.userid=b.userid");
            sqlstr.Append(" order by countnum desc ");
            if (offset > -1)
            {
                sqlstr.Append(" limit " + offset + "," + limit);
                total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg);
            }
           if (total > 0 || offset < 0)
            return MySqlHelper.GetDataList<transTion>(sqlstr.ToString(), out errmsg);
           return new List<transTion>();
        }
        #endregion

        #region 资源管理  横坐标数据 收藏量
        public static DataTable GetResFractionalX(out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT f.restype as restype,d.databasecname as resname FROM chosen_favorites f,db_datalibrarys d where ");
            sqlstr.Append("   d.databaseid = f.restype GROUP BY f.restype");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 浏览量
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetPageViewX(out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT f.restype as restype,d.databasecname as resname FROM base_userlogs f,db_datalibrarys d where ");
            sqlstr.Append("   d.databaseid = f.restype and  f.logtype=2 GROUP BY f.restype");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 分享量
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetSharX(out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT f.restype as restype,d.databasecname as resname FROM base_userlogs f,db_datalibrarys d where ");
            sqlstr.Append("   d.databaseid = f.restype and  f.logtype=5 GROUP BY f.restype");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 购买量
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetPurchX(out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT f.restype as restype,d.databasecname as resname FROM chosen_ordershop f,db_datalibrarys d where ");
            sqlstr.Append("   d.databaseid = f.restype  GROUP BY f.restype");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        #endregion
        #region 资源管理  纵坐标数据 收藏量
        public static DataTable GetResFractionalY(string tablename,int restype,string classid,string starTime,string endTime,out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT count(f.rescode) as num,f.resremark as resname from chosen_favorites f  INNER JOIN  ");
            sqlstr.Append("  (SELECT seqid from  "+tablename+" j where classid in ("+classid+")) a");
            sqlstr.Append("  ON a.seqid=f.rescode  where f.createdtime>='"+starTime+"' and f.createdtime<='"+endTime+ "' and f.restype="+ restype + " GROUP BY f.rescode");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 浏览量
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="restype"></param>
        /// <param name="classid"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetPageViewY(string tablename, int restype, string classid, string starTime, string endTime, out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT count(f.rescode) as num,a.title as resname  from base_userlogs f INNER JOIN  ");
            sqlstr.Append("  (SELECT seqid,title from  " + tablename + " j where classid in (" + classid + ")) a");
            sqlstr.Append("  ON a.seqid=f.rescode  where f.createdtime>='" + starTime + "' and f.createdtime<='" + endTime + "' and f.restype=" + restype + " and  f.logtype=2 GROUP BY f.rescode");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 分享量
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="restype"></param>
        /// <param name="classid"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetSharY(string tablename, int restype, string classid, string starTime, string endTime, out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT count(f.rescode) as num,a.title as resname from base_userlogs f  INNER JOIN  ");
            sqlstr.Append("  (SELECT seqid,title from  " + tablename + " j where classid in (" + classid + ")) a");
            sqlstr.Append("  ON a.seqid=f.rescode  where f.createdtime>='" + starTime + "' and f.createdtime<='" + endTime + "' and f.restype=" + restype + " and  f.logtype=5 GROUP BY f.rescode");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        /// <summary>
        /// 购买量
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="restype"></param>
        /// <param name="classid"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static DataTable GetPurchY(string tablename, int restype, string classid, string starTime, string endTime, out string errmsg)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT count(f.rescode) as num,a.title as resname from chosen_ordershop f INNER JOIN  ");
            sqlstr.Append("  (SELECT seqid,title from  " + tablename + " j where classid in (" + classid + ")) a");
            sqlstr.Append("  ON a.seqid=f.rescode  where f.createdtime>='" + starTime + "' and f.createdtime<='" + endTime + "' and f.restype=" + restype + " GROUP BY f.rescode");
            return MySqlHelper.GetDataTable(sqlstr.ToString(), out errmsg);
        }
        public static List<base_classes> GetChildrenByParentid(int parentid, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sql = "select classid from base_classes where parentclassid=@parentclassid or classid=@parentclassid";
            pars.Add(new DataParameter("@parentclassid", parentid));
            return MySqlHelper.GetDataList<base_classes>(sql, out errmsg, pars.ToArray());
        }
        public static List<db_datalibrarys> GetResTypeList(out string errmsg)
        {
            string sqlstr = "select databaseid,databasename,databasecname,mouldid from db_datalibrarys ";
            return MySqlHelper.GetDataList<db_datalibrarys>(sqlstr, out errmsg);
        }
        #endregion
    }
}
