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
        
        public DbSet<TrueFalseQuestion> TrueFalseQuestiosnSet { get; set; }
        public DbSet<AmericanQuestion> AmericanQuestionsSet { get; set; }
        public DbSet<MoreThanOneQuestion> MoreThenOneQuestionsSet { get; set; }
        public DbSet<OpenQuestion> OpenQuestionSet { get; set; }

        public DbSet<Course> CoursesSet { get; set; }
        public DbSet<Subject> SubjectsSet { get; set; }
        public DbSet<Subsubject> SubsubjectsSet { get; set; }
        public DbSet<Difficulty> DifficultiesSet { get; set; }
        public DbSet<QuestionType> QuestionTypesSet { get; set; }
    }

    // Helper class for getting the data for all types of questions

    public class Questions
    {
        public int QuestionId { get; set; }
        public DateTime LastUseDate { get; set; }
        public string Course { get; set; }
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


    #region Questions

    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string QuestionText { get; set; }
        public DateTime LastUseDate { get; set; }

        public int SubsubjectId { get; set; }
        [ForeignKey("SubsubjectId")]
        public Subsubject Subsubject { get; set; }

        public int DifficultyId { get; set; }
        [ForeignKey("DifficultyId")]
        public Difficulty Difficulty { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public QuestionType QuestionType { get; set; }

        [NotMapped]
        public Course Course
        {
            get
            {
                return this.Subject.ParentCourse;
            }
            set
            {
                this.Subject.ParentCourse = value;
            }
        }

        [NotMapped]
        public Subject Subject
        {
            get
            {
                return this.Subsubject.ParentSubject;
            }
            set
            {
                this.Subsubject.ParentSubject = value;
            }
        }
    }

    [Table("TrueFalseQuestions")]
    public class TrueFalseQuestion : Question
    {
        public bool IsTrue { get; set; }
    }

    [Table("AmericanQuestions")]
    public class AmericanQuestion : Question
    {
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }

        public const int MinAnswers = 4;
        public const int MaxAnswers = 5;
    }

    [Table("MoreThanOneQuestions")]
    public class MoreThanOneQuestion : Question
    {
        public string[] CorrectAnswers { get; set; }
        public string[] IncorrectAnswers { get; set; }

        public const int MinAnswers = 4;
        public const int MaxAnswers = 5;
    }

    [Table("OpenQuestions")]
    public class OpenQuestion : Question
    {
        public string CorrectAnswer { get; set; }
    }

    #endregion Questions

    [Table("Courses")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("Subjects")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course ParentCourse { get; set; }
    }

    [Table("Subsubjects")]
    public class Subsubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject ParentSubject { get; set; }
    }

    [Table("Difficulties")]
    public class Difficulty
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
	}

    [Table("QuestionTypes")]
    public class QuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
