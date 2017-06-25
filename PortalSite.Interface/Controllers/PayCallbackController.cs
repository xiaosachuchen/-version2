using Microsoft.RIPSP.ThirdPayModular.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class PayCallbackController : ApiController
    {
        /// <summary>
        /// 微信支付后台返回接收
        /// </summary>
        [HttpGet, HttpPost, Route("PayCallback/WXPayCallBack")]
        public void WXPayCallBack()
        {
            WXPayBLL resultNotify = new WXPayBLL();
            resultNotify.ProcessNotify();
        }
        /// <summary>
        /// 支付宝支付后台返回接收
        /// </summary>
        [HttpGet, HttpPost, Route("PayCallback/AliPayCallBack")]
        public void AliPayCallBack()
        {
            AliPayBLL resultNotify = new AliPayBLL();
            resultNotify.ProcessNotify();
        }
    }
}
