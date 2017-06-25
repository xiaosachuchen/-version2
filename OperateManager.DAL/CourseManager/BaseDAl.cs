using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlHelperClass;
namespace OperateManager.DAL.CourseManager
{
    public class BaseDAl
    {
        public static void SetSqlConnectionString()
        {
            SqlHelperClass.MySqlHelper.ConnectionString = @"Server=123.59.199.68;Port=3360;Database=publishresourcedelivery;Uid=softcenter;Pwd=retechSoftcenter;CharSet=utf8;"; ;
        }
    }
}
