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

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "Warning", "You have to indicate the id of the student" };
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Student student = db.Students.FirstOrDefault(s => s.ID == id);
            StudentApiViewModel studentToSend = new StudentApiViewModel();

            List< Dictionary<string, string> > coursesList = new List< Dictionary<string, string> >();

            foreach(Enrollment enrollment in student.Enrollments)
            {
                Dictionary<string, string> course = new Dictionary<string, string>();
                course["CourseID"] =  enrollment.CourseID.ToString();
                coursesList.Add(course);
            }

            studentToSend.Id = student.ID;
            studentToSend.FirstName = student.FirstMidName;
            studentToSend.LastName = student.LastName;
            studentToSend.EnrollmentDate = student.EnrollmentDate;
            studentToSend.Enrollments = coursesList;

            return Ok(studentToSend);
        }
    }
}