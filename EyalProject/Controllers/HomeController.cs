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

        public ActionResult DeleteQuestion(int nId, string strType)
        {
            switch (strType)
            {
                case "Open": OpenQuestion oQuest1 = qcDB.OpenQuestionSet.FirstOrDefault(q => q.Id == nId);
                    if (oQuest1 != null) qcDB.OpenQuestionSet.Remove(oQuest1);
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
            questionModel.AddRange(qcDB.OpenQuestionSet);
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
                case "Open": OpenQuestion oQuest1 = qcDB.OpenQuestionSet.FirstOrDefault(q => q.Id == nId);
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
            questionModel.AddRange(qcDB.OpenQuestionSet);
            questionModel.AddRange(qcDB.AmericanQuestionsSet);
            questionModel.AddRange(qcDB.MoreThenOneQuestionsSet);

            return View(questionModel);
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
            qcDB.OpenQuestionSet.Add(question);
            qcDB.SaveChanges();
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
