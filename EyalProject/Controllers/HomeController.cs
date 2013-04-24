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
            ViewBag.Courses = new SelectList(qcDB.CoursesSet.ToList(), "Id", "Name");
            ViewBag.Subjects = new SelectList(qcDB.SubjectsSet.ToList(), "Id", "Name");
            ViewBag.Subsubjects = new SelectList(qcDB.SubsubjectsSet.ToList(), "Id", "Name");
            ViewBag.Difficulties = new SelectList(qcDB.DifficultiesSet.ToList(), "Id", "Name");
            ViewBag.Types = new SelectList(new[] {
                                                   new SelectListItem() { Text = "פתוחה", Value = "Open" },
                                                   new SelectListItem() { Text = "נכון/לא נכון", Value = "TrueFalse" },
                                                   new SelectListItem() { Text = "אמריקאית", Value = "American" },
                                                   new SelectListItem() { Text = "יותר מתשובה אחת", Value = "MoreThanOne" }
                                                 }, "Value", "Text");

            return View();
        }

        [HttpPost]
        public JsonResult AddAmericanQuestion(AmericanQuestion question)
        {
            qcDB.AmericanQuestionsSet.Add(question);
            qcDB.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult AddMoreThanOneQuestion(MoreThanOneQuestion question)
        {
            qcDB.MoreThenOneQuestionsSet.Add(question);
            qcDB.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult AddOpenQuestion(OpenQuestion question)
        {
            qcDB.OpenQuestionsSet.Add(question);
            qcDB.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult AddTrueFalseQuestion(TrueFalseQuestion question)
        {
            qcDB.TrueFalseQuestionsSet.Add(question);
            qcDB.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public ActionResult LoadCourseSubjects(int courseId)
        {
            return Json(qcDB.SubjectsSet.Where(s => s.CourseId == courseId).ToList());
        }

        [HttpPost]
        public ActionResult LoadSubjectSubsubjects(int subjectId)
        {
            return Json(qcDB.SubsubjectsSet.Where(s => s.SubjectId == subjectId).ToList());
        }
    }
}
