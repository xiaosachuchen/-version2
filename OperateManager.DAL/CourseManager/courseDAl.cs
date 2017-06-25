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
    public class courseDAl:BaseDAl
    {
        /// <summary>
        /// 获取课程信息 是否包含详细信息
        /// </summary>
        /// <param name="courseparam"></param>
        /// <param name="ispage"></param>
        /// <param name="offset">第几页</param>
        /// <param name="rows"></param>
        /// <param name="isdetail">是否显示详细信息（包括教师信息，视频信息）</param>
        /// <param name="total"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static List<self_course> GetCourseByPaging(List<QueryModel> queryarr, bool ispage, int offset, int rows,bool isdetail, out int total, out string errmsg)
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
                        sb.Append(string.Format(" {0} {1} =@{1}",query.exp, query.name));
                        if (query.exp == "like")
                            pars.Add(new DataParameter(query.name, string.Format("%{0}%", query.value)));
                        else
                            pars.Add(new DataParameter(query.name, query.value));
                    }
                }
            }
            string sqlstr = string.Format("select * from self_course where {0} order by createdtime DESC  limit {1} offset {2}",sb, rows, offset*rows);
            if (ispage)
            {
                string sqlcount = string.Format("select count(1) from self_course where {0} ",sb);
                total = (int)MySqlHelper.GetRecCount(sqlcount, out errmsg, pars.ToArray());
            }
            if (ispage && total == 0)
                return new List<self_course>();
            return isdetail ? GetAllDetailCourse(MySqlHelper.GetDataList<self_course>(sqlstr, out errmsg, pars.ToArray())) : MySqlHelper.GetDataList<self_course>(sqlstr, out errmsg, pars.ToArray());
        }
        public static List<self_course> GetAllDetailCourse(List<self_course> soursecourse)
        {
            foreach (var item in soursecourse)
            {
                int total = 0;
                string msg = "";
                // Dictionary<string, object> dic = new Dictionary<string, object>();
                List<QueryModel> parm = new List<QueryModel>();
                parm.Add(new QueryModel() { name="courseid",exp="and",value=item.seqid.ToString()});
                item.videos = videoDAL.GetVideoByPaging(parm,false,0,Int32.MaxValue,out total,out msg);
                List<QueryModel> parm_t = new List<QueryModel>();
                parm.Add(new QueryModel() { name = "seqid", exp = "and", value = item.teacher.ToString() });
                item.self_teacher = teacherDAL.GetTeacherByPaging(parm_t, false, 0, 1 ,out total, out msg).FirstOrDefault();

                //Dictionary<string, object> teach = new Dictionary<string, object>();
                //if (item.videos.Count > 0)
                //{
                //    //Dictionary<string, object> videoids = new Dictionary<string, object>();
                //    List<QueryModel> parms = new List<QueryModel>() { new QueryModel() {
                //                                                                            name = "seqid",
                //                                                                            exp = "and",
                //                                                                            value = item.videos.Where(r => !string.IsNullOrWhiteSpace(r.teacher.ToString()))
                //                                                                                                .Select(r=>r.teacher)
                //                                                                                                .FirstOrDefault()
                //                                                                                                .ToString() } };
                //    item.self_teacher = teacherDAL.GetTeacherByPaging(parms,false,0,Int32.MaxValue,out total,out msg).FirstOrDefault();
                //}
            }
            return soursecourse;
        }
        public static List<self_course> GetPageList(List<QueryModel> queryarr, int offset, int limit, out int total, out string errmsg)
        {
            errmsg = null;
            total = 0;
            List<DataParameter> pars = new List<DataParameter>();
            string sqlcount = "select count(1) from self_course where 1=1 ";
            string sqlstr = "select * from self_course where 1=1 ";
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
                return MySqlHelper.GetDataList<self_course>(sqlstr, out errmsg, pars.ToArray());
            return new List<self_course>();
        }

        /// <summary>
        /// 收藏管理 详情查询 
        /// </summary>
        public static self_course GetInfo(int seqid, out string errmsg)
        {
            List<DataParameter> pars = new List<DataParameter>();
            string sqlstr = "select * from self_course  where seqid=@seqid ";
            pars.Add(new DataParameter("seqid", seqid));
            return MySqlHelper.GetDataInfo<self_course>(sqlstr, out errmsg, pars.ToArray());
        }

        public static bool Add(self_course info, out string errmsg)
        {
            return OrmClass.AutoSqlExecute(info,"insert",out errmsg);
        }

        /// <summary>
        /// 订单管理 更新数据 
        /// </summary>
        /// <param name="self_course">要更新的订单管理对象</param> 
        /// <param name="errmsg">错误信息</param>
        /// <returns>返回对象</returns>
        public static bool Update(self_course info, out string errmsg)
        {
            return OrmClass.AutoSqlExecute(info, "update", out errmsg);
        }

        /// <summary>
        /// 订单管理 删除 
        /// </summary>
        public static bool Delete(string id, out string errmsg)
        {
            self_course course = new self_course() { seqid =Convert.ToInt32(id) };
            return OrmClass.AutoSqlExecute(course, "delete", out errmsg);
        }
    }
}
