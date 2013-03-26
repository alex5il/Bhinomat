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

        public ActionResult AddQuestion2()
        {
            return View();
        }

        public ActionResult AddQuestion()
        {
            List<SelectListItem>[] arrList = CreateItems();

            ViewBag.SubjectTypes = new SelectList(arrList[0], "Value", "Text", null);
            ViewBag.SecondarySubjectTypes = new SelectList(arrList[1], "Value", "Text", null);
            ViewBag.DifficultyTypes = new SelectList(arrList[2], "Value", "Text", null);
            ViewBag.QuestionTypes = new SelectList(arrList[3], "Value", "Text", null);

            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(Questions newQuestion)
        {
            List<SelectListItem>[] arrList = CreateItems();

            ViewBag.SubjectTypes = new SelectList(arrList[0], "Value", "Text", null);
            ViewBag.SecondarySubjectTypes = new SelectList(arrList[1], "Value", "Text", null);
            ViewBag.DifficultyTypes = new SelectList(arrList[2], "Value", "Text", null);
            ViewBag.QuestionTypes = new SelectList(arrList[3], "Value", "Text", null);

            if (ModelState.IsValid)
            {
                // TODO :// Add the rest of the cases

                switch (newQuestion.Type)
                {
                    case "Open":
                        Open OpenType = new Open();

                        OpenType.Question = newQuestion.Question;
                        OpenType.Difficulty = newQuestion.Difficulty;
                        OpenType.Subject = newQuestion.Subject;
                        OpenType.SecondarySubject = newQuestion.SecondarySubject;
                        OpenType.Type = newQuestion.Type; // TODO :// Remove from here cause we know the type by table?
                        OpenType.LastUseDate = DateTime.Now; // TODO :// Remove this from here and add to the main question table
                        OpenType.AnswearCorrect = newQuestion.AnswearCorrect;

                        qcDB.Open.Add(OpenType);
                        qcDB.SaveChanges();
                        break;
                    case "TrueFalse":
                        TrueFalse TrueFalseType = new TrueFalse();

                        TrueFalseType.Question = newQuestion.Question;
                        TrueFalseType.Difficulty = newQuestion.Difficulty;
                        TrueFalseType.Subject = newQuestion.Subject;
                        TrueFalseType.SecondarySubject = newQuestion.SecondarySubject;
                        TrueFalseType.Type = newQuestion.Type; // TODO :// Remove from here cause we know the type by table?
                        TrueFalseType.LastUseDate = DateTime.Now; // TODO :// Remove this from here and add to the main question table?
                        TrueFalseType.AnswearCorrect = newQuestion.AnswearCorrect;

                        qcDB.TrueFalse.Add(TrueFalseType);
                        qcDB.SaveChanges();
                        break;
                    case "American":
                        break;
                    case "MultiChoice":
                        break;
                }

                ViewBag.OutMessage = "השאלה נוצרה בהצלחה!";
                return View();
            }

            ViewBag.OutMessage = "הייתה תקלה בניסיון יצירת השאלה!";
            return View();
        }

        private List<SelectListItem>[] CreateItems()
        {
            // TODO :// Optimise the List item Creation

            List<SelectListItem>[] arrList = new List<SelectListItem>[4];

            List<SelectListItem> itemsSubject = new List<SelectListItem>();

            itemsSubject.Add(new SelectListItem { Text = "עברית", Value = "עברית" });
            itemsSubject.Add(new SelectListItem { Text = "מתמטיקה", Value = "מתמטיקה" });
            itemsSubject.Add(new SelectListItem { Text = "פיזיקה", Value = "פיזיקה" });
            itemsSubject.Add(new SelectListItem { Text = "כימיה", Value = "כימיה" });

            List<SelectListItem> itemsSecondarySubject = new List<SelectListItem>();

            itemsSecondarySubject.Add(new SelectListItem { Text = "אינפי", Value = "אינפי" });
            itemsSecondarySubject.Add(new SelectListItem { Text = "גיאומטריאה", Value = "גיאומטריאה" });
            itemsSecondarySubject.Add(new SelectListItem { Text = "אלגברה ליניארית", Value = "אלגברה ליניארית" });
            itemsSecondarySubject.Add(new SelectListItem { Text = "תורת הקבוצות", Value = "תורת הקבוצות" });

            List<SelectListItem> itemsDifficulty = new List<SelectListItem>();

            itemsDifficulty.Add(new SelectListItem { Text = "קל", Value = "קל" });
            itemsDifficulty.Add(new SelectListItem { Text = "בינוני", Value = "בינוני" });
            itemsDifficulty.Add(new SelectListItem { Text = "קשה", Value = "קשה" });

            List<SelectListItem> itemsQuestionType = new List<SelectListItem>();

            itemsQuestionType.Add(new SelectListItem { Text = "נכון-לא נכון", Value = "TrueFalse" });
            itemsQuestionType.Add(new SelectListItem { Text = "אמריקאי", Value = "American" });
            itemsQuestionType.Add(new SelectListItem { Text = "יותר מתשובה 1", Value = "MultiChoice" });
            itemsQuestionType.Add(new SelectListItem { Text = "פתוחה", Value = "Open" });

            arrList[0] = itemsSubject;
            arrList[1] = itemsSecondarySubject;
            arrList[2] = itemsDifficulty;
            arrList[3] = itemsQuestionType;

            return arrList;
        }
    }
}
