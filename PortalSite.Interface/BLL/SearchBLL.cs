using ISeeAPI;
using ISeeAPI.Models;
using Microsoft.RIPSP.Model;
using Newtonsoft.Json;
using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PortalSite.Interface.BLL
{
    public static class SearchBLL
    {
        public static List<Options> restypeList = null;
        public static Dictionary<string, List<Options>> dictList = null;
        public static Dictionary<int,List<Options>> classList = null;
        public static List<Options> authorsList = null;
        public static List<ISeeIndexServer> ISeeConfig = null;

        public static void GetResTypeOptions(out string errmsg)
        {
            errmsg = null;
            if (restypeList == null||restypeList.Count==0)
            {
                restypeList = new List<Options>();
                List<db_datalibrarys> librarylist =  DAL.SearchDAL.GetResTypeList(out errmsg);
                if(string.IsNullOrEmpty(errmsg)&& librarylist != null && librarylist.Count > 0)
                {
                    foreach(db_datalibrarys library in librarylist)
                    {
                        restypeList.Add(new Options() { id = library.databaseid.ToString(), text = library.databasecname, tag = library });
                    }
                }
            }
        }
        public static void GetDicOptions(out string errmsg)
        {
            errmsg = null;
            if (dictList == null)
            {
                List<base_dics> list = DAL.SearchDAL.GetDictList("", out errmsg);
                dictList = new Dictionary<string, List<Options>>();
                foreach (base_dics dics in list)
                {
                    if (dictList.ContainsKey(dics.dictype))
                        dictList[dics.dictype].Add(new Options() { id = dics.dicvalue, text = dics.dicname });
                    else
                        dictList.Add(dics.dictype, new List<Options>() { new Options() { id = dics.dicvalue, text = dics.dicname } });
                }
            }
        }
        public static void GetAuthorOptions(out string errmsg)
        {
            errmsg = null;
            if (authorsList == null || authorsList.Count == 0)
                authorsList = DAL.SearchDAL.GetAuthorsList(out errmsg);
        }
        public static void GetClassOptions(int parentid, out string errmsg)
        {
            errmsg = null;
            if (classList == null || !classList.ContainsKey(parentid) || classList[parentid].Count == 0)
            {
                classList = new Dictionary<int, List<Options>>();
                List<base_classes> classlist = DAL.SearchDAL.GetClassAll(out errmsg);
                if (!string.IsNullOrEmpty(errmsg) || classlist == null || classlist.Count == 0)
                {
                    classList.Add(parentid, new List<Options>());
                    return;
                }
                if (!classList.ContainsKey(parentid))
                    classList.Add(parentid, new List<Options>());
                else
                    classList[parentid].Clear();
                GetChildren(parentid, classList[parentid], classlist);
            }
        }
        private static void GetChildren(int parentid, List<Options> optionlist, List<base_classes> classlist)
        {
            List<base_classes> rootclass = classlist.FindAll(o => o.parentclassid == parentid);
            if (rootclass == null || rootclass.Count == 0)
            {
                return;
            }
            Options option;
            foreach (base_classes classes in rootclass)
            {
                option = new Options()
                {
                    id = classes.classid.ToString(),
                    text = classes.classname,
                    tag = classes.parentclassid.ToString()
                };
                optionlist.Add(option);
                GetChildren((int)classes.classid, optionlist, classlist);
            }
        }

        public static List<SearchGroupModel> GetClassGroupTree(int parentid, List<SearchGroupModel> sgrouplist)
        {
            List<SearchGroupModel> grouptree = new List<SearchGroupModel>();
            List<Options> optiononelist = classList[parentid].FindAll(o => o.tag.ToString() == parentid.ToString());
            SearchGroupModel tsgm;
            if (optiononelist != null)
            {
                foreach (Options option in optiononelist)
                {
                    tsgm = sgrouplist.Find(o => o.groupvalue == option.id);
                    if (tsgm == null)
                    {
                        tsgm = new SearchGroupModel()
                        {
                            groupvalue = option.id,
                            groupname = option.text,
                            groupcount = 0,
                        };
                    }
                    tsgm.children = new List<SearchGroupModel>();
                    grouptree.Add(tsgm);
                    SearchGroupChidren(tsgm, classList[parentid], sgrouplist);
                    if (string.IsNullOrEmpty(tsgm.grouptags))
                        tsgm.grouptags = tsgm.groupvalue;
                    else
                        tsgm.grouptags = tsgm.grouptags.Trim(',');
                }
            }
            return grouptree;
        }
        private static void SearchGroupChidren(SearchGroupModel sgroupmodel, List<Options> grouplist, List<SearchGroupModel> sgroup)
        {
            List<Options> optionlist = grouplist.FindAll(o => o.tag.ToString() == sgroupmodel.groupvalue.ToString());
            if (optionlist != null)
            {
                SearchGroupModel tsgm;
                foreach (Options option in optionlist)
                {
                    tsgm = sgroup.Find(o => o.groupvalue == option.id);
                    if (tsgm != null)
                    {
                        sgroupmodel.groupcount += tsgm.groupcount;
                        sgroupmodel.grouptags += tsgm.groupvalue + ",";
                        sgroupmodel.children.Add(tsgm);

                        tsgm.children = new List<SearchGroupModel>();                     
                        SearchGroupChidren(tsgm, grouplist, sgroup);
                        if (string.IsNullOrEmpty(tsgm.grouptags))
                            tsgm.grouptags = tsgm.groupvalue;
                        else
                            tsgm.grouptags = tsgm.grouptags.Trim(',');
                    }
                }
            }
        }


        /// <summary>
        /// 获取检索结果
        /// </summary>
        /// <param name="query">检索对象</param>
        /// <param name="usetime">检索用时</param>
        /// <param name="errmsg">错误信息</param>
        /// <returns>检索结果</returns>
        public static SearchResult SearchQuery(QuerySearchModel query, ref TimeSpan usetime, ref string errmsg)
        {
            ISeeClient ISee = new ISeeClient();
            ISeeIndexServer ISeeServerInfo = GetISeeServer(query.restype);
            if (ISeeServerInfo == null)
            {
                errmsg = "未获取到对应索引:" + query.restype;
                return null;
            }
            ISee.Ip = ISeeServerInfo.IP;
            ISee.Port = ISeeServerInfo.Port;
            ISee.AutoFilterTag = false;// ISeeServerInfo.AutoFilterTag;
            ISee.MaxMatchCount = ISeeServerInfo.MaxMatchCount;
            ISee.SearchMode = SearchMode.MatchExtended2;
            if (query.rows <= 0)
                query.rows = ISeeServerInfo.MaxMatchCount;

            SearchOptions option = new SearchOptions();
            option.Limit = query.rows;
            option.Offset = (query.page - 1) * query.rows;
            option.Offset = option.Offset + ISee.Limit > ISeeServerInfo.MaxMatchCount ? (ISeeServerInfo.MaxMatchCount - ISee.Limit) : option.Offset;
            option.Index = ISeeServerInfo.Index;
            FilterAttrs(query.queryExp, query.restype, ref option, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            // 分组字段
            if (!string.IsNullOrEmpty(query.groupName))
            {
                option.GroupAttrName = query.groupName;
                option.ResultAttrs = query.groupName;
                // 分组排序
                GroupSort groupSort = GroupSort.CountDesc;
                if (!string.IsNullOrEmpty(query.sortExp) && Enum.TryParse<GroupSort>(query.sortExp, out groupSort))
                {
                    option.GroupSort = groupSort;
                }
                else
                {
                    option.GroupSort = GroupSort.CountDesc; // 默认
                }
            }
            else
            {
                // 排序
                if (!string.IsNullOrEmpty(query.sortExp))
                {
                    option.SortExp = query.sortExp;
                }
                //查询词计数
                //if (!string.IsNullOrEmpty(option.KeyExp))
                //{
                //    if (query.isSearch)
                //    {
                //        string _errmsg;
                //        Regex regex = new Regex("[\u4e00-\u9fa5]+");
                //        Dictionary<string, int> keylist = new Dictionary<string, int>();
                //        foreach (Match match in regex.Matches(option.KeyExp))
                //        {
                //            if (match.Success && match.Groups.Count > 0 && !keylist.ContainsKey(match.Groups[0].Value))
                //                keylist.Add(match.Groups[0].Value, 0);
                //        }
                //        if (keylist.Count > 0)
                //        {
                //            string[] keyarr = new string[keylist.Count];
                //            int i = 0;
                //            foreach (string key in keylist.Keys)
                //            {
                //                keyarr[i] = key;
                //                i++;
                //            }
                //            DAL.SearchDAL.UpdateHotKeyData(keyarr, out _errmsg);
                //        }
                //    }
                //}
            }
            // 查询结果
            SearchResult results = ISee.Query(option);
            if (!string.IsNullOrEmpty(ISee.ErrMsg))
            {
                errmsg = ISee.ErrMsg;
                return null;
            }
            if (!string.IsNullOrEmpty(results.Warnmsg))
            {

            }
            usetime = ISee.FoundTime;
            return results;
        }
        private static void FilterAttrs(string queryExp, int resType, ref SearchOptions option, out string errmsg)
        {
            errmsg = null;
            if (string.IsNullOrEmpty(queryExp))
            {
                option.KeyExp = queryExp;
                return;
            }
            dynamic expjson;
            try
            {
                expjson = JsonConvert.DeserializeObject(queryExp);
            }
            catch (Exception ex)
            {
                option.KeyExp = queryExp;
                return;
            }
            option.KeyExp = expjson.key;
            if (queryExp.IndexOf("\"filters\":") > 0 && expjson.filters.Count > 0)
            {
                option.Filters = new List<RangeFilter>();
                RangeFilter rf;
                bool isexclude;
                bool isrange;
                foreach (dynamic filter in expjson.filters)
                {
                    if (filter != null && filter.value != null && !string.IsNullOrEmpty(filter.value.ToString()))
                    {
                        if ("|!=|out|".IndexOf("|" + filter.oper.ToString() + "|") >= 0)
                            isexclude = true;
                        else
                            isexclude = false;
                        if ("|in|out|".IndexOf(filter.oper.ToString()) > 0)
                            isrange = true;
                        else
                            isrange = false;

                        string[] arr = filter.value.ToString().Split(',');
                        switch ((string)filter.type)
                        {
                            case "num":
                                rf = FilterAnalysisByNum(filter.name.ToString(), isexclude, isrange, arr);
                                break;
                            case "time":
                                rf = FilterAnalysisByNum(filter.name.ToString(), isexclude, isrange, arr);
                                break;
                            default:
                                rf = FilterAnalysisByString(filter.name.ToString(), isexclude, arr);
                                break;
                        }
                        if (rf != null)
                            option.Filters.Add(rf);
                    }
                }
            }
        }
        private static RangeFilter FilterAnalysisByNum(string name, bool isexclude, bool isrange, string[] arr)
        {
            List<ulong> listarr = new List<ulong>();
            for (int i = 0; i < arr.Length; i++)
            {
                ulong tulong;
                if (ulong.TryParse(arr[i], out tulong) && (tulong > 0 || (name.Equals("isfull") && tulong >= 0)))
                    listarr.Add(tulong);
            }
            RangeFilter rf = null;
            if (listarr.Count > 0)
            {
                if (isrange && listarr.Count == 2)
                {
                    rf = new RangeFilter(name, listarr[0], listarr[1], isexclude);
                }
                else
                {
                    rf = new RangeFilter(name, isexclude, listarr.ToArray());
                }
            }
            return rf;
        }
        private static RangeFilter FilterAnalysisByTime(string name, bool isexclude, bool isrange, string[] arr)
        {
            List<DateTime> listarr = new List<DateTime>();
            for (int i = 0; i < arr.Length; i++)
            {
                DateTime ttime;
                if (DateTime.TryParse(arr[i], out ttime) && ttime > DateTime.MinValue)
                    listarr.Add(ttime);
            }
            RangeFilter rf = null;
            if (listarr.Count > 0)
            {
                if (isrange && listarr.Count == 2)
                {
                    rf = new RangeFilter(name, listarr[0], listarr[1], isexclude);
                }
                else
                {
                    rf = new RangeFilter(name, isexclude, listarr.ToArray());
                }
            }
            return rf;
        }
        private static RangeFilter FilterAnalysisByString(string name, bool isexclude, string[] arr)
        {
            List<string> listarr = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!string.IsNullOrEmpty(arr[i]))
                    listarr.Add(arr[i]);
            }
            RangeFilter rf = null;
            if (listarr.Count > 0)
            {
                rf = new RangeFilter(name, isexclude, listarr.ToArray());
            }
            return rf;
        }
        private static Dictionary<int, ISeeIndexServer> GetIndexServerList(out string errmsg)
        {
            List<isee_services> list = DAL.SearchDAL.GetIndexServerList(out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || list == null) return null;
            Dictionary<int, ISeeIndexServer> dictlist = new Dictionary<int, ISeeIndexServer>();
            foreach (isee_services service in list)
            {
                int res = Convert.ToInt32(service.restype);
                if (!dictlist.ContainsKey(res))
                    dictlist.Add(res, new ISeeIndexServer(res, service.ip, (int)service.port, service.servicecode, Models.GlobalParameters.ISee_AutoFilterTag, Models.GlobalParameters.ISee_MaxMatchCount));
            }
            return dictlist;
        }
        /// <summary>
        /// 根据索引类型获取索引服务器信息
        /// </summary>
        /// <param name="resType">资源类型</param>
        /// <returns>索引服务器信息</returns>
        public static ISeeIndexServer GetISeeServer(int resType)
        {
            string errmsg = null;
            Dictionary<int, ISeeIndexServer> list = GetIndexServerList(out errmsg);
            if (list == null)
                return null;
            if (list.ContainsKey(resType))
                return list[resType];
            else
                return null;
        }
        /// <summary>
        /// 根据资源类型获取资源库表名称
        /// </summary>
        /// <param name="resType"></param>
        /// <returns></returns>
        public static string GetTableNameByResType(int resType)
        {
            if (resType < 1) return null;
            string errmsg = null;
            List<db_datalibrarys> librarylist = DAL.SearchDAL.GetResTypeList(out errmsg);
            if (string.IsNullOrEmpty(errmsg) && librarylist != null && librarylist.Count > 0)
            {
                db_datalibrarys library = librarylist.Find(o => o.databaseid == resType);
                if (library != null)
                    return library.databasename;
              
            }
            return null;
        }

        public static List<base_classes> GetChildrenByParentid(int parentid, out string errmsg)
        {
            return DAL.SearchDAL.GetChildrenByParentid(parentid, out errmsg);
        }
    }
}