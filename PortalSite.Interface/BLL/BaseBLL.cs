using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.RIPSP.Model;

namespace PortalSite.Interface.BLL
{
    public static class BaseBLL
    {
        internal static List<base_itemcontents> GetItemContents(string itemmark, bool ispage, int offset, int rows, out int total, out string errmsg)
        {
            List<base_itemcontents> list = DAL.BaseDAL.GetItemContents(itemmark, ispage, offset, rows, out total, out errmsg);
            return list;
        }
        internal static bool UpdateDoLog(int restype, int seqid, int dotype, int userid, out string errmsg)
        {
            string tablename = SearchBLL.GetTableNameByResType(restype);
            string columnname = null;
            switch (dotype) //操作类型[1：点击；2：下载：3：在线阅读...] 
            {
                case 1:
                    columnname = "hits";
                    break;
                case 2:
                    columnname = "downloads";
                    break;
                case 3:
                    columnname = "readnum";
                    break;
            }
            DAL.BaseDAL.UpdateDoLog(tablename, columnname, seqid, out errmsg);
            base_userlogs log = new base_userlogs()
            {
                logtype = dotype,
                restype = restype.ToString(),
                rescode = seqid.ToString(),
                logcontent = string.Format("{0} {1} 资源库[{2}]资源编号[{3}]", userid > 0 ? "用户" + userid.ToString() : "游客", columnname, restype, seqid),
                creator = userid,
                createdtime = DateTime.Now
            };
            DAL.BaseDAL.WriteUserLog(log, out errmsg);
            return true;
        }

        internal static List<Options> GetDicOptions(string dicType)
        {
            Dictionary<string, List<base_dics>> diclist = FreelyListCache.Cache_DicsList;
            List<Options> options = new List<Options>();
            if (!diclist.ContainsKey(dicType))
            {
                Type typeenum = GlobalDic.GetType(dicType);
                if (typeenum != null)  //枚举字典中存在
                {
                    options = GlobalDic.GetEnumList(dicType); //获取枚举字典选项
                    foreach (Options op in options)
                    {
                        if (!diclist.ContainsKey(dicType))
                            diclist.Add(dicType, new List<base_dics>() { new base_dics() { dicname = op.text, dicvalue = op.id, dictype = dicType } });
                        else
                            diclist[dicType].Add(new base_dics() { dicname = op.text, dicvalue = op.id, dictype = dicType });
                    }
                    return options;
                }
            }
            if (diclist.ContainsKey(dicType))
                foreach (base_dics dic in diclist[dicType])
                {
                    options.Add(new Options() { id = dic.dicvalue, text = dic.dicname });
                }
            return options;
        }
        internal static string GetDicName(List<Options> options, dynamic value)
        {
            if (value == null) return null;
            string valuestr = value.ToString();
            if (string.IsNullOrEmpty(valuestr))
                return null;
            if (options == null)
                return null;
            Options opt = options.Find(o => o.id == valuestr);
            if (opt != null)
                return opt.text;
            return null;
        }
        internal static List<Options> GetClassOptions(int parentID)
        {
            if (parentID < 0) parentID = 0;
            Options option = new Options() { id = parentID.ToString(), children = new List<Options>() };
            List<base_classes> classlist = FreelyListCache.Cache_ClassList;
            if (classlist != null)
            {
                SetClassesChildren(option, classlist);
            }
            return option.children;
        }

        private static void SetClassesChildren(Options parentOption, List<base_classes> list)
        {
            List<base_classes> classesChildren = list.FindAll(
                delegate (base_classes c)
                {
                    return c.parentclassid.ToString() == parentOption.id;
                });
            Options option;
            foreach (base_classes classes in classesChildren)
            {
                option = new Options { id = classes.classid.ToString(), text = classes.classname, children = new List<Options>() };
                parentOption.children.Add(option);
                SetClassesChildren(option, list);
            }
        }
        internal static bool WriteUserLog(base_userlogs userlog, out string errmsg)
        {
            return DAL.BaseDAL.WriteUserLog(userlog, out errmsg);
        }

        internal static int GetFullPathResourceCheckCost(int restype, long seqid, int userid, int customerid, string userIp, out string tablename, out int costtype, out decimal m_price, out decimal m_discount, out string serviceno, out string orderno, out string errmsg)
        {
            return DAL.BaseDAL.GetFullPathResourceCheckCost(restype, seqid, userid, customerid, userIp, out tablename, out costtype, out m_price, out m_discount, out serviceno, out orderno, out errmsg);
        }
        internal static bool PutIdentifyCode(base_identifycode info, out string errmsg)
        {
            return DAL.BaseDAL.PutIdentifyCode(info, out errmsg);
        }
        internal static base_identifycode GetIdentifyCodeInfo(string phone, string email,string code, out string errmsg)
        {
            return DAL.BaseDAL.GetIdentifyCodeInfo(phone,email, code, out errmsg);
        }
        internal static List<base_itemcontents> GetItemEchart(string time,out string errmsg) {
            //List<base_itemcontents> list = DAL.BaseDAL.GetItemEchart(time,out errmsg);
            //return list;
            return DAL.BaseDAL.GetItemEchart(time,out errmsg);
        }
    }
}