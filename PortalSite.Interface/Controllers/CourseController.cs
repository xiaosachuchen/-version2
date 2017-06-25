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
using System.Web.SessionState;
using PortalSite.Interface.Globle;
namespace PortalSite.Interface.Controllers
{
   
    public class CourseController : ApiController, IRequiresSessionState
    {

        [HttpGet, Route("Course/GetCoursesDic")]
        public object GetCourseDic(string dictype, bool ispage = false, int rows = 0, int offset = 0, string orderby = "")
        {
            string errmsg = null;
            int total = 0;
            List<Options> list = BLL.BaseBLL.GetDicOptions(dictype);
            return new { grades=list,subjects=list,msg=errmsg,dictotal=total};
        }
        [HttpGet, Route("Course/GetCourses")]
        public object GetCourseList(string coursetype, bool ispage = false, int rows = 0, int offset = 0,string press="",string grade="",string subject="",string orderby="")
        {
            string errmsg = null;
            int total = 100;
            //生成测试数据代码
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                List<self_course> courses = new List<self_course>();
                for (int i = 0; i < rows; i++)
                {
                    courses.Add(new self_course() {
                                                        subject = 2,
                                                        grade=3,
                                                        seqid = 1,
                                                        source = "img/1/240-240/hx0001.jpg",
                                                        abstracts = @"文题解释、译注解析、艺术鉴
                                                                      赏、知识小结、文化常识、扩
                                                                      展阅读和课后解答.....test",
                                                        title = "课程测试数据",
                                                        teachername ="李老师",
                                                        time="12H50Min",
                                                        s_prise =20,
                                                        createdtime = i % 2 == 0 ? DateTime.Now : new DateTime(2017, 6, 1)
                    }
                                                   );
                }
                return new { courses = courses, msg = errmsg, coursetotal = total,pageindex=offset };
            }
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = GlobleSetting.GetDicValue(coursetype,"coursetype")});
            parms.Add(new QueryModel() { name = "subject", exp = "and", value =GlobleSetting.GetDicValue(subject,"subject") });
            parms.Add(new QueryModel() { name = "grade", exp = "and", value =GlobleSetting.GetDicValue(grade,"grade") });
            parms.Add(new QueryModel() { name = "press", exp = "and", value =GlobleSetting.GetDicValue(press,"press")});
            parms.Add(new QueryModel() { name = "version", exp = "and", value = GlobleSetting.GetDicValue(press, "version") });
            List<self_course> coures = courseDAl.GetCourseByPaging(parms, ispage, offset, rows,false, out total, out errmsg);
            //重组课程封面
            coures.ForEach(r=>r.source=string.Format("{0}{1}",Globle.GlobleSetting.GetAppSettingKey("RootFilePath"),r.thumbnail));
            return new { courses = coures, msg = errmsg, coursetotal = total };
        }
        [HttpGet, Route("Course/GetCoursesGroupByDate")]
        public object GetCourseListGroupbyDate(string coursetype, bool ispage = false, int rows = 0, int offset = 0, string press = "", string grade = "", string subject = "", string orderby = "")
        {
            string errmsg = null;
            int total = 100;
            //生成测试数据代码
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                List<self_course> courses = new List<self_course>();
                for (int i = 0; i < rows; i++)
                {
                    courses.Add(new self_course()
                    {
                        subject = 2,
                        grade = 3,
                        seqid = 1,
                        source = "img/1/240-240/hx0001.jpg",
                        abstracts = @"文题解释、译注解析、艺术鉴
                                    赏、知识小结、文化常识、扩
                                    展阅读和课后解答.....test",
                        title = "课程测试数据",
                        teachername = "李老师",
                        time = "12H50Min",
                        s_prise = 20,
                        createdtime=i%2==0? DateTime.Now:new DateTime(2017,6,1)
                    }
                    );
                }
                var coursesgroupby = from p in courses
                                     group p by p.createdtime.ToShortDateString()
                                     into g
                                     select g;
                Dictionary<string, List<self_course>> dicourse = new Dictionary<string, List<self_course>>();
                foreach (IGrouping<string,self_course> item in coursesgroupby)
                {
                    if (dicourse.Keys.Contains(item.Key))
                    {
                        dicourse[item.Key] = item.ToList();
                    }
                    else
                    {
                        dicourse.Add(item.Key, item.ToList());
                    }
                }
                return new { courses = dicourse, msg = errmsg, coursetotal = total, pageindex = offset };
            }
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = coursetype });
            parms.Add(new QueryModel() { name = "subject", exp = "and", value = subject });
            parms.Add(new QueryModel() { name = "grade", exp = "and", value = grade });
            parms.Add(new QueryModel() { name = "press", exp = "and", value = press });
            List<self_course> coures = courseDAl.GetCourseByPaging(parms, ispage, offset, rows, false, out total, out errmsg);
            return new { courses = coures, msg = errmsg, coursetotal = total };
        }
        [HttpGet, Route("Course/GetCourseDetail")]
        public object GetCourssDetail(int courseid)
        {
            string errmsg = null;
            int total = 0;
            //生成测试数据代码
            if (ConfigurationManager.AppSettings["createdata"].ToString().Equals("yes"))
            {
                self_teacher test_teacher = new self_teacher() {
                                                                    abstracts = "好老师",
                                                                    title = "王老师",
                                                                    source = "4小",
                                                                    grade = "初中",
                                                                    subject = "语文"
                                                               };
                self_course test_course= new self_course() {
                                                                    subject = 2,
                                                                    seqid = 1,
                                                                    source = "img/1/240-240/hx0001.jpg",
                                                                    abstracts = "不错的视频",
                                                                    title = "测试的视频",
                                                                    teachername = "王老师",
                                                                    s_prise = 20
                                                            };
                List<self_video> test_videos = new List<self_video>();
                for (int i = 0; i < 5; i++)
                {
                    test_videos.Add(new self_video() {
                                                                    subject = 2,
                                                                    seqid = 1,
                                                                    source = "img/1/240-240/hx0001.jpg",
                                                                    abstracts = "不错的视频",
                                                                    title = "测试的视频",
                                                                    s_price = 20
                                                     });
                }
                return new { course= test_course,teacher=test_teacher,videos=test_videos};
            }
            //这里需要进行3次数据库操作，获取课程，获取老师，获取视频
            //待完成代码
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "seqid", exp = "and", value = courseid.ToString() });
            self_course courseinfo = courseDAl.GetCourseByPaging(parms, false,0,1,true,out total,out errmsg).FirstOrDefault();
            return new { courses = courseinfo, msg = errmsg };
        }
        private string GetDicValue(string type, int id)
        {
            try
            {
                string dicValue = BLL.BaseBLL.GetDicOptions(type).Where(r => Convert.ToInt32(r.id) == id).Select(r => r.text).FirstOrDefault();
                return dicValue;
            }
            catch (Exception e)
            {
                return "-";
            }
            
        }
    }
}
