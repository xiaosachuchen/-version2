using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class ResourcesController : ApiController
    {
        private string errmsg = null;
        public BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();
        /// <summary>
        /// 获取资源详情信息
        /// </summary>
        /// <param name="restype">资源类型</param>
        /// <param name="seqid">资源编号</param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfo")]
        public object GetResourceInfo(string restype, int seqid)
        {
          
            DataTable dt = BLL.ResourcesBLL.GetResourceInfo(restype, seqid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
           else if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            return new { Rcode = 1, Rdata = dt };
        }
        #region
        /// <summary>
        /// 根据ids 获取栏目内容列表
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolistbyids")]
        public object GetResourceInfolistbyids(int restype, string seqid, int offset, int rows)
        {
            DataTable dt = BLL.ResourcesBLL.GetResourceInfolistbyids(restype, seqid, offset, rows, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        /// <summary>
        /// 根据ids 获取栏目内容
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfobyids")]
        public object GetResourceInfobyids(int restype, string seqid)
        {
            DataTable dt = BLL.ResourcesBLL.GetResourceInfobyids(restype, seqid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion
        #region
        /// <summary>
        /// 根据ids 获取title
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolistbyids_group")]
        public object GetResourceInfolistbyids_group(int restype, string seqid)
        {
            DataTable dt = BLL.ResourcesBLL.GetResourceInfolistbyids_group(restype, seqid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        /// <summary>
        /// 根据title 获取childrentitle
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolistbyids_group")]
        public object GetResourceInfolistbytitle_group(int restype, string title)
        {
            DataTable dt = BLL.ResourcesBLL.GetResourceInfolistbytitle_group(restype, title, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        /// <summary>
        /// 获取栏目列表 分页
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfos")]
        public object GetResourceInfos(int restype, int rows, int page)
        {
            string sortexp = "pubdate desc";
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourceInfos(restype, rows, sortexp, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        #region 专家
        /// <summary>
        /// 获取国内/海外专家
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceexpert")]
        public object GetResourceexpert(int classid, int restype, int rows, int page)
        {
            string sortexp = "hits desc";
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourceexpert(classid, restype, rows, sortexp, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="country">国别</param>
        /// <param name="restype"></param>
        /// <param name="research">研究领域</param>
        /// <param name="experttype">专家类型</param>
        /// <param name="orgid">机构id</param>
        /// <param name="initials">大写字母
        /// </param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceEptList")]
        public object GetResourceEptList(string keywords,int country,int research,int experttype,int orgid, string sortexp,int rows, int page)
        {
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourceEptList(keywords,country, research, experttype, orgid, rows, sortexp, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        #endregion

        #region 报告
        [HttpGet, Route("Base/GetResourceRptList")]
        public object GetResourceRptList(int themeid, int rpttype,string keywords, string sortexp, int rows, int page)
        {
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourceRptList(themeid, rpttype, keywords, sortexp, rows, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        #endregion
        /// <summary>
        /// 获取栏目列表  仅专题用
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolist")]
        public object GetResourceInfolist(int restype, int rows, int offset)
        {
            string sortexp = " hits desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceInfolist(restype, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        /// <summary>
        /// 根据classid 获取栏目内容
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolistbyclassid")]
        public object GetResourceInfolistbyclassid(int restype, int seqid, string classid, int rows, int offset)
        {
            string sortexp = " hits desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceInfolistbyclassid(restype, seqid, classid, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }

        #region 机构 根据orgid 获取信息
        /// <summary>
        /// 根据orgid 获取栏目内容
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="rows"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfolistbyorgid")]
        public object GetResourceInfolistbyorgid(int restype, int orgid, int rows, int offset)
        {
            string sortexp = " hits desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceInfolistbyorgid(restype, orgid, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 专题页-区分国内/国外
        /// <summary>
        /// 
        /// </summary>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetResourceInfo_report")]
        public object GetResourceInfo_report(int language, int reporttype, int restype, string seqid, int offset, int rows)
        {
            DataTable dt = BLL.ResourcesBLL.GetResourceInfo_report(language, reporttype, restype, seqid, offset, rows, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 专家领域分布
        [HttpGet, Route("Base/GetResourceExpertlist")]
        public object GetResourceExpertlist(int restype, string research, int rows, int offset)
        {
            string sortexp = " hits desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceExpertlist(restype, research, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 世界地图数据
        [HttpGet, Route("Base/GetResourceWordlist")]
        public object GetResourceWordlist(int restype, string country, int rows, int offset)
        {
            string sortexp = " nums desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceWordlist(restype, country, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 期刊
        [HttpGet, Route("Base/GetResourceYear")]
        public object GetResourceYear(int restype, int journalid, int rows, int offset)
        {
            string sortexp = " YEAR(pubyear) desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceYear(restype, journalid, rows, offset, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        [HttpGet, Route("Base/GetResourceIssue")]
        public object GetResourceIssue(int restype, int journalid, int years)
        {
            string sortexp = " issue ASC";

            DataTable dt = BLL.ResourcesBLL.GetResourceIssue(restype, years, journalid, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        [HttpGet, Route("Base/GetResourceJourTitle")]
        public object GetResourceJourTitle(int restype, int journalid, int years, int issue)
        {
            string sortexp = " pubdate desc";

            DataTable dt = BLL.ResourcesBLL.GetResourceJourTitle(restype, journalid, years, issue, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 柱状图数据

        [HttpGet, Route("Base/GetResourceEchar")]
        public object GetResourceEchar(int restype)
        {
            string sortexp = " pubyear ASC";

            DataTable dt = BLL.ResourcesBLL.GetResourceEchar(restype, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }

        [HttpGet, Route("Base/GetResourceEchar_degree")]
        public object GetResourceEchar_degree(int restype)
        {
            string sortexp = " pubyear ASC";

            DataTable dt = BLL.ResourcesBLL.GetResourceEchar_degree(restype, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        [HttpGet, Route("Base/GetResourceEchar_click")]
        public object GetResourceEchar_click(int restype,int years,int rows)
        {
            string sortexp = " pubyear ASC";

            DataTable dt = BLL.ResourcesBLL.GetResourceEchar_click(restype,years,rows, sortexp, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        [HttpGet, Route("Base/GetResourceclass")]
        public object GetResourceclass(int parentId)
        {
           

            DataTable dt = BLL.ResourcesBLL.GetResourceclass(parentId, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }

        #region 机构
        [HttpGet, Route("Base/GetResourceOrg")]
        public object GetResourceOrg() {
            DataTable dt = BLL.ResourcesBLL.GetResourceOrg(out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };

        }
        [HttpGet, Route("Base/GetResourceOrgbyCty")]
        public object GetResourceOrgbyCty(string country, int rows, int page)
        {
            string sortexp = "pubdate desc";
            int total;
        
            DataTable dt = BLL.ResourcesBLL.GetResourceOrgbyCty(country,rows, sortexp,page,out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };

        }
        #endregion

        #region 专题列表条件查询
        [HttpGet, Route("Base/GetResourceSplList")]
        public object GetResourceSplList(int themeid, int orgid,string keywords, string sortexp, int rows, int page)
        {
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourceSplList(themeid, orgid, keywords, sortexp, rows, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        #endregion

        #region 政策条件查询

        [HttpGet, Route("Base/GetResourcePlyList")]
        public object GetResourcePlyList(int policytype,int Ptype, int classid,string disage,int level,string keywords,string startime, string sortexp, int rows, int page)
        {
            int total;
            DataTable dt = BLL.ResourcesBLL.GetResourcePlyList(policytype, Ptype,classid, disage, level, keywords, startime,sortexp, rows, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };
        }
        #endregion
        #region 获取字典对应值
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="restype">资源类型</param>
        /// <param name="seqid">资源编号</param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetDicName")]
        public object GetDicName(string dictype, string dicvalue)
        {
            DataTable dt = BLL.ResourcesBLL.GetDicName(dictype, dicvalue, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        [HttpGet, Route("Base/GetResTypeList")]
        public object GetResTypeList()
        {
            DataTable dt = BLL.ResourcesBLL.GetResTypeList(out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion
        #region
        [HttpGet, Route("Base/getLitaList")]
        public object getLitaList(int restype,string time,string itemark) {
            DataTable dt = BLL.ResourcesBLL.getLitaList(restype,time, itemark,out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

        #region 首页 国内研究 分类获取
        [HttpGet, Route("Base/GetGn_Class")]
        public object GetGn_Class() {
            DataTable dt = BLL.ResourcesBLL.GetGn_Class(out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        #endregion

    }
}
