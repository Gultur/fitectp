using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            // anonymous
            // We need to send liste of courses
            if(Session["User"] != null)
            {
                Person user = (Person)Session["User"];

                if(user is Student)
                {
                    //TODO agend for student !
                    return View("IndexStudent");
                }
                else if(user is Instructor)
                {
                    // Temporary the ID is set manually
                    // waiting to Session["User"].ID applied everywhere

                    Dictionary<int, Dictionary<DayOfCourse, string>> agenda = new Dictionary<int, Dictionary<DayOfCourse, string>>();


                    // TODO Move some instruction in other layer
                    for (int hour = 8; hour <= 19; hour++)
                    {
                        Dictionary<DayOfCourse, string> HourDay = new Dictionary<DayOfCourse, string>();
                        foreach(DayOfCourse day in (DayOfCourse[]) System.Enum.GetValues(typeof(DayOfCourse)))
                        {
                            string libelle ="";
                            Lesson lesson = db.Lessons.Where(l => (l.InstructorID == user.ID && l.Day == day))
                                .Where(l => (l.StartHour == hour || l.StartHour < hour && l.EndHour > hour))
                                .FirstOrDefault();
                            if(lesson != null)
                            {
                                libelle = lesson.Course.Title;
                            }
                            HourDay.Add(day, libelle);
                        }
                        agenda.Add(hour, HourDay);
                    }


                    ViewBag.Lessons = agenda;
                    return View("IndexInstructor");
                }
            }

            ViewBag.Courses = db.Courses.ToList();
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);

            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}