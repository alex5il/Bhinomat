using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EyalProject.Models
{
    public class AddQuestionViewModel
    {
        public Question Question { get; set; }
        public TrueFalseQuestion TrueFalseQuestion { get; set; }
        public AmericanQuestion AmericanQuestion { get; set; }
        public MoreThanOneQuestion MoreThanOneQuestion { get; set; }
        public OpenQuestion OpenQuestion { get; set; }
    }
}