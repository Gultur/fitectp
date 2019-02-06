using ContosoUniversity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class InstructorApiViewModel
    {
        public int InstructorID { get; set; }
        public List<EnrollmentApiViewModel> Schedule { get; set; }

        public class EnrollmentApiViewModel
        {
            public int CourseId { get; set; }
            public DayOfCourse Day { get; set; }
            public int StartHour { get; set; }
            public int Duration { get; set; }
        }
    }
}