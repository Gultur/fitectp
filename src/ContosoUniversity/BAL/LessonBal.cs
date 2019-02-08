using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BAL
{
    public class LessonBAL
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public Lesson CreateEntityLesson(int InstructorID, Enum.DayOfCourse Day, int CourseID, int StartHour, int EndHour, DateTime Launch)
        {

            Lesson lesson = new Lesson();
            lesson.InstructorID = InstructorID;
            lesson.Course = db.Courses.FirstOrDefault(c => c.CourseID == CourseID);
            lesson.Day = Day;
            lesson.StartHour = StartHour;
            lesson.EndHour = EndHour;
            lesson.Launch = Launch;
            return lesson;


        }

        public bool IsPlanningValid(Lesson lesson)
        {
            int count = db.Lessons.Where(l => l.InstructorID == lesson.InstructorID)
                        .Where(l => l.Day == lesson.Day).Where(l => (l.StartHour >= lesson.StartHour && l.StartHour <= lesson.EndHour)
                        || (l.EndHour <= lesson.EndHour && l.EndHour >= lesson.StartHour)).ToList().Count;

            return (count == 0);
        }

        public void AddLesson(Lesson lesson, SchoolContext db)
        {
            try
            {
                using (this.db)
                {
                    db.Lessons.Add(lesson);
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}