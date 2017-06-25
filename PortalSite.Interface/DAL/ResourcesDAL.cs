using PortalSite.Interface.Models;
using SqlHelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.DAL
{
    public static class ResourcesDAL
    {
        /// <summary>
        /// 获取栏目资源详情
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfo(string tablename, int seqid, out string errmsg)
        {
            string sqlstr = "";
            if (tablename == "self_organization")
            {
                sqlstr = string.Format("select o.*,b.dicname as countryname from {0} o,base_dics b where o.seqid={1} and o.country=b.dicvalue", tablename, seqid);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where seqid={1}", tablename, seqid);
            } return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 获取字典值
        /// </summary>
        /// <param name="dictype"></param>
        /// <param name="dicvalue"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetDicName(string dictype, string dicvalue, out string errmsg)
        {
            string  sqlstr = string.Format("select * from base_dics where dictype='{0}' and dicvalue='{1}'", dictype, dicvalue);
            
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResTypeList(out string errmsg)
        {
            string sqlstr = string.Format("SELECT * FROM db_datalibrarys");

            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        
        /// <summary>
        /// 获取全文路径（在线阅读或下载）
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="pageid"></param>
        /// <param name="columnname"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static BookInfo GetFilePath(string tablename, long seqid, long pageid, string columnname, out string errmsg)
        {
            string sqlstr = string.Format("select {0} as bookpath from {1} where seqid={2}", columnname, tablename, seqid);
            BookInfo bookinfo = MySqlHelper.GetDataInfo<BookInfo>(sqlstr, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                errmsg = "获取资源出错" + errmsg;
                return null;
            }
            if (bookinfo == null)
            {
                errmsg = "资源不存在";
                return null;
            }
            if (pageid > 0) //查询当前资源的具体页码
            {
                sqlstr = null;
                switch (tablename)
                {
                    case "self_book":
                        sqlstr = string.Format("select startpage,endpage from self_chapter where booknum={0} and seqid={1} ", seqid, pageid);
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(sqlstr))
                {
                    DataTable dt = MySqlHelper.GetDataTable(sqlstr, out errmsg);
                    if (string.IsNullOrEmpty(errmsg) && dt != null && dt.Rows.Count > 0)
                    {
                        int startpage;
                        if (int.TryParse(dt.Rows[0][0].ToString(), out startpage))
                            bookinfo.CurrentPage = startpage;
                    }
                }
            }
            return bookinfo;
        }

        #region
        /// <summary>
        /// 根据IDlist 查询数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyids(string tablename, string seqid, int offset, int rows, out string errmsg)
        {
            string sqlstr;
            if (tablename == "self_journalarticle")
            {
                sqlstr = string.Format("select j.*,a.title as authorname from {0} j,self_author a where j.seqid in({1}) and j.author=a.seqid limit {2},{3}", tablename, seqid, offset, rows);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where seqid in({1}) limit {2},{3}", tablename, seqid, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 根据ids 获取所有ID的信息。
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfobyids(string tablename, string seqid, out string errmsg)
        {
            string sqlstr = "";
            if (tablename == "self_author") {
                sqlstr = string.Format("select * from {0} where seqid in({1})", tablename, seqid);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where seqid in({1}) and pubdate!='0000-00-00'", tablename, seqid);
            }
        return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion

        #region
        /// <summary>
        /// 查询研究课题-title-childrentitle
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyids_group(string tablename, string seqid, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where seqid in({1}) group BY title ", tablename, seqid);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 查询研究课题 根据title 查询对应的childrentitle
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="title"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbytitle_group(string tablename, string title, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where title ='{1}' ", tablename, title);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion
        /// <summary>
        /// 根据classid获取数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyclassid(string tablename, int seqid, string classsid, int rows, int offset, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where 1=1 and seqid!={1} and classid='{2}' order by {3} limit {4},{5}", tablename, seqid, classsid, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 根据orgid获取数据
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyorgid(string tablename, int orgid, int rows, int offset, string sortexp, out string errmsg)
        {
            string sqlstr = "";
            if (tablename == "self_degreethesis")
            {
                sqlstr = string.Format("select s.*,a.title as authorname from {0} s,self_author a where 1=1 and s.author=a.seqid and s.orgid={1} order by s.{2} limit {3},{4}", tablename, orgid, sortexp, offset, rows);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where 1=1 and orgid={1} order by {2} limit {3},{4}", tablename, orgid, sortexp, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        internal static DataTable GetResourceInfos(string tablename, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            string sqlcount;
            int offset = 0;
            total = 0;
            string sqlstr = "";
            if (page > 0)
            {
                offset = (page - 1) * rows;
                if (tablename == "self_report")
                {
                    sqlcount = string.Format("select count(1) from {0} r ,self_author a,self_organization o  where 1=1 and r.author=a.seqid  and r.orgid=o.seqid ", tablename);
                }
                else if (tablename == "self_policies")
                {
                    sqlcount = string.Format("select count(1) from {0} ", tablename);
                }
                else if (tablename == "self_policyinterpretation")
                {
                    sqlcount = string.Format("select count(1) from {0}", tablename);
                }
                else if (tablename == "self_organization") {
                    sqlcount = string.Format("select count(1) from {0} where 1=1 and country!=''", tablename);
                }
                else
                {
                    sqlcount = string.Format("select count(1) from {0}", tablename);
                }
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
            //政策法规
            if (tablename == "self_policies")
            {
                sqlstr = string.Format("select *,'self_policies' as datanum from {0} where 1=1 and pubdate!='0000-00-00' and writedate!='0000-00-00' and impledate!='0000-00-00'  order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
            }
            else if (tablename == "self_policyinterpretation")
            {
                sqlstr = string.Format("select *,'self_policyinterpretation' as datanum from {0} where 1=1 and pubdate!='0000-00-00' order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
            }
            else if (tablename == "self_report")
            {

                sqlstr = string.Format("select r.*,a.title as authorname,o.title as orgname from {0} r,self_author a,self_organization o  where 1=1 and r.author=a.seqid  and r.orgid=o.seqid and r.pubdate!='0000-00-00' order by r.{1} limit {2},{3}", tablename, sortexp, offset, rows);
            }
            else if (tablename == "self_organization") {
                sqlstr = string.Format("select * from {0} where 1=1 and country!=''  order by {1} limit {2},{3}", tablename, sortexp, offset, rows);

            }
            else
            {
                sqlstr = string.Format("select * from {0} where 1=1  order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #region 专家
        /// <summary>
        /// 获取国内/海外专家
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="tablename"></param>
        /// <param name="rows"></param>
        /// <param name="sortexp"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceexpert(int classid, string tablename, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            int offset = 0;
            total = 0;
            string sqlstr;
            string sqlcount;
            if (page > 0)
            {
                offset = (page - 1) * rows;
                if (tablename == "self_expert")
                {
                    if (classid == 1)
                    {
                        sqlcount = string.Format("select count(1) from {0}  where 1=1 and country !='CN' and birthday!='0000-00-00' ", tablename);
                    }
                    else
                    {
                        sqlcount = string.Format("select count(1) from {0}  where 1=1 and country ='CN' and birthday!='0000-00-00' ", tablename);
                    }
                }
                else if (tablename == "self_specialsubject")
                {
                    sqlcount = string.Format("select count(1) from {0} s,self_organization o  where 1=1 and s.orgid=o.seqid and s.classid={1} and s.pubdate!='0000-00-00'", tablename, classid);
                }
                else
                {
                    sqlcount = string.Format("select count(1) from {0}  where 1=1", tablename);
                }
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
            if (tablename == "self_expert")
            {

                if (classid == 1)
                {
                    sqlstr = string.Format("select * from {0} where 1=1 and country !='CN' and birthday!='0000-00-00' order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
                }
                else
                {
                    sqlstr = string.Format("select * from {0} where 1=1 and country='CN' and birthday!='0000-00-00' order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
                }
            }
            else if (tablename == "self_specialsubject")
            {
                sqlstr = string.Format("select s.*,o.title as orgname  from {0} s,self_organization o where 1=1 and s.orgid=o.seqid and s.classid={1} and s.pubdate!='0000-00-00' order by {2} limit {3},{4}", tablename, classid, sortexp, offset, rows);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where 1=1 and classid={1} order by {2} limit {3},{4}", tablename, classid, sortexp, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }

        internal static DataTable GetResourceEptList(string keywords, int country, int research, int experttype, int orgid,int rows, string sortexp, int page, out int total, out string errmsg)
        {
            int offset = 0;
            total = 0;
            string sqlstr;
            string sqlcount;
            string sqlAdd="";
            string sqlexp = " order by ";
            List<DataParameter> pars = new List<DataParameter>();
            //pars.Add(new DataParameter("@tablename", tablename));
            pars.Add(new DataParameter("@sortexp", sortexp));
            sqlexp += sortexp;
            offset = (page - 1) * rows;
            pars.Add(new DataParameter("@offse", offset));
            sqlexp += " limit @offse,@rows";
            pars.Add(new DataParameter("@rows", rows));

            //order by @sortexp limit @offset,@rows
            if (research!=0) {
                sqlAdd += "and research in (@research)";
                pars.Add(new DataParameter("@research", research));
            }
            if (experttype!=0)
            {
                sqlAdd += " and experttype=@experttype";
                pars.Add(new DataParameter("@experttype", experttype));
            }
            if (orgid!=0)
            {
                sqlAdd += " and orgid=@orgid";
                pars.Add(new DataParameter("@orgid", orgid));
            }
 
            if (page > 0)
            {
               
               
                    if (country == 1)
                    {
                        sqlcount = "select count(1) from self_expert where 1=1 and country !='CN' and title like '%" + keywords + "%'";
                        sqlcount += sqlAdd;
                    }
                    else
                    {
                        sqlcount = "select count(1) from self_expert  where 1=1 and country ='CN' and title like '%" + keywords + "%' ";
                        sqlcount += sqlAdd;
                     }
                   total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
                    if (total == 0)
                        return null;
                    }
           
                if (country == 1)
                {
                    sqlstr = "select * from self_expert where 1=1 and country !='CN' and title like '%" + keywords + "%' ";
                    sqlstr += sqlAdd;
                    sqlstr += sqlexp;
                }
                else
                {
                    sqlstr = "select * from self_expert where 1=1 and country='CN' and title like '%" + keywords + "%' ";
                    sqlstr += sqlAdd;
                    sqlstr += sqlexp;
            }
          
            return MySqlHelper.GetDataTable(sqlstr, out errmsg, pars.ToArray());
        }
        #endregion

        #region 报告
        internal static DataTable GetResourceRptList(int themeid, int rpttype,string keywords, string sortexp, int rows,  int page, out int total, out string errmsg)
        {
            int offset = 0;
            total = 0;
            string sqlstr;
            string sqlcount;
            string sqlAdd = "";
            string sqlexp = " order by ";
            List<DataParameter> pars = new List<DataParameter>();
            //pars.Add(new DataParameter("@tablename", tablename));
            pars.Add(new DataParameter("@sortexp", sortexp));
            sqlexp += sortexp;
            offset = (page - 1) * rows;
            pars.Add(new DataParameter("@offse", offset));
            sqlexp += " limit @offse,@rows";
            pars.Add(new DataParameter("@rows", rows));

            //order by @sortexp limit @offset,@rows
            if (themeid != 0)
            {
                sqlAdd += "and r.classid in(@classid)";
                pars.Add(new DataParameter("@classid", themeid));
            }
            if (rpttype != 0)
            {
                sqlAdd += " and r.reporttype=@reporttype";
                pars.Add(new DataParameter("@reporttype", rpttype));
            }
            if (page > 0)
            {
               

               
                sqlcount = "select count(1) from self_report r where 1=1 and r.title like '%" + keywords + "%' ";
                sqlcount += sqlAdd;
              
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
                if (total == 0)
                    return null;
            }

           
                sqlstr = "select r.*,o.title as orgname from self_report r,self_organization o where 1=1 and r.title like '%" + keywords + "%' ";
                sqlstr += sqlAdd;
                sqlstr += sqlexp;
           
         

            return MySqlHelper.GetDataTable(sqlstr, out errmsg, pars.ToArray());
        }
        #endregion
        /// <summary>
        /// 获取栏目列表不分页
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="rows"></param>
        /// <param name="sortexp"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolist(string tablename, int rows, int offset, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where 1=1 order by {1} limit {2},{3}", tablename, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfo_report(int language, int reporttype, string tablename, string seqid, int offset, int rows, out string errmsg)
        {
            string sqlstr;
            //中文
            if (language == 1)
            {
                sqlstr = string.Format("select * from {0} where seqid in({1}) and reporttype={2} and language=1 limit {3},{4}", tablename, reporttype, seqid, offset, rows);
            }
            else
            {
                sqlstr = string.Format("select * from {0} where seqid in({1}) and reporttype={2} and language!=1 limit {3},{4}", tablename, reporttype, seqid, offset, rows);
            }
            sqlstr = string.Format("select * from {0} where seqid in({1}) limit {2},{3}", tablename, seqid, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion
        #region 专家领域分布
        /// <summary>
        /// 专家领域分布
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="research"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceExpertlist(string tablename, string research, int rows, int offset, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where 1=1 and birthday!='0000-00-00' and research like '%{1}%' order by {2} limit {3},{4}", tablename, research, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion

        internal static DataTable GetResourceWordlist(string tablename, string country, int rows, int offset, string sortexp, out string errmsg)
        {

            string sqlstr = string.Format("select r.orgid as orgid,count(r.seqid) as nums,o.title as title from {0} r,self_organization o where 1=1 and r.country='{1}' and o.seqid = r.orgid GROUP by r.orgid order by {2} limit {3},{4}", tablename, country, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }

        #region 期刊
        internal static DataTable GetResourceYear(string tablename, int journalid, int rows, int offset, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select pubyear as years from {0} where 1=1 and pubyear != 0 and  journalid={1} GROUP by pubyear order by {2} limit {3},{4}", tablename, journalid, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }

        internal static DataTable GetResourceIssue(string tablename, int journalid, int years, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select pubyear as years,issue from {0} where 1=1 and pubyear = {1} and journalid={2}  order by {3}", tablename, years, journalid, sortexp);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        internal static DataTable GetResourceJourTitle(string tablename, int journalid, int years, int issue, string sortexp, out string errmsg)
        {
            string sqlstr = string.Format("select * from {0} where 1=1 and journalid={1} and  pubyear = {2} and  issue ={3}  order by {4}", tablename, journalid, years, issue, sortexp);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }

        #endregion
        #region
        internal static DataTable GetResourceEchar(string tablename, string sortexp, out string errmsg)
        {
            int year = DateTime.Now.Year;
            //SELECT pubyear, count(1) as num1 FROM self_journal where 1 = 1 and pubyear != 0 and pubyear >= 2017 - 7 and pubyear <= 2017 group BY pubyear;
            string sqlstr = string.Format("select pubyear, count(1) as nums from {0} where 1=1 and pubyear != 0 and pubyear >= {1} - 7 and pubyear <= {1} group BY pubyear  order by {2}", tablename, year, sortexp);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        internal static DataTable GetResourceEchar_degree(string tablename, string sortexp, out string errmsg)
        {
            int year = DateTime.Now.Year;
            string sqlstr = string.Format("select degreeyear as pubyear,count(1) nums from {0} where 1=1 and degreeyear != 0 and degreeyear >= {1} - 7 and degreeyear <= {1} group BY degreeyear  order by {2}", tablename, year, sortexp);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        internal static DataTable GetResourceclass(int parentid, out string errmsg)
        {
            string sqlstr = string.Format("select * from base_classes where parentclassid={0}", parentid);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
       
       internal static DataTable GetResourceEchar_click(string tablename,int years, int rows,string sortexp, out string errmsg)
        {
            int offset = 0;
            string sqlstr = "";
            if (tablename == "self_degreethesis")
            {
                sqlstr = string.Format("select seqid as rescode,'self_degreethesis' as restype,title from {0} where 1=1 and pubdate!='0000-00-00' and degreeyear={1} order by degreeyear limit {2},{3}", tablename, years, offset, rows);

            }
            else
            {
                sqlstr = string.Format("select seqid as rescode,'{0}' as restype,title from {0} where 1=1 and pubdate!='0000-00-00' and pubyear={1} order by {2} limit {3},{4}", tablename, years, sortexp, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion
        #region 机构
        internal static DataTable GetResourceOrg( out string errmsg)
        {
     
            string sqlstr = string.Format(" SELECT 'Country' AS dicname,'' as country, sum(t.nums) as nums from( SELECT o.country, d.dicname, count(o.country) as nums from self_organization o, base_dics d where o.country = d.dicvalue group BY o.country ) as t UNION ALL SELECT d.dicname,o.country,count(o.country) as nums from self_organization o,base_dics d where o.country = d.dicvalue group BY o.country");
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        internal static DataTable GetResourceOrgbyCty(string country, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            string sqlcount;
            int offset = 0;
            total = 0;
          
            if (page > 0)
            {
                offset = (page - 1) * rows;
                sqlcount = string.Format("select count(1) from self_organization where country='{0}'", country);
               
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
            string sqlstr = string.Format("select * from self_organization where 1=1 and country='{0}' order by {1}  limit {2},{3}", country, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg); 
        }

        #endregion


        #region 专题 条件查询专题列表

        internal static DataTable GetResourceSplList(int themeid, int orgid,string keywords, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            int offset = 0;
            total = 0;
            string sqlstr;
            string sqlcount;
            string sqlAdd = "";
            string sqlexp = " order by ";
            List<DataParameter> pars = new List<DataParameter>();
            //pars.Add(new DataParameter("@tablename", tablename));
            pars.Add(new DataParameter("@sortexp", sortexp));
            sqlexp += sortexp;
            pars.Add(new DataParameter("@offse", offset));
            sqlexp += " limit @offse,@rows";
            pars.Add(new DataParameter("@rows", rows));

            //order by @sortexp limit @offset,@rows
            if (themeid != 0)
            {
                sqlAdd += "and s.classid in(@classid)";
                pars.Add(new DataParameter("@classid", themeid));
            }
            if (orgid != 0)
            {
                sqlAdd += " and s.orgid=@orgid";
                pars.Add(new DataParameter("@orgid", orgid));
            }
            if (page > 0)
            {
                offset = (page - 1) * rows;


                sqlcount = "select count(1) from self_specialsubject s,self_organization o  where 1=1 and s.orgid=o.seqid and s.pubdate!='0000-00-00' and s.title like '%" + keywords + "%'  ";
                sqlcount += sqlAdd;

                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
                if (total == 0)
                    return null;
            }


            sqlstr = "select s.*,o.title as orgname  from self_specialsubject s,self_organization o  where 1=1 and s.orgid=o.seqid  and s.pubdate!='0000-00-00' and s.title like '%" + keywords + "%'  ";
            sqlstr += sqlAdd;
            sqlstr += sqlexp;



            return MySqlHelper.GetDataTable(sqlstr, out errmsg, pars.ToArray());
        }
        #endregion

        #region 政策条件查询
        internal static DataTable GetResourcePlyList(int policytype,int Ptype ,int classid,string disage,int level,string keywords,string startime, string sortexp, int rows, int page, out int total, out string errmsg)
        {
           
            int offset = 0;
            total = 0;
            string sqlstr;
            string sqlcount;
            string sqlplyAdd = "";
            string sqlplyterAdd = "";
            string sqlexp = " order by ";
            List<DataParameter> pars = new List<DataParameter>();
            //pars.Add(new DataParameter("@tablename", tablename));
            pars.Add(new DataParameter("@sortexp", sortexp));
            sqlexp += sortexp;
            offset = (page - 1) * rows;
            pars.Add(new DataParameter("@offse", offset));
            sqlexp += " limit @offse,@rows";
            pars.Add(new DataParameter("@rows", rows));

            //order by @sortexp limit @offset,@rows
            if (classid != 0)
            {
                sqlplyAdd += "and classid in(@classid)";
                sqlplyterAdd += "and classid in(@classid)";
                pars.Add(new DataParameter("@classid", classid));
            }
            if (Ptype != 0)
            {
                //sqlplyAdd += "and classid in(@classid)";
                sqlplyterAdd += "and policytype =@policytype";
                pars.Add(new DataParameter("@policytype", Ptype));
            }
            
            if (!string.IsNullOrEmpty(disage) && disage != "全部" && disage != "请选择")
                {
                sqlplyAdd += "and disagency =@disagency";
                pars.Add(new DataParameter("@disagency", disage));
            }
            if (level != 0)
            {
                sqlplyAdd += "and level =@level";
                pars.Add(new DataParameter("@level", level));
            }
            if (!string.IsNullOrEmpty(startime)) {
                sqlplyAdd += "and pubdate ='@startime'";
                sqlplyterAdd += "and pubdate ='@startime'";
                pars.Add(new DataParameter("@classid", startime));
         
            }
            if (page > 0)
            {
                offset = (page - 1) * rows;

                if (policytype == 1)
                {
                    
                    sqlcount = "select count(1) from self_policies where 1=1   and keywords like '%" + keywords + "%' or title like '%" + keywords + "%' ";
                    sqlcount += sqlplyAdd;
                }
                else {

                    sqlcount = "select count(1) from self_policyinterpretation where 1=1 and title like '%" + keywords+"%' ";
                    sqlcount += sqlplyterAdd;

                }
               total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
                
                if (total == 0)
                    return null;
            }

            if (policytype == 1)
            {
                sqlstr = "select *,'self_policies' as datanum  from self_policies where 1=1  and keywords like '%" + keywords + "%' or title like '%" + keywords + "%' ";
                sqlstr += sqlplyAdd;
                sqlstr += sqlexp;
            }
            else{
                sqlstr = "select *,'self_policyinterpretation' as datanum from self_policyinterpretation where 1=1 and  title like '%" + keywords + "%' ";
                sqlstr += sqlplyterAdd;
                sqlstr += sqlexp;
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg, pars.ToArray());
        }
        #endregion
        internal static DataTable getLitaList(string tablename, string time, string itemark,out string errmsg)
        {
            string sqlstr = string.Format("SELECT title,seqid as rescode,'{0}' as restype from {0} b where   YEAR(b.pubdate)={1} AND (b.title LIKE '%{2}%' OR  b.keywords LIKE '%{2}%') limit 0,8", tablename,time, itemark);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #region 国内研究
        internal static DataTable GetGn_Class(out string errmsg)
        {
            string sqlstr = string.Format("SELECT ms.itemid as itemid,ms.itemname as itemname,ms.itemmark as itemmark,d.authormark as authormark,d.iteauthorname as iteauthorname  from " +
                "base_items ms  INNER JOIN(  SELECT m.parentid as itemid,m.itemname as iteauthorname,m.itemmark as authormark from " +
                "base_items m INNER JOIN (SELECT *from base_items where parentid=44)as b on m.parentid=b.itemid ) as d on ms.itemid=d.itemid where parentid=44 ");
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion
    }
}