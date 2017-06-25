using Microsoft.RIPSP.Model;
using PortalSite.Interface.Models;
using PublicHelperClass;
using System;
using System.IO;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using Microsoft.RIPSP.BaseModel;
using OperateManager.Models.Resourcedb;
using LitJson;
using PortalSite.Interface.WebServiceClient;
using PortalSite.Interface.DAL;

namespace PortalSite.Interface.Controllers
{
    public class UserAccountController : ApiController
    {
        private string errmsg = null;
        public BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetCurrentUser")]
        public object GetCurrentUser()
        {
            if (userLoginManager.LoginUser == null)
            {
                return new { Rcode = -2, Rmsg = userLoginManager.Errmsg };
            }
            return new { Rcode = 1, Data = userLoginManager.LoginUser };
        }
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/SetUserLogout")]
        public object SetUserLogout()
        {
            userLoginManager.Logout();
            userLoginManager.LoginUser = null;
            return new { Rcode = 1 };
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Route("UserAccount/UserLogin")]
        public object UserLogin(UserLoginViewModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return new { Rcode = 0, Rmsg = "请输入完整的用户名密码" };
            }
            string errmsg = null;
            user.Password = StrFormatClass.MD5ToDepth(user.Password);
            UserInfoViewModel userInfo = userLoginManager.UserLogin(user.UserName, user.Password, null, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "登录错误" + errmsg };
            else if (userInfo == null || userInfo.UserID < 1)
                return new { Rcode = 0, Rmsg = "用户名或密码错误" };
            else
            {
                base_userlogs log = new base_userlogs()
                {
                    logtype = 22,
                    restype = "0",
                    rescode = userInfo.UserID.ToString(),
                    logcontent = string.Format("登录用户{0}", user.UserName),
                    creator = 0,
                    createdtime = DateTime.Now
                };
                BLL.BaseBLL.WriteUserLog(log, out errmsg);//记录登录日志
                                
                return new { Rcode = 1, Rdata = userInfo };
            }

        }
        /// <summary>
        /// 第三方登录，检测账号是否已绑定现有用户
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost, Route("Sport/Interface/UserAccount/CheckUserIDBind")]
        public object UserLoginByOther(base_users userinfo)
        {
            userinfo.passwd = Models.GlobalParameters.DefaultUserPasswd;
            userinfo.usertype = 1;
            userinfo.userlevel = 1;
            userinfo.sourcetype = 4;
            userinfo.sourceremarks = userinfo.usertypename;
            userinfo.customerid = 0;
            userinfo.status = 1;
            userinfo.createdtime = DateTime.Now;
            UserInfoViewModel userVM = userLoginManager.GetUserInfoByOther(userinfo, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (userVM == null || userVM.UserID < 1)
                return new { Rcode = -1, Rmsg = "绑定用户出错" };
            else
            {
                base_userlogs log = new base_userlogs()
                {
                    logtype = 22,
                    restype = "0",
                    rescode = userVM.UserID.ToString(),
                    logcontent = string.Format("登录用户{0}", userVM.UserName),
                    creator = 0,
                    createdtime = DateTime.Now
                };
                return new { Rcode = 1, Rmsg = "登录成功", Rdata = userVM };
            }

        }
        [HttpPost, Route("UserAccount/UserReg")]
        public object UserReg(base_user_extend userinfo)
        {
            string errmsg = null;
            if (userinfo.yzmcode==null|| userinfo.yzmcode.Length != 6)
            {
                return new { Rcode = -1, Rmsg = "注册失败：请填写验证码" };
            }
            base_identifycode identifycode = BLL.BaseBLL.GetIdentifyCodeInfo(userinfo.phone, userinfo.email, userinfo.yzmcode, out errmsg);
            if (identifycode == null)
                return new { Rcode = 0, Rmsg = "验证码错误" };
            else if (DateTime.Now > identifycode.endtime)
                return new { Rcode = 0, Rmsg = "验证码超时，请重新获取验证码" };

            userinfo.passwd = StrFormatClass.MD5ToDepth(userinfo.passwd);
            userinfo.usertype = 1;
            userinfo.userlevel = 1;
            userinfo.sourcetype = 1;
            userinfo.customerid = 0;
            userinfo.status = 1;
            userinfo.grade = 0;
            userinfo.period = 0;
            userinfo.classname = ".";
            userinfo.school = ".";
            int userid = userLoginManager.UserReg(userinfo, out errmsg);
            if (userid > 0)
            {
                base_userlogs log = new base_userlogs()
                {
                    logtype = 21, // 21 注册； 22 登录； 
                    restype = "0",
                    rescode = userid.ToString(),
                    logcontent = string.Format("注册用户{0}", userinfo.username),
                    creator = 0,
                    createdtime = DateTime.Now
                };
                BLL.BaseBLL.WriteUserLog(log, out errmsg);//记录注册日志
                                
                return new { Rcode = 1, Rdata = userid };
            }
            else if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "注册失败：" + errmsg };
            }
            else
            {
                return new { Rcode = 0, Rmsg = "注册失败" };
            }
        }

        
        [HttpPost, Route("UserAccount/UserFindPasswd")]
        public object UserFindPasswd(base_users userinfo)
        {
            string errmsg = null;
            if (userinfo.yzmcode == null || userinfo.yzmcode.Length != 6)
            {
                return new { Rcode = -1, Rmsg = "找回密码失败：请填写验证码" };
            }
            base_identifycode identifycode = BLL.BaseBLL.GetIdentifyCodeInfo(userinfo.phone, userinfo.email, userinfo.yzmcode, out errmsg);
            if (identifycode == null)
                return new { Rcode = 0, Rmsg = "验证码错误" };
            else if (DateTime.Now > identifycode.endtime)
                return new { Rcode = 0, Rmsg = "验证码超时，请重新获取验证码" };

            userinfo.passwd = StrFormatClass.MD5ToDepth(userinfo.passwd);
       
            bool issuc = userLoginManager.UserFindPasswd(userinfo, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "找回密码失败：" + errmsg };
            }
            if (issuc)
            { 
                return new { Rcode = 1, Rmsg = "ok" };
            }
            else
            {
                return new { Rcode = 0, Rmsg = "找回密码失败" };
            }
        }
        #region 用户中心
        /// <summary>
        /// 用户中心 获取用户基础信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserInfo")]
        public object GetUserInfo()
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
           
            base_users info = BLL.UserLoginManager.GetUserInfo(userid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (info == null || info.userid < 1)
                return new { Rcode = 0, Rmsg = "用户不存在" };
            return new { Rcode = 1 ,Rdata=info};

        }
        /// <summary>
        /// 用户中心 修改基础信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut, Route("UserAccount/PutUserInfo")]
        public object PutUserInfo([FromBody] base_users info) {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            info.userid = userLoginManager.LoginUser.UserID;
            bool success = BLL.UserLoginManager.PutUserInfo(info,out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "修改失败" };
            UserInfoViewModel user = userLoginManager.LoginUser;
            if (!string.IsNullOrEmpty(info.nickname))
                user.NickName = info.nickname;
            if (!string.IsNullOrEmpty(info.nickname))
                user.Photo = info.photo;
           new  BLL.UserLoginManager().WriteCookie(user, ref errmsg);
            
            return new { Rcode = 1, Rmsg = "修改成功" };
        }
        /// <summary>
        /// 用户中心 验证用户密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserPwd")]
        public object GetUserPwd(string pwd) {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1) 
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            pwd = StrFormatClass.MD5ToDepth(pwd);
            base_users info =BLL.UserLoginManager.GetUserPwd(userid,pwd,out errmsg);
            if (info == null || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            return new { Rcode = 1 };

        }
        /// <summary>
        /// 关于 修改用户信息
        /// </summary>
        /// <param name="realname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/PutUserInfornames")]
        public object PutUserInfornames(string realname,string email,string phone,string opinion)
        {
           
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            bool success = BLL.UserLoginManager.PutUserInforname(userid,realname,email,phone,opinion,out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "反馈失败" };
            }
            return new { Rcode = 1, Rmsg = "修改失败" };


        }
        /// <summary>
        /// 关于 修改用户信息
        /// </summary>
        /// <param name="realname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/PutUserInforschoolinfo")]
        public object PutUserInforschoolinfo(string updateuserid,string username,string realname, string email, string phone, string opinion,string period,string grade,string classname,string school)
        {
            //在这里对包一注册接口进行验证。
            bool need_remote = false;
            string errmsg = null;
            //如果需要访问远程接口的话，就进行下面的代码
            if (need_remote)
            {
                string baseurl = "";
                string url = string.Format(
                                                @"{0}?userName={1}&realName={2}&nickName={3}&sex={4}&phoneNum={5}&birthDay={6}&gradeId={7}"
                                                , baseurl, username, realname, realname, 0, phone, DateTime.Now.ToShortDateString(), grade
                                           );
                JsonData result = SmartHttpRequest.WebServiceGet(url, "Post");
                if (result == null)
                {
                    if (((string)result["code"]).Equals("-1"))
                    {
                        UserAccountDAL.DeleteUser(updateuserid, out errmsg);
                        return new { Rcode = -1, Rmsg = "注册失败!!!" };
                        //并且删除用户信息
                    }
                }
            }
            int userid = Convert.ToInt32(updateuserid);//userLoginManager.LoginUser.UserID;
            bool success = BLL.UserLoginManager.PutUserInforname(userid, realname, email, phone, opinion, out errmsg,Convert.ToInt32(period), Convert.ToInt32(grade),classname,school);
            if (!success || !string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "反馈失败" };
            }
            return new { Rcode = 1, Rmsg = "修改失败" };


        }
        /// <summary>
        /// 用户中心 修改用户密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPut, Route("UserAccount/PutUserPwd")]
        public object PutUserPwd(string pwd)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            pwd = StrFormatClass.MD5ToDepth(pwd);
            bool success = BLL.UserLoginManager.PutUserPwd(userid, pwd, out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "修改失败" };
            return new { Rcode = 1, Rmsg = "修改失败" };

        }
        /// <summary>
        /// 我的订单
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserOrdInfo")]
        public object GetUserOrdInfo(int rows,int page)
        {
            string sortexp = "createdtime desc";
            string errmsg = null;
            int total;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
          
            DataTable dt = BLL.UserLoginManager.GetUserOrdInfo(userid,sortexp, rows, page,out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };

        }
        /// <summary>
        /// 用户中心 获取用户订单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserOrdersList")]
        public object GetUserOrdersList(int timetype,int page, int rows)
        {
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            int total = 0;
            List<chosen_orderinfo> list = BLL.UserLoginManager.GetUserOrdersList(userid,timetype, page, rows, out total, out errmsg);
           
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据出错" };
            List<Options> OrderStatusList = BLL.BaseBLL.GetDicOptions("OrderStatus");
            foreach (chosen_orderinfo item in list)
            {
                item.statusname = BLL.BaseBLL.GetDicName(OrderStatusList, item.status);//订单状态
            }
            return new { Rcode = 1, total = total, Rdata = list};
        }
        /// <summary>
        /// 用户中心  批量删除用户订单信息
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/UpdateUserOrdersList")]
        public object UpdateUserOrdersList(string orderno)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            bool success = BLL.UserLoginManager.UpdateUserOrdersList(userid, orderno, out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || !success)
                return new { Rcode = -1, Rmsg = "获取数据出错" };
            return new { Rcode = 1 };
        }
        /// <summary>
        /// 图书详情-点击购买
        /// </summary>
        /// <param name="m_price"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/InsertOrdersInfo")]
        public object InsertOrdersInfo(int m_price,string title) {
            string errmsg = null;
            chosen_orderinfo info = new chosen_orderinfo();
            string datenum = DateTime.Now.ToString("yyyyMMddhh24mmss");
            Random rd = new Random();
            string sjnum = rd.Next(1000000, 10000000).ToString();
            string ordernum = datenum + sjnum;
            info.orderno = ordernum;//订单编号
            info.ordername = title;//订单名称
            info.createdtime = DateTime.Now;//下单时间
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            info.userid = userLoginManager.LoginUser.UserID;//用户账户
            info.creator = userLoginManager.LoginUser.UserID;//客户账户
            info.feetype = "1";//货币类型
            info.ctype = 1;   //下单方式
            info.status = 1;//支付状态
            info.terminaltype = 1;//操作终端
            info.addressid = 0; //收货地址编号
            info.paybank = 0;
            info.m_price = m_price;
            info.s_price = m_price;
            info.paytype = 2;
            bool success = BLL.UserLoginManager.AdduserOrders(info, out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "添加失败" };
            return new { Rcode = 1, Rmsg = "添加成功" };
        }
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserFavor")]
        public object GetUserFavor(int restype,int rows, int page)
        {
            string sortexp = "createdtime desc";
            string errmsg = null;
            int total;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;

            DataTable dt = BLL.UserLoginManager.GetUserFavor(userid,restype,sortexp,  rows, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };

        }
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/GetUserHotkey")]
        public object GetUserHotkey(int rows, int page)
        {
            string sortexp = "createdtime desc";
            string errmsg = null;
            int total;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;

            DataTable dt = BLL.UserLoginManager.GetUserHotkey(userid, sortexp, rows, page, out total, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, total, Rdata = dt };

        }
        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/DeletedUserFavorList")]
        public object DeletedUserFavorList(string seqid)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = 0, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            bool success = BLL.UserLoginManager.DeletedUserFavorList(userid, seqid, out errmsg);
            if (!string.IsNullOrEmpty(errmsg) || !success)
                return new { Rcode = -1, Rmsg = "获取数据出错" };
            return new { Rcode = 1 };
        }
        /// <summary>
        /// 资源收藏
        /// </summary>
        /// <param name="restype">资源类型</param>
        /// <param name="seqid">资源编号</param>
        /// <param name="resremark">资源标题</param>
        /// <param name="classid">资源分类ID，多个逗号分隔</param>
        /// <returns></returns>
        [HttpGet, Route("UserAccount/ResourcesFavorites")]
        public object ResourcesFavorites(int restype, int seqid, string resremark,string classid)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            int userid = userLoginManager.LoginUser.UserID;
            resremark = System.Web.HttpUtility.UrlDecode(resremark);
            bool success = BLL.UserLoginManager.ResourcesFavorites(userid, restype, seqid, resremark, classid, out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "收藏失败：" + errmsg };
            return new { Rcode = 1 };
        }
        #endregion



        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UserAccount/FileUpload")]
        public object FileUpload()
        {
            System.Net.Http.HttpResponseMessage rmsg = new System.Net.Http.HttpResponseMessage();
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            var dir = request["dir"];
            if (dir == null || string.IsNullOrEmpty(dir) || !Microsoft.RIPSP.Model.GlobalParameters.UploadParams.ContainsKey(dir))
            {
                rmsg.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = -1,
                    error = 1,
                    Rmsg = "未注册的请求"
                }));
                return rmsg;
            }
            int count = request.Files.Count;
            if (count == 0)
            {
                rmsg.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = 0,
                    error = 1,
                    Rmsg = "未找到上传文件"
                }));
                return rmsg;
            }
            System.Web.HttpPostedFile file = request.Files[0];
            UploadParamModel uploadParam = Microsoft.RIPSP.Model.GlobalParameters.UploadParams[dir];
            string ext = Path.GetExtension(file.FileName).ToLower();
            if (Array.IndexOf<string>(uploadParam.Extension, ext) == -1)
            {
                rmsg.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = 0,
                    error = 1,
                    Rmsg = string.Format("仅可以上传[{0}]类型的文件", string.Join(";", uploadParam.Extension))
                }));
                return rmsg;
            }
            /*
            if (Array.IndexOf<string>(uploadParam.ContentType, file.ContentType) == -1) 
            {
                rmsg.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = 0,
                    error = 1,
                    Rmsg = string.Format("仅可以上传[{0}]类型的文件", string.Join(";", uploadParam.ContentType))
                }));
                return rmsg;
            }
            */
            if (file.ContentLength > uploadParam.ContentLength)
            {
                rmsg.Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = 0,
                    error = 1,
                    Rmsg = string.Format("仅可以上传不超过[{0}]大小的文件", uploadParam.ContentLength)
                }));
                return rmsg;
            }
            DateTime dtNow = DateTime.Now;
            string fileName = string.Format("{0}{1}", dtNow.ToString(@"yyyyMMddHHmmssfff"), ext);
            string filePath = string.Format("/{0}/{1}/{2}/{3}", Models.GlobalParameters.RootFileDirectoryName, uploadParam.DirName, dtNow.ToString(@"yyyyMM"), fileName);
            string savePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", Models.GlobalParameters.RootFilePath,Models.GlobalParameters.RootFileDirectoryName, uploadParam.DirName, dtNow.ToString(@"yyyyMM"), fileName);
            if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            file.SaveAs(savePath);
            var msg1 = new System.Net.Http.HttpResponseMessage()
            {
                Content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    Rcode = 1,
                    url = filePath,
                    filename = fileName,
                    oridocname = file.FileName,
                    FCount = request.Files.Count,
                    error = 0,
                    Rmsg = "保存成功"
                }))
            };
            return msg1;
        }

    }
}
