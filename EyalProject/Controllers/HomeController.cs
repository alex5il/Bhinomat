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
            //List<SelectListItem>[] arrList = qcDB.CoursesSet.;



            ViewBag.Courses       = new SelectList(qcDB.CoursesSet.ToList(), "Id", "Name");
            ViewBag.Subjects      = new SelectList(qcDB.SubjectsSet.ToList(), "Id", "Name");
            ViewBag.Subsubjects   = new SelectList(qcDB.SubsubjectsSet.ToList(), "Id", "Name");
            ViewBag.Difficulties  = new SelectList(qcDB.DifficultiesSet.ToList(), "Id", "Name");
            ViewBag.QuestionTypes = new SelectList(qcDB.QuestionTypesSet.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(AddQuestionViewModel viewModel) {
            return View();
        }


        //[HttpPost]
        //public ActionResult AddQuestion(Questions newQuestion)
        //{
            


        //    List<SelectListItem>[] arrList = CreateItems();

        //    ViewBag.Courses = new SelectList(arrList[0], "Value", "Text", null);
        //    ViewBag.SubjectTypes = new SelectList(arrList[1], "Value", "Text", null);
        //    ViewBag.SecondarySubjectTypes = new SelectList(arrList[2], "Value", "Text", null);
        //    ViewBag.DifficultyTypes = new SelectList(arrList[3], "Value", "Text", null);
        //    ViewBag.QuestionTypes = new SelectList(arrList[4], "Value", "Text", null);

        //    if (ModelState.IsValid)
        //    {
        //        // TODO :// Add the rest of the cases

        //        switch (newQuestion.Type)
        //        {
        //            case "Open":
        //                Open OpenType = new Open();

        //                OpenType.Question = newQuestion.Question;
        //                OpenType.Difficulty = newQuestion.Difficulty;
        //                OpenType.Subject = newQuestion.Subject;
        //                OpenType.SecondarySubject = newQuestion.SecondarySubject;
        //                OpenType.Type = newQuestion.Type; // TODO :// Remove from here cause we know the type by table?
        //                OpenType.LastUseDate = DateTime.Now; // TODO :// Remove this from here and add to the main question table
        //                OpenType.AnswearCorrect = newQuestion.AnswearCorrect;

        //                qcDB.Open.Add(OpenType);
        //                qcDB.SaveChanges();
        //                break;
        //            case "TrueFalse":
        //                TrueFalse TrueFalseType = new TrueFalse();

        //                TrueFalseType.Question = newQuestion.Question;
        //                TrueFalseType.Difficulty = newQuestion.Difficulty;
        //                TrueFalseType.Subject = newQuestion.Subject;
        //                TrueFalseType.SecondarySubject = newQuestion.SecondarySubject;
        //                TrueFalseType.Type = newQuestion.Type; // TODO :// Remove from here cause we know the type by table?
        //                TrueFalseType.LastUseDate = DateTime.Now; // TODO :// Remove this from here and add to the main question table?
        //                TrueFalseType.AnswearCorrect = newQuestion.AnswearCorrect;

        //                qcDB.TrueFalse.Add(TrueFalseType);
        //                qcDB.SaveChanges();
        //                break;
        //            case "American":
        //                break;
        //            case "MultiChoice":
        //                break;
        //        }

        //        ViewBag.OutMessage = "השאלה נוצרה בהצלחה!";
        //        return View();
        //    }

        //    ViewBag.OutMessage = "הייתה תקלה בניסיון יצירת השאלה!";
        //    return View();
        //}

        [HttpPost]
        public ActionResult LoadCourseSubjects(int courseId)
        {
            return Json(qcDB.SubjectsSet.Where(s => s.CourseId == courseId).ToList());
        }

        public ActionResult EditCourses()
        {
            return View();
        }
    }
}
