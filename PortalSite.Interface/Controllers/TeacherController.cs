using Microsoft.RIPSP.Model;
using OperateManager.DAL.CourseManager;
using OperateManager.Models.Resourcedb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalSite.Interface.Controllers
{
    public class TeacherController : ApiController
    {
        [HttpGet, Route("Teacher/GetTeachers")]
        public object GetCourseList(string teachertype, bool ispage = false, int rows = 0, int offset = 0)
        {
            //生成测试数据代码
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                List<self_teacher> teachers = new List<self_teacher>();
                for (int i = 0; i < 9; i++)
                {
                    teachers.Add(new self_teacher() {
                                                        title ="王老师",
                                                        thumbnail = "img/pic-32.png",
                                                        abstracts = "北京大学语言处理博士，资 深英语类考试培训专家多年 潜心研究如何迅速提",
                                                        subject="物理",
                                                        status =1,}
                                                    );
                }
                return new { data=teachers,msg=""};
            }
            string errmsg = null;
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
           // parms.Add(new QueryModel() {name="type",exp="and" });
            List<self_teacher> masters = teacherDAL.GetTeacherByPaging(parms, ispage, offset, rows, out total, out errmsg);
            return new {  data = masters, msg = errmsg, coursetotal = total };
        }
        [HttpGet, Route("Teacher/GetTeacherDetail")]
        public object GetCourssDetail(int teacherid)
        {
            string errmsg = null;
            int total = 0;
            self_teacher courseinfo = teacherDAL.GetInfo(teacherid, out errmsg);
            return new { courses = courseinfo, msg = errmsg };
        }
    }
}
