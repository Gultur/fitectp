using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    public class StudentEnrollmentsViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Course> CoursesList { get;set; }
    }
}