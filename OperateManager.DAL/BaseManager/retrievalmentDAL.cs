using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperateManager.Models.Resourcedb;
using SqlHelperClass;
using Microsoft.RIPSP.Model;

namespace OperateManager.DAL.BaseManager
{
    public  class retrievalmentDAL
    {
        /// <summary>
        /// 检索管理
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<HotkeyLog> GetHotkeyLogList(string starttime,string endtime,string keyword,int offset,int limit,out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlstr = new StringBuilder();
            StringBuilder sqlcount = new StringBuilder();
            sqlstr.Append("select * from base_hotkeylog where 1=1 ");
            sqlcount.Append("select count(*) from base_hotkeylog where 1=1 ");
            if(!string.IsNullOrEmpty(starttime))
            {
                sqlstr.Append(" and createdtime>=@starttime");
                sqlcount.Append(" and createdtime>=@starttime");
                pars.Add(new DataParameter("starttime", starttime));
            }
            if(!string.IsNullOrEmpty(endtime))
            {
                sqlstr.Append(" and createdtime>=@endtime");
                sqlcount.Append(" and createdtime>=@endtime");
                pars.Add(new DataParameter("endtime", endtime));
            }
            if(!string.IsNullOrEmpty(keyword))
            {
                sqlstr.AppendFormat(" and keyword like '%{0}%'", keyword);
                sqlcount.AppendFormat(" and keyword like '%{0}%'", keyword);
            }
            sqlstr.Append(" order by seqid desc ");
            if (offset > -1)
            {
                sqlstr.Append(" limit " + offset + "," + limit);
                total = (int)MySqlHelper.GetRecCount(sqlcount.ToString(), out errmsg, pars.ToArray());
            }
            if (total > 0 || offset < 0)
                return MySqlHelper.GetDataList<HotkeyLog>(sqlstr.ToString(), out errmsg, pars.ToArray());
            return new List<HotkeyLog>();
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
        public static List<OrgUsers> GetOrgUsersList(string starttime, string endtime, string stype,string areacode,string industry,string max_price,string min_price,int offset, int limit, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlstr = new StringBuilder();
            StringBuilder sqlcount = new StringBuilder();
            sqlstr.Append("SELECT * from(SELECT s.customerid,s.servicename,c.areacode,c.industry,s.stype,s.starttime,s.endtime,ord.paytime,ord.m_price,c.customername from chosen_serviceinfo as s LEFT JOIN (SELECT * from chosen_orderinfo o ) as ord on ord.serviceno = s.serviceno and ord.customerid = s.customerid LEFT JOIN base_customers c ON c.customerid = s.customerid) as t where 1 = 1");
            sqlcount.Append("SELECT count(*) from(SELECT s.customerid,s.servicename,c.areacode,c.industry,s.stype,s.starttime,s.endtime,ord.paytime,ord.m_price,c.customername from chosen_serviceinfo as s LEFT JOIN (SELECT * from chosen_orderinfo o ) as ord on ord.serviceno = s.serviceno and ord.customerid = s.customerid LEFT JOIN base_customers c ON c.customerid = s.customerid) as t where 1 = 1");
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
            if(!string.IsNullOrEmpty(min_price))
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
    }
}
