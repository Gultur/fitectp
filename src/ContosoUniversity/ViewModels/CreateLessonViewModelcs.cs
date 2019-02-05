using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public class CreateLessonViewModelcs
    {

        public List<Course> CourseList { get; set; }

        [Required]
        public DayOfCourse Day { get; set; }
        [Required]
        [Range(9, 18)]
        public int StartHour { get; set; }
        [Required]
        [Range(10, 19)]
        public int EndHour { get; set; }

    }
}