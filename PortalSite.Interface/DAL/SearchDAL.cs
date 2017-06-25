using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.RIPSP.Model;
using SqlHelperClass;
using System.Text;

namespace PortalSite.Interface.DAL
{
    public static class SearchDAL
    {
        internal static void UpdateHotKeyData(string[] keyarr, out string _errmsg)
        {
            throw new NotImplementedException();
        }

        internal static List<isee_services> GetIndexServerList(out string errmsg)
        {
            string sqlstr = "select servicecode,ip,port,databaseid as restype,mouldid from isee_services a left join db_datalibrarys b on a.servicecode=b.databasename ";
            return MySqlHelper.GetDataList<isee_services>(sqlstr, out errmsg);
        }

        internal static List<db_datalibrarys> GetResTypeList(out string errmsg)
        {
            string sqlstr = "select databaseid,databasename,databasecname,mouldid from db_datalibrarys ";
            return MySqlHelper.GetDataList<db_datalibrarys>(sqlstr, out errmsg);
        }
        internal static List<base_dics> GetDictList(string dicType, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select * from base_dics");
            if (dicType == null)
            {
                sqlstr.Append(" where isdictype=1");
            }
            else if (dicType != "")
            {
                sqlstr.Append(" where isdictype=0 and dictype=@dictype");
                pars.Add(new DataParameter("@dictype", dicType));
            }
            return MySqlHelper.GetDataList<base_dics>(sqlstr.ToString(), out errmsg, pars.ToArray());
        }
        public static List<base_classes> GetClassAll(out string errmsg)
        {
            string sqlstr = "select classid,classname,parentclassid from base_classes order by parentclassid asc";
            return MySqlHelper.GetDataList<base_classes>(sqlstr, out errmsg);
        }
        public static List<Options> GetAuthorsList(out string errmsg)
        {
            string sql = "select seqid id,title text from self_author";
            return MySqlHelper.GetDataList<Options>(sql, out errmsg);
        }
        public static List<base_classes> GetChildrenByParentid(int parentid,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sql = "select classid from base_classes where parentclassid=@parentclassid";
            pars.Add(new DataParameter("@parentclassid", parentid));
            return MySqlHelper.GetDataList<base_classes>(sql, out errmsg,pars.ToArray());
        }
    }
}