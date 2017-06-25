using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model; 
using Microsoft.RIPSP.BLL.ChosenManager;
using System.Data;

namespace Microsoft.RIPSP.Controller.ChosenManager 
{
	 /// <summary>
	 /// 订单商品管理 ordershop Controller 
	 /// </summary>
	 public class ordershopController : BaseController.BaseController 
	 {
		 

		 /// <summary>
		 /// 订单商品管理 列表查询 
		 /// </summary>
		 public object Get(int offset,int limit,string shopname,string shopno)
		 {
			 int total;
			 List<QueryModel> queryarr = new List<QueryModel>();
            QueryModel model = null;
            if(!string.IsNullOrEmpty(shopname)&&!string.Equals(shopname,"-1"))
            {
                model = new QueryModel();
                model.exp = "商品名称";
                model.name = "shopname";
                model.value = shopname;
                queryarr.Add(model);
            }
            if (!string.IsNullOrEmpty(shopno) && !string.Equals(shopno, "-1"))
            {
                model = new QueryModel();
                model.exp = "订单号";
                model.name = "shopno";
                model.value = shopno;
                queryarr.Add(model);
            }
            List<chosen_ordershop> list = ordershopBLL.GetPageList(queryarr,offset,limit,out total,out errmsg);
             
			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { rows = list, total };
		 }

		 /// <summary>
		 /// 订单商品管理 详情查询 
		 /// </summary>
		 public object Get(int id) 
		 {
			 chosen_ordershop info = ordershopBLL.GetInfo(id,out errmsg) 
;			 if (!string.IsNullOrEmpty(errmsg))
			 {
				 return new { Rcode = -1, Rmsg = "获取数据失败" };
			 }
			 return new { Rcode = 1, Rdata = info };
		 }

		 /// <summary>
		 /// 订单商品管理 添加数据 
		 /// </summary>
		 public object Post([FromBody] chosen_ordershop info) 
		 {
			 bool success = ordershopBLL.Add(info, out errmsg);
            WriteSysLog(0, string.Format("添加订单商品管理{0},返回{1} {2}", info.shopname, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "添加失败" };
			 return new { Rcode = 1, Rmsg = "添加成功" };
		 }

		 /// <summary>
		 /// 订单商品管理 更新数据 
		 /// </summary>
		 public object Put(int id,[FromBody] chosen_ordershop info) 
		 {
			 info.orderid = id; 	
			 bool success = ordershopBLL.Update(info, out errmsg);
            WriteSysLog(1, string.Format("更新订单商品管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "更新失败" };
			 return new { Rcode = 1, Rmsg = "更新成功" };
		 }

		 /// <summary>
		 /// 订单商品管理 删除 
		 /// </summary>
		 public object Delete(int id) 
		 {
			 bool success = ordershopBLL.Delete(id, out errmsg);
            WriteSysLog(-1, string.Format("删除订单商品管理 id为{0},返回{1} {2}", id, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }

		 /// <summary>
		 /// 订单商品管理 批量删除 
		 /// </summary>
		 public object Delete(IdArrayModel IdArray) 
		 {
			 if (IdArray == null)
			 {
				 return new { Rcode = 0, Rmsg = "删除失败，请选择要删除的对象" };
			  }
			 bool success = ordershopBLL.BatchDelete(IdArray.IdArray,out errmsg);
            string idstr = string.Join(",", IdArray.IdArray);
            WriteSysLog(-1, string.Format("批量删除订单商品管理 id为({0}),返回{1} {2}", idstr, success, errmsg));
            if (!success || !string.IsNullOrEmpty(errmsg))
				  return new { Rcode = -1, Rmsg = "删除失败" };
			 return new { Rcode = 1, Rmsg = "删除成功" };
		 }
        /// <summary>
        /// 资源管理  收藏量/分享/浏览/购买
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RIPSP/Base/GetInfoX")]
        public object GetInfoX(int type)
        {
            DataTable dt = orderinfoBLL.GetResFractional(type,out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        /// <summary>
        /// 资源管理  收藏量 y轴
        /// </summary>
        /// <returns></returns>
          [HttpGet, Route("RIPSP/Base/GetInfoY")]
        public object GetInfoY(int type,int restype, int classid,string starTime,string endTime)
        {

            DataTable dt = orderinfoBLL.GetResFractionalY(type,restype,classid, starTime,endTime, out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
            else if (dt == null || dt.Rows.Count == 0)
                return new { Rcode = 0, Rmsg = "未找到该资源" };
            return new { Rcode = 1, Rdata = dt };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RIPSP/Base/GetResTypeList")]
        public object GetResTypeList()
        {

            List<db_datalibrarys> list = orderinfoBLL.GetResTypeList();
            if (!string.IsNullOrEmpty(errmsg))
                return new { Rcode = -1, Rmsg = "获取数据错误" };
           
            return new { Rcode = 1, Rdata = list };
        }
    }
}
