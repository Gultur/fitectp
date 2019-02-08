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
        public List<LessonApiViewModel> schedule { get; set; }

    }
}