using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.RIPSP.Model;
using SqlHelperClass;
using System.Data;
using OperateManager.Models.Resourcedb;

namespace PortalSite.Interface.DAL
{
    public static class UserAccountDAL
    {
        public static void DeleteUser(string userid, out string errmsg)
        {
            string deletesql = string.Format("delete from base_users where userid={0}",userid);
            MySqlHelper.ExecuteCommand(deletesql, out errmsg,null);
        }
        internal static int UserReg(base_user_extend user, out string errmsg)
        {
            if (!string.IsNullOrEmpty(user.email) && CheckUserExist("email", user.email, out errmsg))
            {
                errmsg = "邮箱已存在，请更换邮箱重新注册";
                return 0;
            }
            if (!string.IsNullOrEmpty(user.phone) && CheckUserExist("phone", user.phone, out errmsg))
            {
                errmsg = "手机号码已存在，请更换手机号码重新注册";
                return 0;
            }
            if (!string.IsNullOrEmpty(user.username))
            {
                if (CheckUserExist("username", user.username, out errmsg))
                {
                    errmsg = "用户名已存在，请更换用户名重新注册";
                    return 0;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(user.phone))
                {
                    user.username = string.Format("phonereg_{0}", user.phone);
                }
                else if (!string.IsNullOrEmpty(user.email))
                {
                    user.username = string.Format("emailreg_{0}", user.email.Replace("@", "_").Replace(".", "_"));
                }
                else
                {
                    errmsg = "用户名、邮箱和手机号码至少填写一项";
                    return 0;
                }
            }
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr;
            pars.Clear();
            pars.Add(new DataParameter("username", user.username));
            pars.Add(new DataParameter("passwd", user.passwd));
            pars.Add(new DataParameter("nickname", user.nickname));
            pars.Add(new DataParameter("realname", user.realname));
            pars.Add(new DataParameter("usertype", user.usertype));
            pars.Add(new DataParameter("userlevel", user.userlevel));
            pars.Add(new DataParameter("balance", 0));
            pars.Add(new DataParameter("score", 0));
            pars.Add(new DataParameter("photo", user.photo));
            pars.Add(new DataParameter("phone", user.phone));
            pars.Add(new DataParameter("email", user.email));
            pars.Add(new DataParameter("country", user.country));
            pars.Add(new DataParameter("areacode", user.areacode));
            pars.Add(new DataParameter("sex", user.sex));
            pars.Add(new DataParameter("birthday", user.birthday));
            pars.Add(new DataParameter("createdtime", DateTime.Now));
            pars.Add(new DataParameter("sourcetype", user.sourcetype));
            pars.Add(new DataParameter("sourceremarks", user.sourceremarks));
            pars.Add(new DataParameter("customerid", user.customerid));
            pars.Add(new DataParameter("status", user.status));
            pars.Add(new DataParameter("period", user.period));
            pars.Add(new DataParameter("grade", user.grade));
            pars.Add(new DataParameter("classname", user.classname));
            pars.Add(new DataParameter("school", user.school));
            sqlstr = @"insert into base_users (
                                              username,passwd,nickname,
                                              realname,usertype,userlevel,
                                              balance,score,photo,phone,
                                              email,country,areacode,
                                              sex,birthday,createdtime,
                                              sourcetype,sourceremarks,
                                              customerid,status,period,grade,classname,school
                                              ) values 
                                              (
                                               @username,@passwd,@nickname,@realname,
                                               @usertype,@userlevel,@balance,@score,
                                               @photo,@phone,@email,@country,@areacode,
                                               @sex,@birthday,@createdtime,@sourcetype,
                                               @sourceremarks,@customerid,@status,
                                               @period,@grade,@classname,@school
                                              )";
            string useridstr = MySqlHelper.ExecuteInsert(sqlstr, out errmsg, pars.ToArray());
            if (!string.IsNullOrEmpty(errmsg))
            {
                return -1;
            }
            int userid;
            int.TryParse(useridstr, out userid);
            return userid;
        }

        internal static bool UserFindPasswd(base_users userinfo, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from base_users where status>0 ";
            if (!string.IsNullOrEmpty(userinfo.phone))
            {
                sqlstr += " and phone=@phone";
                pars.Add(new DataParameter("phone", userinfo.phone));
            }
            else if (!string.IsNullOrEmpty(userinfo.email))
            {
                sqlstr += " and email=@email";
                pars.Add(new DataParameter("email", userinfo.email));
            }
            base_users user = MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg, pars.ToArray());
            if (!string.IsNullOrEmpty(errmsg))
            {
                return false;
            }
            if (user == null)
            {
                errmsg = "手机号码或邮件账号不存在";
                return false;
            }
            sqlstr = "update base_users set passwd=@passwd where userid=@userid ";
            pars.Clear();
            pars.Add(new DataParameter("passwd", userinfo.passwd));
            pars.Add(new DataParameter("userid", user.userid));
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars.ToArray()) > 0;
        }

        private static bool CheckUserExist(string namestr, string valuestr, out string errmsg)
        {
            DataParameter[] pars = new DataParameter[]
            {
                new DataParameter("valuestr", valuestr)
            };
            string sqlstr = string.Format("select count(1) from base_users where {0}=@valuestr and status!=-1", namestr);
            return MySqlHelper.GetRecCount(sqlstr, out errmsg, pars.ToArray()) == 0 ? false : true;
        }

        internal static base_users UserLogin(string phone, string email, string username, string passwd, string jgid, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("passwd", passwd));
            string sqlstr = "select * from base_users where status>0 and passwd=@passwd ";
            if (!string.IsNullOrEmpty(phone))
            {
                sqlstr += " and phone=@phone";
                pars.Add(new DataParameter("phone", phone));
            }
            else if (!string.IsNullOrEmpty(email))
            {
                sqlstr += " and email=@email";
                pars.Add(new DataParameter("email", email));
            }
            else if (!string.IsNullOrEmpty(username))
            {
                sqlstr += " and username=@username";
                pars.Add(new DataParameter("username", username));
            }
            else
            {
                errmsg = "参数错误";
                return null;
            }
            base_users user = MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg, pars.ToArray());
            if (!string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            if (user == null)
            {
                errmsg = "账号或密码错误";
                return null;
            }
            return user;
        }

        internal static base_users GetUserInfoByOther(base_users user, out string errmsg)
        {
            string sqlstr = "select a.userid,a.username,a.nickname,a.realname,a.usertype,a.customerid,a.sourcetype,a.sourceremarks,a.photo from base_users a,base_otherloginbind b where a.UserID=b.userid and b.bindtype=@bindtype and b.code=@code";
            DataParameter[] pars = new DataParameter[]
            {
                new DataParameter("bindtype",user.sourceremarks),
                new DataParameter("code",user.sourcetypename)
            };
            base_users userinfo = MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg, pars);
            if (!string.IsNullOrEmpty(errmsg))
                return null;
            if (userinfo != null && userinfo.userid > 0)
                return userinfo;
            MySqlTransactionHelper tran = new MySqlTransactionHelper();
            if (tran.TransactionBegin(out errmsg))
            {
                sqlstr = "insert into base_users (username,nickname,passwd,usertype,photo,sex,createdtime,sourcetype,sourceremarks,customerid,status) values (@username,@nickname,@passwd,@usertype,@photo,@sex,@createdtime,@sourcetype,@sourceremarks,0,1)";
                pars = new DataParameter[]
                {
                    new DataParameter("@username",user.username),
                    new DataParameter("@nickname",user.nickname),
                    new DataParameter("@passwd",user.passwd),
                    new DataParameter("@usertype",user.usertype),
                    new DataParameter("@photo",user.photo),
                    new DataParameter("@sex",user.sex),
                    new DataParameter("@createdtime",user.createdtime),
                    new DataParameter("@sourcetype",user.sourcetype),
                    new DataParameter("@sourceremarks",user.sourceremarks)
                };
                string userIdstr = tran.TransactionExecuteInsert(sqlstr, out errmsg, pars);
                int userId;
                if (!string.IsNullOrEmpty(errmsg) || !int.TryParse(userIdstr, out userId) || userId < 1)
                {
                    string err;
                    tran.TransactionRollback(out err);
                    return null;
                }
                user.userid = userId;
                sqlstr = "insert into base_otherloginbind (bindtype,userid,code,token,createdtime) values (@bindtype,@userid,@code,@token,@createdtime)";
                pars = new DataParameter[]
               {
                    new DataParameter("@bindtype",user.sourceremarks),
                    new DataParameter("@userid",user.userid),
                    new DataParameter("@code",user.sourcetypename),
                    new DataParameter("@token",user.usertypename),
                    new DataParameter("@createdtime",user.createdtime)
               };
                tran.TransactionExecuteCommand(sqlstr, out errmsg, pars);
                if (!string.IsNullOrEmpty(errmsg))
                {
                    string err;
                    tran.TransactionRollback(out err);
                    return null;
                }
                bool issuc = tran.TransactionCommit(out errmsg);
                if (!issuc)
                {
                    string err;
                    tran.TransactionRollback(out err);
                    return null;
                }
                return user;
            }
            return null;
        }
        #region 用户中心
        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static base_users GetUserInfo(int userid, out string errmsg)
        {
            string sqlstr = string.Format("select * from base_users where userid={0}", userid);
            return MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg);
        }
        /// <summary>
        /// 用户中心 修改基础信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool PutUserInfo(base_users info,out string errmsg)
        {
            MySqlTransactionHelper tran = new MySqlTransactionHelper();
            if (!tran.TransactionBegin(out errmsg))
            {
                return false;
            }

            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("@userid", info.userid));
            pars.Add(new DataParameter("@nickname", info.nickname));
            pars.Add(new DataParameter("@sex", info.sex));
            pars.Add(new DataParameter("@birthday", info.birthday));
            pars.Add(new DataParameter("@phone", info.phone));
            pars.Add(new DataParameter("@email", info.email));
            pars.Add(new DataParameter("@realname", info.realname));            //pars.Add(new DataParameter("@realname", info.degree));

            pars.Add(new DataParameter("@photo", info.photo));
      
            string sqlstr = "update base_users set nickname=@nickname,sex=@sex,birthday=@birthday,phone=@phone,email=@email,realname=@realname,photo=@photo where userid=@userid";
            tran.TransactionExecuteCommand(sqlstr, out errmsg, pars);
            if (!string.IsNullOrEmpty(errmsg))
            {
                string err;
                tran.TransactionRollback(out err);
                return false;
            }
            return tran.TransactionCommit(out errmsg);
        }
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static base_users GetUserPwd(int userid,string pwd,out string errmsg)
        {
            string sqlstr = string.Format("select * from base_users where userid={0} and passwd='{1}'", userid, pwd);
            return MySqlHelper.GetDataInfo<base_users>(sqlstr, out errmsg);
        }
       /// <summary>
       /// 修改密码
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="pwd"></param>
       /// <param name="errmsg"></param>
       /// <returns></returns>
        internal static bool PutUserPwd(int userid, string pwd, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("@passwd", pwd));
            pars.Add(new DataParameter("@userid", userid));
            string sqlstr = "update base_users set passwd=@passwd where userid=@userid";
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;

        }
        
        internal static bool  PutUserInforname(int userid, string realname, string email, string phone, string opinion, int period,int grade,string classname,string school,out string errmsg)
        {
            MySqlTransactionHelper tran = new MySqlTransactionHelper();
            if (!tran.TransactionBegin(out errmsg))
            {
                return false;
            }
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("@realname", realname));
            pars.Add(new DataParameter("@email", email));
            pars.Add(new DataParameter("@phone", phone));
            pars.Add(new DataParameter("@userid", userid));
            pars.Add(new DataParameter("@period", period));
            pars.Add(new DataParameter("@grade", grade));
            pars.Add(new DataParameter("@classname", classname));
            pars.Add(new DataParameter("@school", school));
            string sqlstr = "update base_users set realname=@realname,email=@email,phone=@phone,period=@period,grade=@grade,classname=@classname,school=@school where userid=@userid";
            tran.TransactionExecuteCommand(sqlstr, out errmsg, pars);
            if (!string.IsNullOrEmpty(errmsg))
            {
                string err;
                tran.TransactionRollback(out err);
                return false;
            }
            List<DataParameter> pars2 = new List<DataParameter>();
            pars2.Add(new DataParameter("@opinion", opinion));
            pars2.Add(new DataParameter("@userid", userid));
            string sqlstr2 = "insert into chosen_feedback(userid,opinion) values (@userid,@opinion)";
            tran.TransactionExecuteCommand(sqlstr2, out errmsg, pars2);
            if (!string.IsNullOrEmpty(errmsg))
            {
                string err;
                tran.TransactionRollback(out err);
                return false;
            }
            return tran.TransactionCommit(out errmsg);
        }
        //internal static bool PutFeedBack(int userid, string opinion, out string errmsg)
        //{
        //    List<DataParameter> pars = new List<DataParameter>();
        //    pars.Add(new DataParameter("@opinion", opinion));
        //    pars.Add(new DataParameter("@userid", userid));
        //    string sqlstr = "insert into chose_feedback(userid,opinion) values (@userid,@opinion)";
        //    return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;

        //}
        /// <summary>
        ///用户中心-我的订单 
        /// </summary>
        /// <param name="useid"></param>
        /// <param name="rows"></param>
        /// <param name="sortexp"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetUserOrdInfo(int userid, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            string sqlcount;
            int offset = 0;
            total = 0;
            if (page > 0) {
                offset = (page - 1) * rows;
                sqlcount = string.Format("select count(1) from chosen_orderinfo where userid={0} and status>=0", userid);

                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
            string sqlstr = string.Format("select * from chosen_orderinfo where 1=1 and userid={0} and status>=0 order by {1}  limit {2},{3}", userid, sortexp, offset, rows);
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }

        internal static List<chosen_orderinfo> GetUserOrdersList(int userid,int timetype, int page, int rows, out int total, out string errmsg)
        {
            string sqlstr = "";
            string sqlcount = "";
            if (timetype == 1)
            {
                sqlstr = "SELECT * from chosen_orderinfo where status>=0 and userid="+userid+ " and DATE_FORMAT(createdtime, '%Y%m')= DATE_FORMAT(CURDATE() , '%Y%m') ";
                sqlcount = "SELECT count(1) from chosen_orderinfo where status>=0 and userid=" + userid + " and DATE_FORMAT(createdtime, '%Y%m')= DATE_FORMAT(CURDATE() , '%Y%m') ";
            }
            else if (timetype == 2)
            {
               
                sqlstr = "SELECT * from chosen_orderinfo where status>=0 and userid=" + userid + " and  QUARTER(createdtime) = QUARTER(now()) ";
                sqlcount = "SELECT count(1) from chosen_orderinfo where status>=0 and userid=" + userid + " and  QUARTER(createdtime) = QUARTER(now()) ";
            }
            else if (timetype == 3) {
                sqlstr = "SELECT * from chosen_orderinfo where status>=0 and userid=" + userid + " and  YEAR(createdtime)=YEAR(NOW()) ";
                sqlcount = "SELECT count(1) from chosen_orderinfo where status>=0 and userid=" + userid + " and  YEAR(createdtime)=YEAR(NOW()) ";
            }
            
            total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
            if (total <= 0)
                return new List<chosen_orderinfo>();

            sqlstr = string.Format("{0} ORDER BY createdtime desc limit {1},{2}", sqlstr, (page - 1) * rows, rows);
            return MySqlHelper.GetDataList<chosen_orderinfo>(sqlstr, out errmsg);
        }
        /// <summary>
        /// 添加到我的订单中
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool AdduserOrders(chosen_orderinfo info,out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            pars.Add(new DataParameter("orderno", info.orderno));
            pars.Add(new DataParameter("ordername", info.ordername));
            pars.Add(new DataParameter("userid", info.userid));
            pars.Add(new DataParameter("customerid", info.customerid));
            pars.Add(new DataParameter("serviceno", info.serviceno));
            pars.Add(new DataParameter("feetype", info.feetype));
            pars.Add(new DataParameter("m_price", info.m_price));
            pars.Add(new DataParameter("s_price", info.s_price));
            pars.Add(new DataParameter("addressid", info.addressid));
            pars.Add(new DataParameter("ctype", info.ctype));
            pars.Add(new DataParameter("paytype", info.paytype));
            pars.Add(new DataParameter("paybank", info.paybank));
            pars.Add(new DataParameter("payno", info.payno));
            pars.Add(new DataParameter("createdtime", info.createdtime));
            pars.Add(new DataParameter("paytime", info.paytime));
            pars.Add(new DataParameter("payresulttime", info.payresulttime));
            pars.Add(new DataParameter("deliverytime", info.deliverytime));
            pars.Add(new DataParameter("deliveryoktime", info.deliveryoktime));
            pars.Add(new DataParameter("logisticaltype", info.logisticaltype));
            pars.Add(new DataParameter("logisticalcode", info.logisticalcode));
            pars.Add(new DataParameter("status", info.status));
            pars.Add(new DataParameter("creator", info.creator));
            pars.Add(new DataParameter("terminaltype", info.terminaltype));
            string sqlstr = "insert into chosen_orderinfo (orderno,ordername,userid,customerid,serviceno,feetype,m_price,s_price,addressid,ctype,paytype,paybank,payno,createdtime,paytime,payresulttime,deliverytime,deliveryoktime,logisticaltype,logisticalcode,status,creator,terminaltype) values (@orderno,@ordername,@userid,@customerid,@serviceno,@feetype,@m_price,@s_price,@addressid,@ctype,@paytype,@paybank,@payno,@createdtime,@paytime,@payresulttime,@deliverytime,@deliveryoktime,@logisticaltype,@logisticalcode,@status,@creator,@terminaltype) ";
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
        }
        /// <summary>
        /// 用户中心 批量删除用户订单信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderno"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool UpdateUserOrdersList(int userid, string orderno, out string errmsg)
        {
            string sqlstr = "UPDATE chosen_orderinfo set status=-1 where userid=" + userid + " and orderno in('"+orderno+"')";
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool DeletedUserFavorList(int userid, string seqid, out string errmsg)
        {
            string sqlstr = "delete from chosen_favorites where  userid=" + userid + " and seqid in(" + seqid + ")";
            return MySqlHelper.ExecuteCommand(sqlstr, out errmsg) > 0;
        }
        /// <summary>
        ///用户中心-我的收藏 
        /// </summary>
        /// <param name="useid"></param>
        /// <param name="rows"></param>
        /// <param name="sortexp"></param>
        /// <param name="page"></param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static DataTable GetUserFavor(int userid,int restype,string sortexp, int rows, int page, out int total, out string errmsg)
        {
            string sqlcount;
            string sqlstr;
            int offset = 0;
            total = 0;
            if (page > 0)
            {
                offset = (page - 1) * rows;
                if (restype != 0)
                {
                    sqlcount = string.Format("select count(1) from chosen_favorites f,db_datalibrarys d  where f.userid={0} and d.databaseid=f.restype and f.restype={1}", userid, restype);
                }
                else {
                    sqlcount = string.Format("select count(1) from chosen_favorites f,db_datalibrarys d where f.userid={0} and d.databaseid=f.restype", userid);
                }
               

                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
            if (restype != 0)
            {
                sqlstr = string.Format("select f.*,d.databasecname as restypename from chosen_favorites f,db_datalibrarys d where 1=1 and f.userid={0} and d.databaseid=f.restype  and f.restype={1} order by {2}  limit {3},{4}", userid,restype, sortexp, offset, rows);
            }
            else {
                sqlstr = string.Format("select f.*,d.databasecname as restypename from chosen_favorites f,db_datalibrarys d where 1=1 and  f.userid={0} and d.databaseid=f.restype  order by {1}  limit {2},{3}", userid, sortexp, offset, rows);
            }
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        /// <summary>
        /// 用户收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="restype"></param>
        /// <param name="seqid"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        internal static bool ResourcesFavorites(int userid, int restype, int seqid, string resremark, string classid,out string errmsg)
        {
            string sqlstr = string.Format("select count(1) as count from chosen_favorites where userid={0} and restype='{1}' and rescode={2}", userid, restype, seqid);
            int count = (int)MySqlHelper.GetRecCount(sqlstr, out errmsg);
            if (count == 0)
            {

                sqlstr = string.Format("insert into chosen_favorites (userid,restype,rescode,resremark,createdtime ) values (@userid,@restype,@rescode,@resremark,@createdtime)");
                DataParameter[] pars = new DataParameter[] {
                    new DataParameter("userid", userid),
                    new DataParameter("restype", restype),
                    new DataParameter("rescode", seqid),
                    new DataParameter("resremark",resremark),
                    //new DataParameter("classid",classid),
                    new DataParameter("createdtime", DateTime.Now)
                };
                return MySqlHelper.ExecuteCommand(sqlstr, out errmsg, pars) > 0;
            }
            return true;
        }

        internal static DataTable GetUserHotkey(int userid, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            string sqlcount;
            string sqlstr;
            int offset = 0;
            total = 0;
            if (page > 0)
            {
               offset = (page - 1) * rows;
               sqlcount = string.Format("select count(1) from base_hotkeylog where userid={0} ", userid);
               total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg);
                if (total == 0)
                    return null;
            }
        
            sqlstr = string.Format("select * from base_hotkeylog  where 1=1 and userid={0} order by {1}  limit {2},{3}", userid, sortexp, offset, rows);
          
            return MySqlHelper.GetDataTable(sqlstr, out errmsg);
        }
        #endregion
    }
}