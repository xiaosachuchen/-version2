using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Models
{
    /// <summary>
    /// 用于前台用户数据
    /// 保存在cookie中
    /// </summary>
    public class UserInfoViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        public int UserType { get; set; }
        public int Customerid { get; set; }
        public int SourceType { get; set; }
        public string SourceRemarks { get; set; }
        public string Photo { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
    }
    /// <summary>
    /// 用于用户登录
    /// </summary>
    public class UserLoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}