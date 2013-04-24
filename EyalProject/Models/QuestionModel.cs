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
        
        public DbSet<TrueFalseQuestion> TrueFalseQuestionsSet { get; set; }
        public DbSet<AmericanQuestion> AmericanQuestionsSet { get; set; }
        public DbSet<MoreThanOneQuestion> MoreThenOneQuestionsSet { get; set; }
        public DbSet<OpenQuestion> OpenQuestionsSet { get; set; }

        public DbSet<Course> CoursesSet { get; set; }
        public DbSet<Subject> SubjectsSet { get; set; }
        public DbSet<Subsubject> SubsubjectsSet { get; set; }
        public DbSet<Difficulty> DifficultiesSet { get; set; }
    }

    #region Questions

    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string QuestionText { get; set; }
        public DateTime? LastUseDate { get; set; }

        public int SubsubjectId { get; set; }
        [ForeignKey("SubsubjectId")]
        public Subsubject Subsubject { get; set; }

        public int DifficultyId { get; set; }
        [ForeignKey("DifficultyId")]
        public Difficulty Difficulty { get; set; }

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

        public string IncorrectAnswers { get; set; }

        [NotMapped]
        public List<string> IncorrectAnswersArray
        {
            get
            {
                return (this.IncorrectAnswers != null) ? 
                       new List<string>(this.IncorrectAnswers.Split(';')) :
                       new List<string>();
            }
            set
            {
                this.IncorrectAnswers = string.Join(";", value);
            }
        }

        [NotMapped]
        public const int MinAnswers = 4;

        [NotMapped]
        public const int MaxAnswers = 5;
    }

    [Table("MoreThanOneQuestions")]
    public class MoreThanOneQuestion : Question
    {
        public string CorrectAnswers { get; set; }
        public string IncorrectAnswers { get; set; }

        [NotMapped]
        public List<string> CorrectAnswersArray
        {
            get
            {
                return (this.CorrectAnswers != null) ?
                       new List<string>(this.CorrectAnswers.Split(';')) :
                       new List<string>();
            }
            set
            {
                this.CorrectAnswers = string.Join(";", value);
            }
        }

        [NotMapped]
        public List<string> IncorrectAnswersArray
        {
            get
            {
                return (this.IncorrectAnswers != null) ?
                       new List<string>(this.IncorrectAnswers.Split(';')) :
                       new List<string>();
            }
            set
            {
                this.IncorrectAnswers = string.Join(";", value);
            }
        }

        [NotMapped]
        public const int MinAnswers = 4;

        [NotMapped]
        public const int MaxAnswers = 8;
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

    public enum QuestionType
    {
        American,
        MoreThanOne,
        Open,
        TrueFalse
    }
}
