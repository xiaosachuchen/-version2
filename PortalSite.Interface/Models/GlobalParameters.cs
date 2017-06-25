using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PortalSite.Interface.Models
{
    public static class GlobalParameters
    {
        public static readonly string DefaultUserPasswd = "689EE787E0EA220E6D5A72163EB8C437";//"111111"
        public static readonly bool ISee_AutoFilterTag = false;
        public static readonly int ISee_MaxMatchCount = 1000;

        public static readonly string RootFilePath = ConfigurationManager.AppSettings["RootFilePath"];
        public static readonly string RootFileDirectoryName = ConfigurationManager.AppSettings["RootFileDirectoryName"];


        #region yunpian云片短信发送服务配置
        public static readonly string Sms_Yunpian_ApiKey = "370b2527af1e8953f01813bd2b11b94c";
        public static readonly string Sms_Yunpian_ContentTemplate = "【中国问题研究平台】您的验证码是{0}";
        #endregion

        #region 邮件发送
        public static readonly string SmtpServer = "mail.crup.com.cn";
        public static readonly int SmtpPost = 25;
        public static readonly string SmtpUser = "chinastudies@crup.com.cn";
        public static readonly string SmtpPass = "chinastudies123456";
        public static readonly string SmtpMail = "chinastudies@crup.com.cn";
        public static readonly string SmtpSignTitle = "【中国问题研究平台】";
        #endregion
    }
}