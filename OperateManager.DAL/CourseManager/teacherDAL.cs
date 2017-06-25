using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlHelperClass;
using Microsoft.RIPSP.Model;
using OperateManager.Models.Resourcedb;
using OperateManager.DAL.Reflect;

namespace OperateManager.DAL.CourseManager
{
    public class teacherDAL:BaseDAl
    {
        public static List<self_teacher> GetTeacherByPaging(List<QueryModel> queryarr, bool ispage, int offset, int rows, out int total, out string errmsg)
        {
            total = 0;
            errmsg = null;
            List<DataParameter> pars = new List<DataParameter>();
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1 ");
            if (queryarr.Count > 0)
            {
                foreach (QueryModel query in queryarr)
                {
                    if (!string.IsNullOrEmpty(query.value))
                    {
                        sb.Append(string.Format(" {0} {1} =@{1}", query.exp, query.name));
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
                }
            }
            string sqlstr = string.Format("select * from self_teacher where {0} order by createdtime DESC  limit {1} offset {2}",sb, rows, offset);
            if (ispage)
            {
                string sqlcount = string.Format("select count(1) from self_teacher where {0}  ",sb);
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars);
            }
            if (ispage && total == 0)
                return new List<self_teacher>();
            return MySqlHelper.GetDataList<self_teacher>(sqlstr, out errmsg, pars);
        }
        public static List<self_teacher> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            List<DataParameter> pars = new List<DataParameter>();
            string sqlcount = "select count(1) from self_teacher where 1=1 ";
            string sqlstr = "select * from self_teacher where 1=1 ";
            if (queryarr.Count > 0)
            {
                foreach (QueryModel query in queryarr)
                {
                    if (!string.IsNullOrEmpty(query.value))
                    {
                        sqlcount = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
                        sqlstr = string.Format("{0} and {1} {2} @{1}", sqlstr, query.name, query.exp);
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
                }
            }
            sqlstr += " order by seqid desc ";
            if (offset > -1)
            {
                sqlstr += " limit " + offset + "," + limit;
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
            }
            if (total > 0 || offset < 0)
                return MySqlHelper.GetDataList<self_teacher>(sqlstr, out errmsg, pars.ToArray());
            return new List<self_teacher>();
        }

        /// <summary>
        /// 收藏管理 详情查询 
        /// </summary>
        public static self_teacher GetInfo(int seqid, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from self_teacher  where seqid=@seqid";
            pars.Add(new DataParameter("seqid", seqid));
            return MySqlHelper.GetDataInfo<self_teacher>(sqlstr, out errmsg, pars.ToArray());
        }
        public static bool Add(self_teacher info, out string errmsg)
        {
            return OrmClass.AutoSqlExecute(info, "insert", out errmsg);
        }

        /// <summary>
        /// 订单管理 更新数据 
        /// </summary>
        /// <param name="self_course">要更新的订单管理对象</param> 
        /// <param name="errmsg">错误信息</param>
        /// <returns>返回对象</returns>
        public static bool Update(self_teacher info, out string errmsg)
        {
            return OrmClass.AutoSqlExecute(info, "update", out errmsg);
        }

        /// <summary>
        /// 订单管理 删除 
        /// </summary>
        public static bool Delete(string id, out string errmsg)
        {
            self_teacher course = new self_teacher() { seqid = Convert.ToInt32(id) };
            return OrmClass.AutoSqlExecute(course, "delete", out errmsg);
        }
    }
}
