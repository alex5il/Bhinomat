namespace EyalProject.Migrations
{
    using EyalProject.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EyalProject.Models.QuestionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EyalProject.Models.QuestionsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Course[] courses = {
                                    new Course { Name = "מבנה המשחב"},
                                    new Course { Name = "חישוביות ומורכבות החישובים"},
                                    new Course { Name = "אלגברה ליניארית"},
                                    new Course { Name = "אוטומטים ושפות פורמליות"}
                               };

            context.Database.ExecuteSqlCommand("delete from Courses");
            context.CoursesSet.AddOrUpdate(courses);


            Subject[] subjects = {
                                    new Subject {Name="אסמבלי", ParentCourse = courses[0]},
                                    new Subject {Name="MIPS", ParentCourse = courses[0]},
                                    new Subject {Name="נושא כלשהו", ParentCourse = courses[1]},
                                    new Subject {Name="אלכס", ParentCourse = courses[2]},
                                    new Subject {Name="עודד", ParentCourse = courses[3]}
                                 };

            context.Database.ExecuteSqlCommand("delete from Subjects");
            context.SubjectsSet.AddOrUpdate(subjects);

            Subsubject[] subsubjects = {
                                           new Subsubject {Name = "פקודות בסיסיות", ParentSubject = subjects[0]},
                                           new Subsubject {Name = "פקודות מתקדמות", ParentSubject = subjects[0]},
                                           new Subsubject {Name = "מבנה בסיסי", ParentSubject = subjects[1]},
                                           new Subsubject {Name = "מבנה מתקדם", ParentSubject = subjects[2]},
                                           new Subsubject {Name = "תת-נושא כלשהו", ParentSubject = subjects[2]},
                                           new Subsubject {Name = "קונוננקו", ParentSubject = subjects[3]},
                                           new Subsubject {Name = "שירלי", ParentSubject = subjects[4]}
                                       };

            context.Database.ExecuteSqlCommand("delete from Subsubjects");
            context.SubsubjectsSet.AddOrUpdate(subsubjects);


            Difficulty[] difficulties = {
                                            new Difficulty {Name="קל"},
                                            new Difficulty {Name="בינוני"},
                                            new Difficulty {Name="קשה"}
                                        };

            context.Database.ExecuteSqlCommand("delete from Difficulties");
            context.DifficultiesSet.AddOrUpdate(difficulties);
        }
    }
}
