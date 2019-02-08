using ContosoUniversity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class InstructorApiViewModel
    {
        public int instructorId { get; set; }
        public List<EnrollmentApiViewModel> schedule { get; set; }

        public class EnrollmentApiViewModel
        {
            public int courseId { get; set; }
            public string day { get; set; }
            public string startHour { get; set; }
            public int duration { get; set; }
        }
    }
}