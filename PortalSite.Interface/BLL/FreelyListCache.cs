using Microsoft.RIPSP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.BLL
{
    public static class FreelyListCache
    {
        private static Dictionary<string, List<base_dics>> _diclist = null;
        private static List<base_classes> _classlist = null;

        /// <summary>
        /// 字典缓存列表
        /// </summary>
        public static Dictionary<string, List<base_dics>> Cache_DicsList
        {
            get
            {
                if (_diclist == null||_diclist.Count==0)
                {
                    string errmsg = null;
                    List<base_dics> dicList = DAL.BaseDAL.GetDicList("", out errmsg);//取所有字典值
                    if (dicList != null)
                    {
                        _diclist = new Dictionary<string, List<base_dics>>();
                        foreach (base_dics dic in dicList)
                        {
                            if (dic.isdictype == 0)
                            {
                                if (_diclist.ContainsKey(dic.dictype))
                                    _diclist[dic.dictype].Add(dic);
                                else
                                {
                                    _diclist.Add(dic.dictype, new List<base_dics>() { dic });
                                }
                            }
                        }
                    }
                }
                return _diclist;
            }
            set
            {
                _diclist = value;
            }
        }
        /// <summary>
        /// 分类缓存列表
        /// </summary>
        public static List<base_classes> Cache_ClassList
        {
            get
            {
                if (_classlist == null || _classlist.Count == 0)
                {
                    string errmsg = null;
                    _classlist = DAL.BaseDAL.GetClassList(-1, out errmsg);
                }
                return _classlist;
            }
            set
            {
                _classlist = value;
            }
        }

    }
}