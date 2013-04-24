using EyalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EyalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        QuestionsContext qcDB = new QuestionsContext();

        public ActionResult BuildDb()
        {
            ViewBag.Message = "Build DB page here";

            return View();
        }

        public ActionResult BuildTest()
        {
            return View();
        }

        public ActionResult AddQuestion()
        {
            ViewBag.Courses       = new SelectList(qcDB.CoursesSet.ToList(), "Id", "Name");
            ViewBag.Subjects      = new SelectList(qcDB.SubjectsSet.ToList(), "Id", "Name");
            ViewBag.Subsubjects   = new SelectList(qcDB.SubsubjectsSet.ToList(), "Id", "Name");
            ViewBag.Difficulties  = new SelectList(qcDB.DifficultiesSet.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public void AddAmericanQuestion(AmericanQuestion question)
        {

        }

        [HttpPost]
        public void AddMoreThanOneQuestion(MoreThanOneQuestion question)
        {

        }

        [HttpPost]
        public void AddOpenQuestion(OpenQuestion question)
        {

        }

        [HttpPost]
        public void AddTrueFalseQuestion(TrueFalseQuestion question)
        {
            qcDB.TrueFalseQuestionsSet.Add(question);
            qcDB.SaveChanges();
        }

        [HttpPost]
        public ActionResult LoadCourseSubjects(int courseId)
        {
            return Json(qcDB.SubjectsSet.Where(s => s.CourseId == courseId).ToList());
        }
    }
}
