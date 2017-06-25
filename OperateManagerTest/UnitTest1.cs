using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperateManager.DAL.CourseManager;
using OperateManager.Models.Resourcedb;
using System.Collections.Generic;
using Microsoft.RIPSP.Model;

namespace OperateManagerTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestInsertCourse()
        {
            self_course course = new self_course()
            {
                abstracts = "语文课程",
                title = "课程",
                press = 1,
                version = 1,
                grade = 1,
                teacher=1,
                issue=1,
                coursetype=1,
                m_price=Convert.ToDecimal(11.5),
                subject=1,
                hits=10000,
                status=5,
                thumbnail="E:\\",
                createdtime = DateTime.Now
            };
            string ermsg = "";
            courseDAl.Add(course,out ermsg);
        }
        [TestMethod]
        public void TestUpdateCourse()
        {
            self_course course = new self_course()
            {
                seqid=1,
                abstracts = "语文课程",
                title = "课程",
                press = 1,
                version = 1,
                grade = 1,
                teacher = 1,
                issue = 1,
                coursetype = 1,
                m_price = Convert.ToDecimal(11.5),
                subject = 1,
                hits = 10000,
                status = 5,
                thumbnail = "E:\\",
                createdtime = DateTime.Now
            };
            string ermsg = "";
            courseDAl.Update(course, out ermsg);
            Equals(ermsg==null);
        }
        [TestMethod]
        public void TestDeleteCourse()
        {
            string ermsg = "";
            courseDAl.Delete("2", out ermsg);
            Equals(ermsg == null);
        }
        [TestMethod]
        public void TestQueryCourse()
        {
            courseDAl.SetSqlConnectionString();
            string ermsg = "";
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
            //parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = 1.ToString() });
            //parms.Add(new QueryModel() { name = "subject", exp = "and", value = 2.ToString() });
            //parms.Add(new QueryModel() { name = "grade", exp = "and", value = 3.ToString() });
            //parms.Add(new QueryModel() { name = "press", exp = "and", value = 2.ToString() });
            List<self_course> course = courseDAl.GetCourseByPaging(parms,false,0,100,true,out total, out ermsg);
            Equals(course.Count>0);
        }
        [TestMethod]
        public void TestQueryCourse2()
        {
            string ermsg = "";
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = 1.ToString() });
            //parms.Add(new QueryModel() { name = "subject", exp = "and", value = 2.ToString() });
            //parms.Add(new QueryModel() { name = "grade", exp = "and", value = 3.ToString() });
            //parms.Add(new QueryModel() { name = "press", exp = "and", value = 2.ToString() });
            List<self_course> course = courseDAl.GetCourseByPaging(parms, false, 0, 100, false, out total, out ermsg);
            Equals(course.Count > 0);
        }
        [TestMethod]
        public void TestQueryCourse3()
        {
            string ermsg = "";
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = 1.ToString() });
            parms.Add(new QueryModel() { name = "subject", exp = "and", value = 8.ToString() });
            //parms.Add(new QueryModel() { name = "grade", exp = "and", value = 3.ToString() });
            //parms.Add(new QueryModel() { name = "press", exp = "and", value = 2.ToString() });
            List<self_course> course = courseDAl.GetCourseByPaging(parms, false, 0, 100, false, out total, out ermsg);
            Equals(course.Count > 0);
        }
        [TestMethod]
        public void TestQueryCourse4()
        {
            string ermsg = "";
            int total = 0;
            List<QueryModel> parms = new List<QueryModel>();
            parms.Add(new QueryModel() { name = "coursetype", exp = "and", value = 1.ToString() });
            parms.Add(new QueryModel() { name = "subject", exp = "and", value = 8.ToString() });
            parms.Add(new QueryModel() { name = "grade", exp = "and", value = 7.ToString() });
            //parms.Add(new QueryModel() { name = "press", exp = "and", value = 2.ToString() });
            List<self_course> course = courseDAl.GetCourseByPaging(parms, false, 0, 100, false, out total, out ermsg);
            Equals(course.Count > 0);
        }
    }
}
