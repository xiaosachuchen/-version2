using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.RIPSP.Model;
using Microsoft.RIPSP.BLL.BaseManager;
using Microsoft.RIPSP.BaseController;
using OperateManager.Models.Resourcedb;
using OperateManager.BLL.BaseManager;

namespace OperateManager.Web.Controllers.BaseManager
{
    /// <summary>
    /// 用户数据管理
    /// </summary>
    public class usersmentController : BaseController
    {
        private string errmsg = null;

       
        /// <summary>
        /// 用户数统计（个人）
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public object Get(string starttime, string endtime)
        {
            List<UserCountList> list = new List<UserCountList>();
            UserCountList user = new UserCountList();
            user.UsersCount = usersBLL.GetUsersCount(out errmsg);
            user.UsersActCount = usersBLL.GetActCount(starttime, endtime, out errmsg);
            user.UsersNewCount = usersBLL.GetUsersNewCount(starttime, endtime, out errmsg);
            list.Add(user);
            return new { rows = list };
        }
        /// <summary>
        /// 用户百分比（个人）
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        [HttpGet, Route("RIPSP/Base/UserSource")]
        public object UserSource(string starttime, string endtime)
        {
            List<UserCountList> list = new List<UserCountList>();
            UserCountList user = new UserCountList();
            int UsersLoss = usersBLL.GetUserLoss(starttime, endtime, out errmsg);
            int UserRete = usersBLL.GetUserRete(starttime, endtime, out errmsg);
            int UserCount= usersBLL.GetUsersCount(out errmsg);
            int UserNewcount = usersBLL.GetUsersNewCount(starttime, endtime, out errmsg);
            double Losscount = Convert.ToDouble(UsersLoss) / Convert.ToDouble(UserCount);
            double Retcount = Convert.ToDouble(UserRete) / Convert.ToDouble(UserNewcount);
            user.UsersLoss = string.Format("{0:0.00}", Losscount * 100);
            user.UsersRete = string.Format("{0:0.00}", Retcount * 100 );
            list.Add(user);
            return new { rows = list };

        }
        /// <summary>
        /// 机构用户
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="stype"></param>
        /// <param name="areacode"></param>
        /// <param name="industry"></param>
        /// <param name="max_price"></param>
        /// <param name="min_price"></param>
        /// <returns></returns>
        [HttpGet, Route("RIPSP/Base/GetUserOrgList")]
        public  object GetUserOrgList(int offset, int limit,string starttime, string endtime, string stype, string areacode, string industry, string max_price, string min_price)
        {
            int total;
            string errmsg;
            List<OrgUsers> list = usersBLL.GetOrgUsersList(starttime, endtime, stype, areacode, industry, max_price, min_price, offset, limit, out total, out errmsg);
            foreach (OrgUsers item in list)
            {
                item.stypename = Microsoft.RIPSP.BaseBLL.GlobalCommonBLL.GetDicName("ServiceType", item.stype.ToString());
                item.areacode = Microsoft.RIPSP.BaseBLL.GlobalCommonBLL.GetDicName("areacode", item.areacode);
                item.industry = Microsoft.RIPSP.BaseBLL.GlobalCommonBLL.GetDicName("industry", item.industry);
            }
            return new { rows = list, total };
        }

        //机构用户
        [HttpGet, Route("RIPSP/Base/UserOrgCount")]
        public object UserOrgCount(string starttime, string endtime)
        {
            List<UserCountList> list = new List<UserCountList>();
            UserCountList user = new UserCountList();
            user.UsersCount = usersBLL.GetUserOrgCount(out errmsg);
            user.UsersActCount = usersBLL.GetOrgActCount(starttime, endtime, out errmsg);
            list.Add(user);
            return new { rows = list };
        }
    }
}