using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.ChosenManager;
using Microsoft.RIPSP.BaseBLL;
using System.Data;

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 订单管理 orderinfo Controller 
	 /// </summary>
	 public class orderinfoController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 订单管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit,string serviceno, string customerid,string status)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
             QueryModel model = null;
            if (!string.IsNullOrEmpty(serviceno) && !string.Equals(serviceno, "-1"))
            {
                model = new QueryModel();
                model.exp = "服务编号";
                model.name = "serviceno";
                model.value = serviceno;
                queryarr.Add(model);
            }
            if (!string.IsNullOrEmpty(customerid) && !string.Equals(customerid, "-1"))
            {
                model = new QueryModel();
                model.exp = "客户编号";
                model.name = "customerid";
                model.value = customerid;
                queryarr.Add(model);
            }
            if (!string.IsNullOrEmpty(status) && !string.Equals(status, "-1"))
            {
                model = new QueryModel();
                model.exp = "状态";
                model.name = "status";
                model.value = status;
                queryarr.Add(model);
            }
            List<chosen_orderinfo> list = orderinfoBLL.GetPageList(queryarr,offset,limit,out total,out errmsg); 
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
            foreach (chosen_orderinfo chosenorderinfo in list)
            {
                chosenorderinfo.paybankname = BaseBLL.GlobalCommonBLL.GetDicName("PayBank", chosenorderinfo.paybank.ToString());//支付渠道
                chosenorderinfo.statusname = BaseBLL.GlobalCommonBLL.GetDicName("OrderStatus", chosenorderinfo.status.ToString());//订单状态
                chosenorderinfo.paytypename= BaseBLL.GlobalCommonBLL.GetDicName("PayType", chosenorderinfo.paytype.ToString());//支付方式
                chosenorderinfo.terminaltypename= BaseBLL.GlobalCommonBLL.GetDicName("TerminalType", chosenorderinfo.terminaltype.ToString());//操作终端
                chosenorderinfo.ctypename = BaseBLL.GlobalCommonBLL.GetDicName("Ctype", chosenorderinfo.ctype.ToString());//下单方式
               
            }
            return new { rows = list, total };
		 }

		 /// <summary>
		 /// 订单管理 详情查询 
		 /// </summary>
		 public object Get(string id) 
		 {
			 chosen_orderinfo info = orderinfoBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 订单管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] chosen_orderinfo info) 
		 {
            string datenum = DateTime.Now.ToString("yyyyMMddhh24mmss");
            Random rd = new Random();
            string sjnum = rd.Next(1000000, 10000000).ToString();
            string ordernum = datenum + sjnum;
            info.orderno = ordernum;

            info.createdtime = DateTime.Now;
            info.creator = LoginUserData.UserID;
            info.userid = LoginUserData.UserID;
            info.feetype = "1";
            info.ctype = 1;
            info.status = 1;
            info.terminaltype = 1;
            info.addressid = 0;
             bool success = orderinfoBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加订单{0}，返回{1} {2}", info.ordername, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 订单管理 更新数据 
		 /// </summary>
		 public object Put(string id,[FromBody] chosen_orderinfo info) 
		 {
			 info.orderno = id;
            info.createdtime = DateTime.Now;
            info.creator = LoginUserData.UserID;
            bool success = orderinfoBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新订单id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 订单管理 删除 
		 /// </summary>
		 public object Delete(string id) 
		 {
			 bool success = orderinfoBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除订单id为{0}，返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 订单管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
            string idstr = string.Join(",", IdArray.IdArray);
            bool success = orderinfoBLL.BatchDelete(IdArray.StrIdArray,out errmsg);
            WriteSysLog(-1, string.Format("批量删除订单id为({0})，返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public object Get(string timetype, string starDate, string endDate)
        {

            DataTable dt = orderinfoBLL.GetInfoEchar_user(timetype, starDate, endDate, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        public object Get(string TimeType, string StarDate, string EndDate,int i)
        {

            DataTable dt = orderinfoBLL.GetInfoEchar_org(TimeType, StarDate, EndDate, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }

        [HttpGet, Route("RIPSP/Base/orderinfos")]
        public object orderinfos(int offset, int limit)
        {
            int total;
            List<transTion> list = orderinfoBLL.GetFracList(offset, limit, out total, out errmsg);
           // DataTable dt = orderinfoBLL.GetInfoFractional(out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return new { Rcode = -1, Rmsg = "获取数据失败" };
            }
            return new { rows = list, total };
        }
    }
}
