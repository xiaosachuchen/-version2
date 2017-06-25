using Microsoft.RIPSP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using PortalSite.Interface.Models;
using PortalSite.Interface.BLL;
using LitJson;
using OperateManager.Models.Resourcedb;
using System.Configuration;

namespace PortalSite.Interface.Controllers
{
    public class BaseController : ApiController
    {
        private string errmsg = null;
        BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();
        /// <summary>
        /// 获取字典值的选项
        /// </summary>
        /// <param name="dicType">字典类型</param>
        /// <param name="WithNone">添加--请选择--项</param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetDicsOptionsByDicType")]
        public object GetDicsOptionsByDicType(string dicType, bool WithNone = false)
        {
            List<Options> list = BLL.BaseBLL.GetDicOptions(dicType);
            if (list == null) list = new List<Options>();
            List<Options> resultlist = new List<Options>();
            foreach (Options option in list)
            {
                if (
                    (option.id == "-1" && option.text.IndexOf("删除") >= 0) ||
                    (option.id == "0" && option.text.IndexOf("系统或录入") >= 0) ||
                    (option.id == "0" && option.text.IndexOf("系统生成") >= 0)
                    ) { }
                else
                    resultlist.Add(option);
            }
            if (WithNone)
                resultlist.Insert(0, new Options() { id = "-1", text = "--请选择--" });
            return resultlist;
        }
        /// <summary>
        /// 获取分类树选项
        /// </summary>
        /// <param name="parentID">分类父ID</param>
        ///  <param name="WithNone">添加--请选择--项</param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetClassTreeOptionsByParent")]
        public object GetClassTreeOptionsByParent(int parentID, bool WithNone = false)
        {
            List<Options> list = BLL.BaseBLL.GetClassOptions(parentID);
            if (list == null) list = new List<Options>();
            if (WithNone)
                list.Insert(0, new Options() { id = "-1", text = "--请选择--" });
            return list;
        }

        /// <summary>
        /// 获取栏目内容
        /// </summary>
        /// <param name="itemmark">栏目标识</param>
        /// <param name="rows">取几条 </param>
        /// <param name="offset">从第几条开始取</param>
        /// <returns></returns>
        [HttpGet, Route("Base/GetItemContents")]
        public object GetItemContents(string itemmark, bool ispage = false, int rows = 0, int offset = 0)
        {
            int total=0;
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                List<base_itemcontents> items = new List<base_itemcontents>();
                for (int i = 0; i < rows; i++)
                {
                    items.Add(new base_itemcontents()
                    {
                        title = "item",
                        thumbnail= "img/index/banner-4.png"
                    });
                }
                return new { Rdata = items, msg = errmsg, coursetotal = total };
            }
            if (string.IsNullOrEmpty(itemmark))
            {
                return new { Rcode = -1, Rmsg = "参数错误" };
            }
            List<base_itemcontents> list = BLL.BaseBLL.GetItemContents(itemmark, ispage, offset, rows, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (list == null || list.Count == 0)
                return new { Rcode = 0, Rmsg = "无数据" };
            else
                return new { Rcode = 1, total = total, Rdata = list };
        }
        /// <summary>
        /// 记录用户阅读或下载日志
        /// </summary>
        /// <param name="restype">资源类型</param>
        /// <param name="seqid">资源编号</param>
        /// <param name="dotype">操作类型[1：点击；2：下载：3：在线阅读...]</param>
        /// <returns></returns>
        [HttpGet, Route("Base/UpdateDoLog")]
        public object UpdateDoLog(int restype, int seqid, int dotype)
        {
            int userid = 0;
            if (userLoginManager != null && userLoginManager.LoginUser != null && userLoginManager.LoginUser.UserID > 0)
                userid = userLoginManager.LoginUser.UserID;
            bool issuc = BLL.BaseBLL.UpdateDoLog(restype, seqid, dotype, userid, out errmsg);
            if (!issuc || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "更新日志失败" };
            else
                return new { Rcode = 1 };
        }
        [HttpGet, Route("Base/GetItemEchart")]
        public object GetItemEchart(string time)
        {
            string abstracts;
         
            List<base_itemcontents> list = BLL.BaseBLL.GetItemEchart(time,out errmsg);
            for (int i = 0; i < list.Count; i++) {
                list[i].seqid = i;
             }
            for (int j = 0; j < list.Count; j++) {
                string abstraid = "";
                abstracts = list[j].abstracts;
                string[] strArray = abstracts.Split('|');
                foreach (string a in strArray) {
                    base_itemcontents item = list.Find(o=>o.title==a);
                    if (item != null) {
                        abstraid += item.seqid+",";
                    }
                }
                list[j].abstracts = abstraid.TrimEnd(',');
            }
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else
                return new { Rcode = 1,  Rdata = list };
        }
        [HttpGet, Route("Base/GetItemQsEchart")]
        public object GetItemQsEchart(int year, int wordnum = 0)
        {
            int total;
            string errmsg;
            List<base_itemcontents> list = BLL.BaseBLL.GetItemContents("wx_yjqspicdata", false, 0, 10000, out total, out errmsg);
            if (year > 0)
            {
                list = list.FindAll(o => o.aboutdate == year.ToString());
            }
            Dictionary<string, int> wordlist = new Dictionary<string, int>();
            Dictionary<string, int> nexuslist = new Dictionary<string, int>();
            List<EchartNode> nodes = new List<EchartNode>(); // category,name,value,label
            List<EchartLink> links = new List<EchartLink>(); // source,target,weight,name
            Random rand = new Random();
            foreach (base_itemcontents itemcontents in list)
            {
                if (!wordlist.ContainsKey(itemcontents.title))
                {
                    wordlist.Add(itemcontents.title, 1);
                    nodes.Add(new EchartNode()
                    {
                        category = itemcontents.author,
                        name = itemcontents.title,
                        //   value = 1,
                        symbolSize = 1
                    });
                }
            }
            base_itemcontents tcon;
            foreach (EchartNode node in nodes)
            {
                tcon = list.Find(o => o.title == node.name);
                if (tcon != null && !string.IsNullOrEmpty(tcon.abstracts))
                {
                    foreach (string word in tcon.abstracts.Split('|'))
                    {
                        if (word.Length > 0 && wordlist.ContainsKey(word))
                        {
                            links.Add(new EchartLink()
                            {
                                weight = 1,
                                name = "",
                                source = tcon.title,
                                target = word
                            });
                        }
                    }
                }
            }
            foreach (EchartNode node in nodes)
            {
                node.value = node.symbolSize = links.FindAll(o => o.source == node.name).Count;
            }
            if (wordnum > 0 && wordnum < nodes.Count)
            {
                nodes.Sort(delegate (EchartNode x, EchartNode y)
                {
                    return y.value.CompareTo(x.value);
                });
                nodes = nodes.Take(wordnum).ToList<EchartNode>();
            }
            EchartLink tlink;
            EchartNode tsnode;
            EchartNode ttnode;
            for (int i = 0; i < links.Count; i++)
            {
                tlink = links[i];
                tsnode = nodes.Find(o => o.name == tlink.source);
                ttnode = nodes.Find(o => o.name == tlink.target);
                if (tsnode == null || ttnode == null)
                {
                    links.Remove(tlink);
                    continue;
                }               
                int count = links.FindAll(o => o.source == ttnode.name && o.target == tsnode.name).Count;
                if (count > 0)
                    tlink.weight = tlink.weight * count + 2;
                if (tsnode.category == ttnode.category)
                    tlink.weight = tlink.weight * 8;
            }
            DataTable restable = ResourcesBLL.GetResTypeList(out errmsg);
            List<object> reslist = new List<object>();
            foreach (DataRow dr in restable.Rows)
            {
                reslist.Add(new
                {
                    name = dr["databasecname"].ToString(),
                    value = dr["databaseid"].ToString()
                });
            }
            return new { nodes = nodes, links = links, categoryes = reslist };
        }
    }
}
