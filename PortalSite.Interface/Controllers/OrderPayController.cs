using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.ThirdPayModular.BLL;
using System.Web.Http;
using System.Web.SessionState;

namespace PortalSite.Interface.Controllers
{
    public class OrderPayController : ApiController, IRequiresSessionState
    {
        BLL.UserLoginManager userLoginManager = new BLL.UserLoginManager();
        /// <summary>
        /// PC端订单扫码支付
        /// </summary>
        /// <param name="orderno">订单号</param>
        /// <param name="paytype">支付方式[1 微信支付；2 支付宝支付]</param>
        /// <returns></returns>
        [HttpGet, Route("OrderPay/GetUserOrderPay")]
        public object GetUserOrderPay(string orderno, int paytype)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };

            if (string.IsNullOrEmpty(orderno))
                return new { Rcode = 0, Rmsg = "参数错误" };

            chosen_orderinfo orderinfo = OrderPayBLL.GetUserOrderInfo(orderno, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取订单数据错误" + errmsg };
            if (orderinfo == null)
                return new { Rcode = -1, Rmsg = "订单号不存在" };
            if (orderinfo.status != 1 && orderinfo.status != 1)
                return new { Rcode = -1, Rmsg = "订单不在待支付状态" };
            string savename = string.Format("{0}_{1}", orderno, paytype);
            string url = GetSavePayUrl(savename);
            if (string.IsNullOrEmpty(url))
            {
                switch (paytype)
                {
                    case 1:
                        url = WXPayBLL.GetPayUrl(orderinfo, out errmsg);
                        break;
                    case 2:
                        url = AliPayBLL.GetPayUrl(orderinfo, out errmsg);
                        break;
                    default:
                        errmsg = "错误的支付方式选择";
                        break;
                }
            }
            if (string.IsNullOrEmpty(errmsg) && !string.IsNullOrEmpty(url))
            {
                SetSavePayUrl(savename, url);
                return new { Rcode = 1, Rdata = url, Rmsg = "ok" };
            }
            return new { Rcode = -1, Rmsg = errmsg };
        }
        /// <summary>
        /// 获取订单支付状态
        /// </summary>
        /// <param name="orderno"></param>
        /// <returns></returns>
        [HttpGet, Route("OrderPay/GetUserOrderPayStatus")]
        public object GetUserOrderPayStatus(string orderno)
        {
            string errmsg = null;
            if (userLoginManager.LoginUser == null || userLoginManager.LoginUser.UserID < 1)
                return new { Rcode = -2, Rmsg = "未登录" };
            if (string.IsNullOrEmpty(orderno))
                return new { Rcode = 0, Rmsg = "参数错误" };

            chosen_orderinfo orderinfo = OrderPayBLL.GetUserOrderInfo(orderno, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取订单数据错误" + errmsg };
            if (orderinfo == null)
                return new { Rcode = -1, Rmsg = "订单号不存在" };
            return new { Rcode = 1, Rdata = orderinfo.status };
        }
        private string GetSavePayUrl(string name)
        {
            if (System.Web.HttpContext.Current.Session[name] == null)
                return null;
            return System.Web.HttpContext.Current.Session[name].ToString();
        }
        private void SetSavePayUrl(string name,string value)
        {
            System.Web.HttpContext.Current.Session[name] = value;
        }
    }
}
