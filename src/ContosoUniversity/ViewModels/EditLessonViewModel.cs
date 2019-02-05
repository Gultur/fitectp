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
    public class EditLessonViewModel
    {

        public int CourseID { get; set; }
        public int LessonID { get; set; }

        [Required]
        public DayOfCourse Day { get; set; }
        [Required]
        [Range(8, 18)]
        public int StartHour { get; set; }
        [Required]
        [Range(9, 19)]
        public int EndHour { get; set; }

    }
}