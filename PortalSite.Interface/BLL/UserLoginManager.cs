using Microsoft.RIPSP.Model;
using Newtonsoft.Json;
using PortalSite.Interface.Models;
using PublicHelperClass;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Data;
using OperateManager.Models.Resourcedb;

namespace PortalSite.Interface.BLL
{
    public class UserLoginManager
    {
        public string Errmsg = null;
        public UserInfoViewModel LoginUser = null;
        public UserLoginManager()
        {
            LoginUser = Readcookie(ref Errmsg);
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public int UserReg(base_user_extend user, out string errmsg)
        {
            if (user.usertype == null)
                user.usertype = 1;
            if (user.sourcetype == null)
                user.sourcetype = 3;
            if (string.IsNullOrEmpty(user.passwd))
                user.passwd = Models.GlobalParameters.DefaultUserPasswd;
            if (user.createdtime == null)
                user.createdtime = DateTime.Now;
            if (user.customerid == null)
                user.customerid = 0;
            user.status = 1;
            if (string.IsNullOrEmpty(user.phone) && string.IsNullOrEmpty(user.email))
            {
                errmsg = "手机号码和邮箱至少填写一个";
                return 0;
            }
            if (!ValidatorClass.Validator(user.phone, CheckType.Mobile, null, true))
            {
                errmsg = "手机号码错误";
                return 0;
            }
            if (!ValidatorClass.Validator(user.email, CheckType.Email, null, true))
            {
                errmsg = "邮箱错误";
                return 0;
            }
            if (!ValidatorClass.Validator(user.username, CheckType.Loginid, null, true))
            {
                errmsg = "用户名错误";
                return 0;
            }
            return DAL.UserAccountDAL.UserReg(user, out errmsg);
        }
        internal bool UserFindPasswd(base_users userinfo, out string errmsg)
        {
            return DAL.UserAccountDAL.UserFindPasswd(userinfo, out errmsg);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="passwd"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public UserInfoViewModel UserLogin(string loginname, string passwd, string jgid, out string errmsg)
        {
            long t;
            if (loginname.IndexOf("@") > 0)
                return UserLogin(null, loginname, null, passwd, jgid, out errmsg);
            else if (loginname.Length == 11 && long.TryParse(loginname, out t))
                return UserLogin(loginname, null, null, passwd, jgid, out errmsg);
            else
                return UserLogin(null, null, loginname, passwd, jgid, out errmsg);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="passwd"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public UserInfoViewModel UserLogin(string phone, string email, string username, string passwd, string jgid, out string errmsg)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(username) && string.IsNullOrEmpty(passwd))
            {
                errmsg = "请输入完整的账号密码";
                return null;
            }
            base_users userInfo = DAL.UserAccountDAL.UserLogin(phone, email, username, passwd, jgid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            if (userInfo == null || userInfo.userid == 0)
            {
                errmsg = "用户名或密码错误";
                return null;
            }
            UserInfoViewModel user = new UserInfoViewModel()
            {
                UserID = (int)userInfo.userid,
                UserName = userInfo.username,
                NickName = !string.IsNullOrEmpty(userInfo.nickname) ? userInfo.nickname : (!string.IsNullOrEmpty(userInfo.realname) ? userInfo.realname : userInfo.username),
                RealName = userInfo.realname,
                UserType = (int)userInfo.usertype,
                Customerid = (int)userInfo.customerid,
                SourceType = (int)userInfo.sourcetype,
                SourceRemarks = userInfo.sourceremarks,
                Photo = userInfo.photo,
                LastLoginTime = DateTime.Now
            };
            if (!WriteCookie(user, ref errmsg) || !string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            return user;
        }

        internal UserInfoViewModel GetUserInfoByOther(base_users user, out string errmsg)
        {
            base_users userInfo = DAL.UserAccountDAL.GetUserInfoByOther(user, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            if (userInfo == null || userInfo.userid == 0)
            {
                errmsg = "用户名或密码错误";
                return null;
            }
            UserInfoViewModel uservm = new UserInfoViewModel()
            {
                UserID = (int)userInfo.userid,
                UserName = userInfo.username,
                NickName = !string.IsNullOrEmpty(userInfo.nickname) ? userInfo.nickname : (!string.IsNullOrEmpty(userInfo.realname) ? userInfo.realname : userInfo.username),
                RealName = userInfo.realname,
                UserType = (int)userInfo.usertype,
                Customerid = (int)userInfo.customerid,
                SourceType = (int)userInfo.sourcetype,
                SourceRemarks = userInfo.sourceremarks,
                Photo = userInfo.photo,
                LastLoginTime = DateTime.Now
            };
            if (!WriteCookie(uservm, ref errmsg) || !string.IsNullOrEmpty(errmsg))
            {
                return null;
            }
            return uservm;
        }

        public bool WriteCookie(UserInfoViewModel userInfoVM, ref string errmsg)
        {
            string userData = JsonConvert.SerializeObject(userInfoVM);
            if (string.IsNullOrEmpty(userData))
            {
                errmsg = "序列化错误";
                return false;
            }
            string encTicket = StrFormatClass.Encrypt(userData, ref errmsg);
            if (string.IsNullOrEmpty(encTicket) || !string.IsNullOrEmpty(errmsg))
            {
                return false;
            }
            HttpCookie cookie = new HttpCookie("CIRSUser", encTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                Expires = DateTime.Today.AddDays(1)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

    
        public UserInfoViewModel Readcookie(ref string errmsg)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CIRSUser"];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                errmsg = "未登录";
                return null;
            }
            string decTicket = StrFormatClass.Decrypt(cookie.Value, ref errmsg);
            if (string.IsNullOrEmpty(decTicket) || !string.IsNullOrEmpty(errmsg))
            {
                errmsg = "解密用户认证数据错误";
                return null;
            }
            UserInfoViewModel userData;
            try
            {
                userData = JsonConvert.DeserializeObject<UserInfoViewModel>(decTicket);
            }
            catch
            {
                errmsg = "反序列化错误";
                userData = null;
            }
            return userData;
        }

        public void Logout()
        {
            HttpContext.Current.Response.Cookies.Remove("CIRSUser");
            HttpCookie cookie = HttpContext.Current.Request.Cookies["CIRSUser"];
            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.Today.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        #region 用户中心
        internal static base_users GetUserInfo(int userid,out string errmsg) {
            base_users info = DAL.UserAccountDAL.GetUserInfo(userid,out errmsg);
            
            //info.usertypename = BaseBLL.GetDicOptions("usertype",info.usertype.ToString());
            return info;
        }
        internal static bool PutUserInfo(base_users info, out string errmsg)
        {
            return DAL.UserAccountDAL.PutUserInfo(info,out errmsg);
        }
        internal static base_users GetUserPwd(int userid,string pwd,out string errmsg) {
            return DAL.UserAccountDAL.GetUserPwd(userid,pwd,out errmsg);
        }
        internal static bool  PutUserPwd(int userid, string pwd, out string errmsg)
        {
            return DAL.UserAccountDAL.PutUserPwd(userid, pwd, out errmsg);
        }
        internal static bool PutUserInforname(int userid, string realname, string email, string phone, string opinion, out string errmsg,int period=0,int grade=0,string classname="",string school="")
        {
            return DAL.UserAccountDAL.PutUserInforname(userid, realname, email, phone,opinion,period,grade,classname,school, out errmsg);
        }
      
        internal static DataTable GetUserOrdInfo(int userid,string sortexp, int rows, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            DataTable dt = DAL.UserAccountDAL.GetUserOrdInfo(userid, sortexp, rows, page, out total, out errmsg);
            return dt;
        }
        internal static List<chosen_orderinfo> GetUserOrdersList(int userid,int timetype, int page, int rows, out int total, out string errmsg)
        {
            List<chosen_orderinfo> list = DAL.UserAccountDAL.GetUserOrdersList(userid,timetype, page, rows, out total, out errmsg);
            if (list.Count == 0)
                return list;
            List<Options> OrderStatusList = BaseBLL.GetDicOptions("OrderStatus");
            Options tstatus;
            foreach (chosen_orderinfo item in list) {
                tstatus = OrderStatusList.Find(o => o.id == item.status.ToString());
                if (tstatus != null)
                    item.statusname = tstatus.text;
            }
            return list;
        }
        internal static bool UpdateUserOrdersList(int userid, string orderno, out string errmsg)
        {
            return DAL.UserAccountDAL.UpdateUserOrdersList(userid, orderno, out errmsg);
        }
        internal static bool DeletedUserFavorList(int userid, string seqid, out string errmsg)
        {
            return DAL.UserAccountDAL.DeletedUserFavorList(userid, seqid, out errmsg);
        }
        internal static DataTable GetUserFavor(int userid,int restype, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            DataTable dt = DAL.UserAccountDAL.GetUserFavor(userid,restype, sortexp, rows, page, out total, out errmsg);
            return dt;
        }
        internal static DataTable GetUserHotkey(int userid, string sortexp, int rows, int page, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            DataTable dt = DAL.UserAccountDAL.GetUserHotkey(userid, sortexp, rows, page, out total, out errmsg);
            return dt;
        }
        internal static bool ResourcesFavorites(int userid, int restype, int seqid, string resremark, string classid,out string errmsg)
        {
            return DAL.UserAccountDAL.ResourcesFavorites(userid, restype, seqid, resremark, classid, out errmsg);
        }
        internal static bool AdduserOrders(chosen_orderinfo info, out string errmsg) {
            return DAL.UserAccountDAL.AdduserOrders(info,out errmsg);
        }
        #endregion
    }
}