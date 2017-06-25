using Microsoft.RIPSP.Model;
using PortalSite.Interface.BLL;
using PublicHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class UserValidateController : ApiController
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet, Route("UserValidate/SendYzmSms")]
        public object SendYzmSms(string mobile)
        {
            if (!ValidatorClass.Validator(mobile, CheckType.Mobile, null, false))
            {
                return new { Rcode = -1, Rmsg = "参数错误" };
            }
            Random rand = new Random();
            string errmsg = null;
            base_identifycode info = new base_identifycode
            {
                code = rand.Next(100000, 999999).ToString(),
                sendtime = DateTime.Now,
                endtime = DateTime.Now.AddMinutes(3),
                phone = mobile
            };
            bool success = BaseBLL.PutIdentifyCode(info, out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "写入验证码出错" + errmsg };
            if(!SmsServiceBLL.SmsSingleSendByYunpian(mobile, info.code, null, null, true, out errmsg))
            {
                return new { Rcode = -1, Rmsg = "发送短信出错："+errmsg };
            }
            return new { Rcode = 1, Rmsg = "短信已下发，请注意查收" };
        }
        [HttpGet, Route("UserValidate/SendYzmEmail")]
        public object SendYzmEmail(string email)
        {
            if (!ValidatorClass.Validator(email, CheckType.Email, null, false))
            {
                return new { Rcode = -1, Rmsg = "参数错误" };
            }
            Random rand = new Random();
            string errmsg = null;
            base_identifycode info = new base_identifycode
            {
                code = rand.Next(100000, 999999).ToString(),
                sendtime = DateTime.Now,
                endtime = DateTime.Now.AddMinutes(5),
                email = email
            };
            bool success = BaseBLL.PutIdentifyCode(info, out errmsg);
            if (!success || !string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "写入验证码出错" + errmsg };
            string subject = Models.GlobalParameters.SmtpSignTitle + "验证邮件";
            string content = string.Format("您的验证码为 {0} ,请勿轻易告诉他人！", info.code);
            NetClass.SmtpServer = Models.GlobalParameters.SmtpServer;
            NetClass.Account = Models.GlobalParameters.SmtpUser;
            NetClass.Passwd = Models.GlobalParameters.SmtpPass;
            NetClass.MailFrom = Models.GlobalParameters.SmtpMail;
            if (!NetClass.SendMail(email,subject,content, Models.GlobalParameters.SmtpMail, ref errmsg))
            {
                return new { Rcode = -1, Rmsg = "发送邮件出错：" + errmsg };
            }
            return new { Rcode = 1, Rmsg = "邮件已发送，请注意查收" };
        }
    }
}
