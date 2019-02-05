using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContosoUniversity.Enum;

namespace ContosoUniversity.Models
{
    public class Lesson
    {
        public int ID { get; set; }

        [Required]
        public int InstructorID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public DayOfCourse Day { get; set;}
        [Required]
        [Range(9, 18)]
        public int StartHour { get; set; }
        [Required]
        [Range(10, 19)]
        public int EndHour { get; set; }

        public int Length
        {
            get
            {
                return EndHour - StartHour;
            }
        }


        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }


    }
}