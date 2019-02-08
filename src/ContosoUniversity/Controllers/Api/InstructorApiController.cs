using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContosoUniversity.Controllers
{
    [RoutePrefix("api/instructors")]
    public class InstructorApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        [Route("{id}/weeklyschedule")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Instructor instructor = db.Instructors.FirstOrDefault(s => s.ID == id);

            //TODO : send a not found and not a ok
            if (instructor == null)
            {
                Dictionary<string, string> ErrorDict = new Dictionary<string, string>();
                ErrorDict["Status"] = "Error";
                ErrorDict["Message"] = "There is no instructor with that Id";

                return Ok(ErrorDict);
            }


            InstructorApiViewModel instructorToSend = new InstructorApiViewModel();

            instructorToSend.InstructorID = instructor.ID;
            instructorToSend.Schedule = new List<InstructorApiViewModel.EnrollmentApiViewModel>();

            foreach (Course course in instructor.Courses)
            {
                foreach(Lesson lesson in course.Lessons)
                {
                    InstructorApiViewModel.EnrollmentApiViewModel enrollment = new InstructorApiViewModel.EnrollmentApiViewModel();
                    enrollment.CourseId = course.CourseID;
                    enrollment.Day = lesson.Day.ToString();
                    enrollment.StartHour = lesson.StartHour;
                    enrollment.Duration = lesson.Length * 60;
                    instructorToSend.Schedule.Add(enrollment);
                }
            }

            return Ok(instructorToSend);
        }
    }
}
