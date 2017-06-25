using OperateManager.Models.ModelAttribute;
using SqlHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OperateManager.DAL.Reflect
{
    public class OrmClass
    {
        /// <summary>
        /// 自动化生成sql 语句，并且执行结果，并返回状态
        /// 主要进行 插入,删除，更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool AutoSqlExecute<T>(T model, string type,out string errmsg)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            string sql = "";
            string id = "";
            List<string> keys = new List<string>();
            List<string> updatekeys = new List<string>();
            List<DataParameter> pars = new List<DataParameter>();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
                object[] Attribute = item.GetCustomAttributes(typeof(IgnoreAttribute), false);
                object[] KeyAttribute = item.GetCustomAttributes(typeof(KeyAttribute), false);
                if (KeyAttribute.Count() > 0)
                {
                    //删除,更新时候添加主键
                    if (type == "update" || type == "delete")
                    {
                        pars.Add(new DataParameter(name, value));
                    }
                }
                if (Attribute.Count() == 0)
                {
                    keys.Add("@" + name); //拼接参数
                    updatekeys.Add(name + "=@" + name); //根据属性获取参数
                    if (value is string)
                    {
                        pars.Add(new DataParameter(name, value));
                    }
                    else 
                    {
                        pars.Add(new DataParameter(name, value));
                    }
                }
            }
            switch (type)
            {
                case "update":
                    sql = string.Format("update {0} set {1} where seqid=@seqid", t.Name, string.Join(",", updatekeys.ToArray()));
                    break;
                case "insert":
                    sql = string.Format("insert into {0} ({1}) values ({2})", t.Name, string.Join(",", keys.ToArray()).Replace("@", ""), string.Join(",", keys.ToArray()));
                    break;
                case "delete":
                    sql = string.Format("delete from {0} where seqid=@seqid", t.Name);
                    break;
            }
            //测试使用
            if (string.IsNullOrWhiteSpace(MySqlHelper.ConnectionString))
            {
                MySqlHelper.ConnectionString = @"Server=123.59.199.68;Port=3360;Database=publishresourcedelivery;Uid=softcenter;Pwd=retechSoftcenter;CharSet=utf8;";
            }
            return MySqlHelper.ExecuteCommand(sql, out errmsg, pars) > 0;
        }
    }
}
