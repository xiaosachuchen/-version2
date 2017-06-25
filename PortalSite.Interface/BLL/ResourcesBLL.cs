using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.BLL
{
    public static class ResourcesBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfo(string restype, int seqid, out string errmsg)
        {
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfo(restype, seqid, out errmsg);

            return dt;
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
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetDicName(dictype, dicvalue, out errmsg);

            return dt;
        }
        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResTypeList(out string errmsg)
        {
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResTypeList(out errmsg);

            return dt;
        }
        /// <summary>
        /// 获取全文路径
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="seqid"></param>
        /// <param name="pageid"></param>
        /// <param name="columnname"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static BookInfo GetFilePath(string tablename, long seqid, long pageid, string columnname, out string errmsg)
        {
            return DAL.ResourcesDAL.GetFilePath(tablename, seqid, pageid, columnname, out errmsg);
        }
        #region
        /// <summary>
        /// 根据ids 获取栏目列表
        /// </summary>
        /// <param name = "restype" ></ param >
        /// < param name="seqid"></param>
        /// <param name = "errmsg" ></ param >
        /// < returns ></ returns >
        internal static DataTable GetResourceInfolistbyids(int restype, string seqid, int offset, int rows, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolistbyids(tablename, seqid, offset, rows, out errmsg);
            return dt;
        }
        /// <summary>
        /// 根据ids 获取所有id的信息
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfobyids(int restype, string seqid, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfobyids(tablename, seqid, out errmsg);
            return dt;
        }
        #endregion
        #region
        /// <summary>
        /// 查询研究课题 title-childrentitle用
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyids_group(int restype, string seqid, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolistbyids_group(tablename, seqid, out errmsg);
            return dt;
        }
        internal static DataTable GetResourceInfolistbytitle_group(int restype, string title, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolistbytitle_group(tablename, title, out errmsg);
            return dt;
        }
        #endregion
        /// <summary>
        /// 获取栏目详情
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfos(int restype, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfos(tablename, rows, sortexp, page, out total, out errmsg);

            return dt;
        }
        #region
        /// <summary>
        ///获取国内/海外专家列表 
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="sortexp"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceexpert(int classid, int restype, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceexpert(classid, tablename, rows, sortexp, page, out total, out errmsg);

            return dt;
        }
        internal static DataTable GetResourceEptList(string keywords, int country, int research, int experttype, int orgid, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceEptList(keywords,country, research,experttype,orgid,rows, sortexp, page, out total, out errmsg);

            return dt;
        }
        #endregion

        #region 报告
        internal static DataTable GetResourceRptList(int themeid, int rpttype, string keywords, string sortexp, int rows, int page,out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceRptList(themeid, rpttype, keywords, sortexp, rows, page, out total, out errmsg);

            return dt;
        }
        #endregion
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolist(int restype, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolist(tablename, rows, offset, sortexp, out errmsg);

            return dt;
        }
        /// <summary>
        /// 根据classid 获取栏目项
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="classid"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyclassid(int restype, int seqid, string classid, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolistbyclassid(tablename, seqid, classid, rows, offset, sortexp, out errmsg);
            return dt;
        }
        /// <summary>
        /// 根据orgid 获取栏目项
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="classid"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <param name="sortexp"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetResourceInfolistbyorgid(int restype, int orgid, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfolistbyorgid(tablename, orgid, rows, offset, sortexp, out errmsg);
            return dt;
        }

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name = "restype" ></ param >
        /// < param name="seqid"></param>
        /// <param name = "errmsg" ></ param >
        /// < returns ></ returns >
        internal static DataTable GetResourceInfo_report(int language, int reporttype, int restype, string seqid, int offset, int rows, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceInfo_report(language, reporttype, tablename, seqid, offset, rows, out errmsg);
            return dt;
        }
        #endregion

        #region  专家领域分布
        internal static DataTable GetResourceExpertlist(int restype, string research, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceExpertlist(tablename, research, rows, offset, sortexp, out errmsg);

            return dt;
        }
        #endregion
        internal static DataTable GetResourceWordlist(int restype, string country, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceWordlist(tablename, country, rows, offset, sortexp, out errmsg);

            return dt;
        }

        #region
        internal static DataTable GetResourceYear(int restype, int journalid, int rows, int offset, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceYear(tablename, journalid, rows, offset, sortexp, out errmsg);

            return dt;
        }

        internal static DataTable GetResourceIssue(int restype, int years, int journalid, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceIssue(tablename, journalid, years, sortexp, out errmsg);

            return dt;
        }

        internal static DataTable GetResourceJourTitle(int restype, int journalid, int years, int issue, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceJourTitle(tablename, journalid, years, issue, sortexp, out errmsg);

            return dt;
        }
        #endregion

        #region
        internal static DataTable GetResourceEchar(int restype, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceEchar(tablename, sortexp, out errmsg);

            return dt;
        }
        internal static DataTable GetResourceEchar_degree(int restype, string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceEchar_degree(tablename, sortexp, out errmsg);

            return dt;
        }
        internal static DataTable GetResourceEchar_click(int restype,int years, int rows,string sortexp, out string errmsg)
        {
            errmsg = null;

            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceEchar_click(tablename,years, rows,sortexp, out errmsg);

            return dt;
        }
        #endregion
        internal static DataTable GetResourceclass(int parentid, out string errmsg)
        {
            errmsg = null;  
            DataTable dt = DAL.ResourcesDAL.GetResourceclass(parentid,out errmsg);

            return dt;
        }

        #region
        internal static DataTable GetResourceOrg( out string errmsg)
        {
            errmsg = null;
            DataTable dt = DAL.ResourcesDAL.GetResourceOrg( out errmsg);

            return dt;
        }
        internal static DataTable GetResourceOrgbyCty(string country, int rows, string sortexp, int page, out int total, out string errmsg)
        {
            total = 0;
            errmsg = null;
            DataTable dt = DAL.ResourcesDAL.GetResourceOrgbyCty(country,rows,sortexp,page,out total,out errmsg);

            return dt;
        }
        #endregion

        #region 专题 条件查询
        internal static DataTable GetResourceSplList(int themeid, int orgid,string keywords, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourceSplList(themeid, orgid, keywords, sortexp, rows, page, out total, out errmsg);

            return dt;
        }
        #endregion


        #region 政策条件查询
        internal static DataTable GetResourcePlyList(int policytype,int Ptype, int classid, string disage, int level, string keywords,string startime, string sortexp, int rows, int page, out int total, out string errmsg)
        {
         
            errmsg = null;
            total = 0;
            //string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.GetResourcePlyList(policytype, Ptype, classid,disage,level, keywords,startime, sortexp, rows, page, out total, out errmsg);

            return dt;
        }
        #endregion
        #region
        /// <summary>
        /// 获取文献列表
        /// </summary>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable getLitaList(int restype, string time, string itemark,out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            DataTable dt = DAL.ResourcesDAL.getLitaList(tablename,time,itemark,out errmsg);

            return dt;
        }
        #endregion

        #region 首页国内研究
        internal static DataTable GetGn_Class(out string errmsg)
        {

            errmsg = null;
       
            DataTable dt = DAL.ResourcesDAL.GetGn_Class(out errmsg);

            return dt;
        }
        #endregion
    }
}