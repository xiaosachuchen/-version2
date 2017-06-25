using ISeeAPI.Models;
using Microsoft.RIPSP.Model;
using PortalSite.Interface.BLL;
using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static PortalSite.Interface.BLL.SearchBLL;

namespace PortalSite.Interface.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet, Route("Search/Search")]
        public object Search(string keyword, int page, int rows)
        {
            QuerySearchModel query = new QuerySearchModel();
            query.queryExp = keyword;
            query.restype = 0;
            query.isSearch = true;
            query.page = page;
            query.rows = rows;
            return SearchExtend(query);
        }
        [HttpGet, Route("Search/GetOption")]
        public object GetOption(int indexType)
        {
            ISeeIndexServer server = BLL.SearchBLL.GetISeeServer(indexType);
            if (server == null)
            {
                return new { Rcode = -1, Rmsg = "未获取到对应索引:" + indexType };
            }
            return new { Rcode = 1, Fields = server.Fields, Attrs = server.Attrs };
        }
        #region 搜索
        [HttpPost, Route("Search/SearchExtend")]
        public object SearchExtend(QuerySearchModel query)
        {
            if (query == null)
                return new { Rcode = -1, Rmsg = "错误请求" };

            string errmsg = null;
            TimeSpan usetime = new TimeSpan();
            SearchResult result = SearchBLL.SearchQuery(query, ref usetime, ref errmsg);
            if (!string.IsNullOrEmpty(errmsg) || result == null)
                return new { Rcode = -1, Rmsg = errmsg };
            else if (result.TotalCount == 0)
                return new { Rcode = 0, total = 0, Rmsg = "没有符合条件的数据" };
            else
            {
                if (string.IsNullOrEmpty(query.groupName))
                {
                    DataTable rdt = new DataTable();
                    //   int pagecount = result.ResultCount % query.rows == 0 ? result.ResultCount / query.rows : result.ResultCount / query.rows + 1;
                    if (result.Attrs != null && result.Attrs.Length > 0)
                    {
                        rdt.Columns.Add(new DataColumn("seqid"));
                        foreach (AttrMatch am in result.Attrs)
                        {
                            rdt.Columns.Add(new DataColumn(am.AttrName));
                        }
                    }
                    DataRow dr;
                    foreach (ResultMatches match in result.Matches)
                    {
                        dr = rdt.NewRow();
                        dr["seqid"] = match.DocumentID;
                        foreach (DataColumn dc in rdt.Columns)
                        {
                            if (dc.ColumnName != "seqid")
                                dr[dc.ColumnName] = match.GetAttrValue(dc.ColumnName);
                        }
                        rdt.Rows.Add(dr);
                    }
                    return new
                    {
                        Rcode = 1,
                        list = rdt,
                        total = result.TotalCount,
                        PageCount = result.TotalCount,
                        usetime = usetime.TotalMilliseconds
                    };
                }
                else
                {
                    int classparentid = 12;
                    string _errmsg;
                    List<Options> optionlist = null;
                    switch (query.groupName.Trim())
                    {
                        case "restype":
                            SearchBLL.GetResTypeOptions(out _errmsg);
                            optionlist = SearchBLL.restypeList;
                            break;
                        case "classid":
                            SearchBLL.GetClassOptions(classparentid, out _errmsg);
                            optionlist = SearchBLL.classList[classparentid];
                            break;
                        case "languageid":
                            SearchBLL.GetDicOptions(out _errmsg);
                            optionlist = SearchBLL.dictList["language"];
                            break;
                        case "authorstr":
                            SearchBLL.GetAuthorOptions(out _errmsg);
                            optionlist = SearchBLL.authorsList;
                            break;
                    }
                    List<SearchGroupModel> grouplist = new List<SearchGroupModel>();
                    SearchGroupModel group;
                    foreach (ResultMatches match in result.Matches)
                    {
                        group = new SearchGroupModel();
                        group.groupvalue = match.GetAttrValue("@groupby").ToString().Trim();
                        group.groupcount = int.Parse(match.GetAttrValue("@count").ToString());
                        Options option = optionlist != null ? optionlist.Find(o => o.id == group.groupvalue) : null;
                        if (option != null)
                            group.groupname = option.text;
                        else
                            group.groupname = group.groupvalue;
                        grouplist.Add(group);
                    }
                    if (query.groupName.Trim().Equals("classid"))
                    {
                        grouplist = SearchBLL.GetClassGroupTree(classparentid, grouplist);
                    }
                    return new
                    {
                        Rcode = 1,
                        list = grouplist,
                        total = result.TotalCount,
                        PageCount = result.TotalCount,
                        usetime = usetime.TotalMilliseconds
                    };
                }
            }
        }
        #endregion
        [HttpGet, Route("Search/GetChildrenClassids")]
        public object GetChildrenClassids(int parentid)
        {
            if (parentid <= 0)
                return new { Rcode = -1, Rmsg = "数据出错" };
            string _errmsg = null;
            List<base_classes> child = SearchBLL.GetChildrenByParentid(parentid, out _errmsg);
            if(!string.IsNullOrEmpty(_errmsg))
                return new { Rcode = -1, Rmsg = "错误请求" };

            string id = parentid.ToString();
            if (child==null)
                return new { Rcode = 1, Rdata = id };

            foreach(base_classes row in child)
            {
                id += ","+row.classid.ToString()+",";
            }
            return new { Rcode = 1, Rdata = id.TrimEnd(',') };

        }
    }
}
