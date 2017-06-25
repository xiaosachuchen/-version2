using Microsoft.RIPSP.Model;
using SqlHelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PortalSite.Interface.DAL
{
    public static class BaseDAL
    {
        /// <summary>
        /// 用户日志记录
        /// </summary>
        /// <param name="userlog"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool WriteUserLog(base_userlogs userlog, out string errmsg)
        {
            string sqlstr = "insert into base_userlogs(logtype,restype,rescode,logcontent,creator,createdtime) values (@logtype,@restype,@rescode,@logcontent,@creator,@createdtime)";
            DataParameter[] pars = new DataParameter[]
            {
                new DataParameter("logtype",userlog.logtype),
                new DataParameter("restype",userlog.restype),
                new DataParameter("rescode",userlog.rescode),
                new DataParameter("logcontent",userlog.logcontent),
                new DataParameter("creator",userlog.creator),
                new DataParameter("createdtime",userlog.createdtime),
            };
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
        }

        internal static bool UpdateDoLog(string tablename, string columnname, int seqid, out string errmsg)
        {
            string sqlstr = string.Format("update {0} set {1}={1}+1 where seqid={2}", tablename, columnname, seqid);
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
        }

        /// <summary>
        /// 获取栏目数据
        /// </summary>
        /// <param name="itemmark"></param>
        /// <param name="ispage"></param>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static List<base_itemcontents> GetItemContents(string itemmark, bool ispage, int offset, int rows, out int total, out string errmsg)
        {
            total = 0;
            errmsg = null;
            DataParameter[] pars = new DataParameter[]
            {
                new DataParameter("itemmark",itemmark)
            };
            string sqlstr = string.Format("select * from base_itemcontents where itemmark=@itemmark order by createdtime DESC  limit {0},{1}", offset, rows);
            if (ispage)
            {
                string sqlcount = "select count(1) from base_itemcontents where itemmark=@itemmark ";
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars);
            }
            if (ispage && total == 0)
                return new List<base_itemcontents>();
            return MySqlHelper.GetDataList<base_itemcontents>(sqlstr, out errmsg, pars);
        }
        internal static List<base_itemcontents> GetItemEchart(string time,out string errmsg)
        {
            
            errmsg = null;
        
            string sqlstr = string.Format(" SELECT title,count(seqid) as rescode,group_concat(abstracts SEPARATOR '|') as abstracts ,author as restype FROM base_itemcontents WHERE itemmark = 'wx_yjqspicdata'  and aboutdate='"+ time + "'  GROUP BY title");
          
            return MySqlHelper.GetDataList<base_itemcontents>(sqlstr, out errmsg);
        }
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="dicType">null取字典类型，length>0 取字典值，length=0所有 </param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_dics> GetDicList(string dicType, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_dics ";
            if (dicType == null)
                sqlstr += " where isdictype=1";
            else if (dicType.Length > 0)
            {
                sqlstr += " where isdictype=0 and dictype=@dictype ";
                pars.Add(new DataParameter("dictype", dicType));
            }
            sqlstr += " order by isdictype desc,sorts asc ";
            return MySqlHelper.GetDataList<base_dics>(sqlstr, out errmsg, pars.ToArray());
        }
        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<base_classes> GetClassList(int parentId, out string errmsg)
        {
            string sqlstr = "select * from base_classes ";
            if (parentId >= 0)
                sqlstr += " and parentclassid=" + parentId;
            sqlstr += " order by parentclassid asc,sorts asc ";
            return MySqlHelper.GetDataList<base_classes>(sqlstr, out errmsg);
        }

        #region 资源计费判断及扣费
        /// <summary>
        /// 判断全文资源计费情况
        /// </summary>
        /// <param name="restype">资源库id</param>
        /// <param name="seqid">资源id</param>
        /// <param name="userid">用户id</param>
        /// <param name="customerid">客户id</param>
        /// <param name="userIp">用户ip</param>
        /// <param name="tablename">资源库名</param>
        /// <param name="costtype">计费类型 【0免费资源；1 客户计时订单；2 客户计量订单；3 用户订单；】</param>
        /// <param name="m_price">单条价格</param>
        /// <param name="m_discount">单条折扣</param>
        /// <param name="serviceno">客户服务编号</param>
        /// <param name="orderno">客户服务订单号</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static int GetFullPathResourceCheckCost(int restype, long seqid, int userid, int customerid, string userIp, out string tablename, out int costtype, out decimal m_price, out decimal m_discount, out string serviceno, out string orderno, out string errmsg)
        {
            errmsg = null;
            tablename = null;
            costtype = 0;
            m_price = 0;
            m_discount = 0;
            serviceno = null;
            orderno = null;
            string sqlstr = string.Format("select mouldid,databasename,databasecname,isware from db_datalibrarys where databaseid={0}", restype);
            db_datalibrarys dbinfo = MySqlHelper.GetDataInfo<db_datalibrarys>(sqlstr, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                errmsg = "查询资源库出错" + errmsg;
                return -1;
            }
            if (dbinfo == null)
            {
                errmsg = "资源库参数错误";
                return -1;
            }
            tablename = dbinfo.databasename;
            DataTable dt;

            //首先判断是否商品库中的资源，如果是，判断单条资源是否收费 如果是，资源是否在用户已完成的订单中 （以前购买过，可以直接使用）
            string shopname = null;
            if (dbinfo.isware == 1 && userid > 0)
            {
                sqlstr = string.Format("select m_price,m_discount,title from {0} where seqid={1}", tablename, seqid);
                dt = MySqlHelper.GetDataTable(sqlstr, out errmsg);
                if (!string.IsNullOrEmpty(errmsg) || dt == null)
                {
                    errmsg = "查询资源费用信息出错" + errmsg;
                    return -1;
                }
                if (dt.Rows.Count == 0)
                {
                    errmsg = "资源不存在";
                    return -1;
                }
                if (!decimal.TryParse(dt.Rows[0][0].ToString(), out m_price) || !decimal.TryParse(dt.Rows[0][1].ToString(), out m_discount))
                {
                    errmsg = "查询资源价格及折扣信息出错";
                    return -1;
                }
                if (m_price * m_discount == 0) //免费资源，直接使用
                {
                    costtype = 0;
                    return 1;
                }
                shopname = dt.Rows[0][2].ToString();
                sqlstr = string.Format("select count(1) from chosen_orderinfo a,chosen_ordershop b where a.orderno=b.orderno and a.status=9 and a.userid={0} and b.restype='{1}' and rescode={2}", userid, tablename, seqid);
                long count = MySqlHelper.GetRecCount(sqlstr, out errmsg);
                if (!string.IsNullOrEmpty(errmsg))
                {
                    errmsg = "查询用户订单信息出错" + errmsg;
                    return -1;
                }
                if (count > 0) //用户购买过，直接使用
                {
                    costtype = 3;
                    return 1;
                }
            }
            // 用户是否在客户已购买并有效的授权服务当中   
            if (customerid > 0)
            {
                sqlstr = string.Format("select a.orderno,a.serviceno,b.stype,b.starttime,b.endtime,b.maxnum,b.leftnum,d.rulename,d.rulevalue from chosen_orderinfo a,chosen_serviceinfo b,chosen_servicepermit c,chosen_servicecont d where a.serviceno=b.serviceno and a.serviceno=c.serviceno and a.serviceno=d.serviceno and a.status=9 and a.customerid={0} and d.restype='{1}' ", customerid, tablename);
                dt = MySqlHelper.GetDataTable(sqlstr, out errmsg);
                if (!string.IsNullOrEmpty(errmsg))
                {
                    errmsg = "查询资源服务信息出错" + errmsg;
                    return -1;
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    //资源是否在服务内容当中
                    orderno = dt.Rows[0]["orderno"].ToString();
                    serviceno = dt.Rows[0]["serviceno"].ToString();
                    costtype = int.Parse(dt.Rows[0]["stype"].ToString());
                    return 1;
                }
            }
            //都不符合，生成用户订单，返回需要计费信息
            if (m_price * m_discount > 0)
            {
                chosen_orderinfo order = new chosen_orderinfo();
                order.GetNo();
                order.ordername = string.Format("订购资源库 {0} 的资源 {1} 的全文", dbinfo.databasecname, seqid);


                MySqlTransactionHelper tran = new MySqlTransactionHelper();
                if (tran.TransactionBegin(out errmsg))
                {
                    //写入用户订单
                    List<DataParameter> pars = new List<DataParameter>();
                    pars.Add(new DataParameter("orderno", order.orderno));
                    pars.Add(new DataParameter("ordername", order.ordername));
                    pars.Add(new DataParameter("userid", userid));
                    pars.Add(new DataParameter("m_price", m_price));
                    pars.Add(new DataParameter("s_price", m_price * m_discount));
                    pars.Add(new DataParameter("ctype", 1));
                    pars.Add(new DataParameter("paytype", 1));
                    pars.Add(new DataParameter("status", 1));
                    pars.Add(new DataParameter("Creator", userid));
                    pars.Add(new DataParameter("createdtime", DateTime.Now));
                    string sqlor = "insert into chosen_orderinfo (orderno,ordername,userid,m_price,s_price,ctype,paytype,status,creator,createdtime) values (orderno,ordername,userid,m_price,s_price,ctype,paytype,status,creator,createdtime) ";
                    tran.TransactionExecuteCommand(sqlor, out errmsg, pars);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        string err;
                        tran.TransactionRollback(out err);
                        return -1;
                    }
                    pars.Clear();
                    pars.Add(new DataParameter("shopname", string.IsNullOrEmpty(shopname) ? order.ordername : shopname));
                    pars.Add(new DataParameter("orderno", order.orderno));
                    pars.Add(new DataParameter("restype", restype));
                    pars.Add(new DataParameter("rescode", seqid));
                    pars.Add(new DataParameter("prices", m_price));
                    pars.Add(new DataParameter("thumbnail", null));
                    pars.Add(new DataParameter("shopnum", 1));
                    sqlstr = "insert into chosen_ordershop (shopname,orderno,restype,rescode,prices,thumbnail,shopnum) values (@shopname,@orderno,@restype,@rescode,@prices,@thumbnail,@shopnum) ";
                    tran.TransactionExecuteCommand(sqlstr, out errmsg, pars);
                    if (!string.IsNullOrEmpty(errmsg))
                    {
                        string err;
                        tran.TransactionRollback(out err);
                        return -1;
                    }
                    bool issuc = tran.TransactionCommit(out errmsg);
                    if (!string.IsNullOrEmpty(errmsg) || !issuc)
                    {
                        errmsg = "提交订单出错：" + errmsg;
                        return -1;
                    }                       
                    costtype = 3;
                    orderno = order.orderno;
                    return 0;
                }
                return -1;
            }
            else //免费资源
            {
                costtype = 0;
                return 1;
            }
        }
        #endregion

        #region 插入验证码
        public static bool PutIdentifyCode(base_identifycode info, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();

            pars.Add(new DataParameter("phone", info.phone));
            pars.Add(new DataParameter("email", info.email));
            pars.Add(new DataParameter("code", info.code));
            pars.Add(new DataParameter("sendtime", info.sendtime));
            pars.Add(new DataParameter("endtime", info.endtime));
            string sqlstr = "insert into base_identifycode (phone,email,code,sendtime,endtime) values (@phone,@email,@code,@sendtime,@endtime)";
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars.ToArray()) > 0;
        }
        #endregion

        #region 验证用户提交的验证码
        public static base_identifycode GetIdentifyCodeInfo(string phone, string email, string code, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("code", code));
            string sqlstr = "select * from base_identifycode where code=@code";
            if (!string.IsNullOrEmpty(phone))
            {
                pars.Add(new DataParameter("phone", phone));
                sqlstr += " and phone=@phone ";
            }
            else
            {
                pars.Add(new DataParameter("email", email));
                sqlstr += " and email=@email ";
            }
            sqlstr += " order by endtime desc limit 1";
            return MySqlHelper.GetDataInfo<base_identifycode>(sqlstr, out errmsg, pars.ToArray());
        }
        #endregion
      
    }
}