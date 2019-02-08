using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using ContosoUniversity.Enum;
using ContosoUniversity.BAL;
using ContosoUniversity.Services;
using ContosoUniversity.BAL;

namespace ContosoUniversity.Controllers
{
    public class AccountController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PersonRegisterViewModel newAccount)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonBAL balPerson = new PersonBAL();

                    if (balPerson.IsLoginValid(newAccount.Login,db))
                    {

                        if (newAccount.Roles == EnumRoles.Student)
                        {
                            StudentBAL balStudent = new StudentBAL();
                            balStudent.CreateStudentRegistering(newAccount, db);
                        }
                        else
                        {
                            InstructorBAL balInstructor = new InstructorBAL();
                            balInstructor.CreateInstructorRegistering(newAccount, db);
                        }
                        // Move searching of person in an other layer
                        //  getPersonByLogin(newAccount.Login)
                        
                        Session["User"] = balPerson.GetPersonByLogin(newAccount.Login, db);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        TempData["LoginError"] = "This login already exists";
                        return RedirectToAction(nameof(AccountController.Register), "Account");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginCheck(LoginViewModel logInfos)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // Move comparaison, and searching of person in an other layer
                    PersonBAL balPerson = new PersonBAL();
                    Person personConnecting = balPerson.GetPersonByLogin(logInfos.Login,db);
                    // Move comparaison in an other laver
                    if (personConnecting != null && personConnecting.Password == HashService.GenerateSHA256String(logInfos.Password))
                    {
                        Session["User"] = personConnecting;
                        ViewBag.User = Session["User"];
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        Session["User"] = null;
                        TempData["LoginError"] = "Invalid login or password";
                        return RedirectToAction(nameof(AccountController.Login), "Account");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            Session["User"] = null;
            TempData["LoginError"] = "Invalid login or password";
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        ////TODO Move to Buisiness Layer
        //private bool IsLoginValid(string login)
        //{
        //    Person personConnecting = this.db.People.FirstOrDefault(p => p.Login == login);
        //    return (personConnecting != null) ? false : true;

        //}

    }
}