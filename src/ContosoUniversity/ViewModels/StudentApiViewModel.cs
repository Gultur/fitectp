using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.ViewModels
{
    public class StudentApiViewModel
    {
        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string enrollmentDate { get; set; }
        public List<EnrollmentApiViewModel> enrollments { get; set; }

    }
}
