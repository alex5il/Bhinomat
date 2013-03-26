using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace EyalProject.Models
{
    public class QuestionsContext : DbContext
    {
        public QuestionsContext()
            : base("QuestionCreator")
        {
        }

        public DbSet<QuestionsLists> QuestionsLists { get; set; }

        public DbSet<TrueFalse> TrueFalse { get; set; }
        public DbSet<American> American { get; set; }
        public DbSet<MoreThenOneAnswear> MoreThenOneAnswear { get; set; }
        public DbSet<Open> Open { get; set; }
    }

    [Table("QuestionsLists")]
    public class QuestionsLists
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
    }

    [Table("TrueFalse")]
    public class TrueFalse
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string AnswearCorrect { get; set; }
    }

    [Table("American")]
    public class American
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string Answear1 { get; set; }
        public string Answear2 { get; set; }
        public string Answear3 { get; set; }
        public string Answear4 { get; set; }
        public string AnswearCorrect { get; set; }
    }

    [Table("MoreThenOneAnswear")]
    public class MoreThenOneAnswear
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string Answear1 { get; set; }
        public string Answear2 { get; set; }
        public string Answear3 { get; set; }
        public string Answear4 { get; set; }
        public string Answear5 { get; set; }
        public string Answear6 { get; set; }
        public string Answear7 { get; set; }
        public string Answear8 { get; set; }
        public string AnswearCorrect { get; set; }
    }

    [Table("Open")]
    public class Open
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string AnswearCorrect { get; set; }
    }

    // Helper class for getting the data for all types of questions

    public class Questions
    {
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string SecondarySubject { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string Answear1 { get; set; }
        public string Answear2 { get; set; }
        public string Answear3 { get; set; }
        public string Answear4 { get; set; }
        public string Answear5 { get; set; }
        public string Answear6 { get; set; }
        public string Answear7 { get; set; }
        public string Answear8 { get; set; }
        public string AnswearCorrect { get; set; }
    }
}
