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
            ViewBag.NumberOfAmerican = qcDB.AmericanQuestionsSet.Count();
            ViewBag.NumberOfOpen = qcDB.OpenQuestionsSet.Count();
            ViewBag.NumberOfMoreThanOne = qcDB.MoreThenOneQuestionsSet.Count();
            ViewBag.NumberOfTrueFalse = qcDB.TrueFalseQuestionsSet.Count();
            ViewBag.Total = ViewBag.NumberOfAmerican +
                            ViewBag.NumberOfOpen +
                            ViewBag.NumberOfMoreThanOne +
                            ViewBag.NumberOfTrueFalse;

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

        public ActionResult DeleteQuestion(int nId, string strType)
        {
            switch (strType)
            {
                case "Open": OpenQuestion oQuest1 = qcDB.OpenQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    if (oQuest1 != null) qcDB.OpenQuestionsSet.Remove(oQuest1);
                    break;
                case "TrueFalse": TrueFalseQuestion oQuest2 = qcDB.TrueFalseQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    if (oQuest2 != null) qcDB.TrueFalseQuestionsSet.Remove(oQuest2);
                    break;
                case "American": AmericanQuestion oQuest3 = qcDB.AmericanQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    if (oQuest3 != null) qcDB.AmericanQuestionsSet.Remove(oQuest3);
                    break;
                case "MoreThanOne": MoreThanOneQuestion oQuest4 = qcDB.MoreThenOneQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    if (oQuest4 != null) qcDB.MoreThenOneQuestionsSet.Remove(oQuest4);
                    break;
            }

            qcDB.SaveChanges();

            List<Question> questionModel = new List<Question>();

            questionModel.AddRange(qcDB.TrueFalseQuestionsSet);
            questionModel.AddRange(qcDB.OpenQuestionsSet);
            questionModel.AddRange(qcDB.AmericanQuestionsSet);
            questionModel.AddRange(qcDB.MoreThenOneQuestionsSet);

            return View("QuestionList", questionModel);
        }

        public ActionResult EditQuestion(int nId, string strType)
        {
            ViewBag.Courses = new SelectList(qcDB.CoursesSet.ToList(), "Id", "Name");
            ViewBag.Subjects = new SelectList(qcDB.SubjectsSet.ToList(), "Id", "Name");
            ViewBag.Subsubjects = new SelectList(qcDB.SubsubjectsSet.ToList(), "Id", "Name");
            ViewBag.Difficulties = new SelectList(qcDB.DifficultiesSet.ToList(), "Id", "Name");

            switch (strType)
            {
                case "Open": OpenQuestion oQuest1 = qcDB.OpenQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    break;
                case "TrueFalse": TrueFalseQuestion oQuest2 = qcDB.TrueFalseQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    break;
                case "American": AmericanQuestion oQuest3 = qcDB.AmericanQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    break;
                case "MoreThanOne": MoreThanOneQuestion oQuest4 = qcDB.MoreThenOneQuestionsSet.FirstOrDefault(q => q.Id == nId);
                    break;
            }

            return View("AddQuestion");
        }

        public ActionResult QuestionList()
        {
            List<Question> questionModel = new List<Question>();

            questionModel.AddRange(qcDB.TrueFalseQuestionsSet);
            questionModel.AddRange(qcDB.OpenQuestionsSet);
            questionModel.AddRange(qcDB.AmericanQuestionsSet);
            questionModel.AddRange(qcDB.MoreThenOneQuestionsSet);

            return View(questionModel);
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
