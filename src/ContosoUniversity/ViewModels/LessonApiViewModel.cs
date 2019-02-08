using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class LessonApiViewModel
    {

            public int courseId { get; set; }
            public string day { get; set; }
            public string startHour { get; set; }
            public int duration { get; set; }
        
    }
}