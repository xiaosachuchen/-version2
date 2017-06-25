using LogHelperClassForWeb;
using PortalSite.Interface.Models;
using System;
using System.Collections.Generic;
using Yunpian.conf;
using Yunpian.lib;
using Yunpian.model;

namespace PortalSite.Interface.BLL
{
    public static class SmsServiceBLL
    {
        /// <summary>
        /// 云片单条验证短信发送
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="yzmcode">验证码</param>
        /// <param name="extend">下发号码扩展（纯数字），需申请</param>
        /// <param name="uid">业务ID，需申请</param>
        /// <param name="register">是否注册验证短信，需申请</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool SmsSingleSendByYunpian(string mobile, string yzmcode, string extend, string uid, bool register, out string errmsg)
        {
            errmsg = null;
            Config config = new Config(GlobalParameters.Sms_Yunpian_ApiKey);
            Result result;
            Dictionary<string, string> data = new Dictionary<string, string>();
            SmsOperator sms = new SmsOperator(config);
            data.Clear();
            data.Add("mobile", mobile);
            data.Add("text", string.Format(GlobalParameters.Sms_Yunpian_ContentTemplate, yzmcode));
            // data.Add("extend", extend);
            //  data.Add("uid", uid);
            //  data.Add("register", register.ToString());
            result = sms.singleSend(data);
            LogHelperWeb.WriteInfo(string.Format("调用云片短信发送，向号码 {0} 发送 {1} 返回：{2} 错误{3}", mobile, yzmcode, result.success, result.data.Last));
            if (!result.success)
            {
                errmsg = result.data.Last.ToString();
                return false;
            }
            return true;
        }
    }
}
