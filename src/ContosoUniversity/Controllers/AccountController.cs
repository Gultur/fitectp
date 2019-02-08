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
                    if (IsLoginValid(newAccount.Login))
                    {

                        if (newAccount.Roles == EnumRoles.Student)
                        {
                            StudentBAL bal = new StudentBAL();
                            bal.CreateStudentRegistering(newAccount, db);
                        }
                        else
                        {
                            InstructorBAL bal = new InstructorBAL();
                            bal.CreateInstructorRegistering(newAccount, db);
                        }

                        Session["User"] = db.People.FirstOrDefault(p => p.Login == newAccount.Login);
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        TempData["LoginError"] = "This login is not available";
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
                    Person personConnecting = this.db.People.FirstOrDefault(p => p.Login == logInfos.Login);
                    if (personConnecting != null && personConnecting.Password == HashService.GenerateSHA256String(logInfos.Password))
                    {
                        Session["User"] = personConnecting;
                        ViewBag.User = Session["User"];
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        Session["User"] = null;
                        TempData["LoginError"] = "Invalid Credentials";
                        return RedirectToAction(nameof(AccountController.Login), "Account");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            Session["User"] = null;
            TempData["LoginError"] = "Invalid Credentials";
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //TODO Move to Buisiness Layer
        private bool IsLoginValid(string login)
        {
            Person personConnecting = this.db.People.FirstOrDefault(p => p.Login == login);
            return (personConnecting != null) ? false : true;

        }

    }
}