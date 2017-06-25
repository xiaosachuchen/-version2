using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.Models.Resourcedb
{
   public class UserCountList
    {
        /// <summary>
        /// 总用户数
        /// </summary>
        public int UsersCount { get; set; }
        /// <summary>
        /// 新注册用户
        /// </summary>
        public int  UsersNewCount { get; set; }
        /// <summary>
        /// 活跃用户数
        /// </summary>
        public int  UsersActCount { get; set; }
        /// <summary>
        /// 用户流失人
        /// </summary>
        public string UsersLoss { get; set; }
        /// <summary>
        /// 用户留存
        /// </summary>
        public string UsersRete { get; set; }
        /// <summary>
        /// 重复购买人
        /// </summary>
        public string UserRepPur { get; set; }
    }
}
