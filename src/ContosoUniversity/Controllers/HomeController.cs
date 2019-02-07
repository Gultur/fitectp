using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
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
                    return View("IndexStudent");
                }
                else if(user is Instructor)
                {
                    List<Lesson> lessonsMonday = db.Lessons.Where(l => (l.InstructorID == user.ID && l.Day == Enum.DayOfCourse.Monday)).ToList();

                    Dictionary<string, Dictionary<int, string>> agenda = new Dictionary<string, Dictionary<int, string>>;


                    ViewBag.Lessons = lessonsMonday;
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