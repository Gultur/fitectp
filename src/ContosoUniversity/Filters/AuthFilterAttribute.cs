using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ContosoUniversity.Filters
{
    public class AuthFilterAttribute: AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase filterContext)
        {


            if (HttpContext.Current.Session["User"] == null)
            {
                return false;
            }
            else if( Role.ToString() == "Student" && !(HttpContext.Current.Session["User"] is Student))
            {
                return false;
            }
            else if (Role.ToString() == "Instructor" && !(HttpContext.Current.Session["User"] is Instructor))
            {
                return false;
            }
            return true;
        }
    }
}