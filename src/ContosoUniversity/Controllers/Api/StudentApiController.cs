using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContosoUniversity.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentApiController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "Warning", "You have to indicate the id of the student" };
        //}

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Student student = db.Students.FirstOrDefault(s => s.ID == id);

            //TODO : send a not found and not a ok

            if ( student == null)
            {
                Dictionary<string,string> ErrorDict = new Dictionary<string,string> ();
                ErrorDict["Status"] = "Error";
                ErrorDict["Message"] = "There is no student with that Id";

                return Ok(ErrorDict);
            }

            StudentApiViewModel studentToSend = new StudentApiViewModel();

            List<EnrollmentApiViewModel> coursesList = new List< EnrollmentApiViewModel  >();

            // TODO : Use  modelDto for the enrollments
            foreach(Enrollment enrollment in student.Enrollments)
            {
                EnrollmentApiViewModel course = new EnrollmentApiViewModel();
                course.courseId =  enrollment.CourseID.ToString();
                coursesList.Add(course);
            }

          
            studentToSend.id = student.ID;
            studentToSend.firstname = student.FirstMidName;
            studentToSend.lastname = student.LastName;
            studentToSend.enrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd");
            studentToSend.enrollments = coursesList;

            return Ok(studentToSend);
        }
    }
}