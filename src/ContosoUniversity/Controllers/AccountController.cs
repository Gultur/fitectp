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
            PersonRegisterViewModel model = new PersonRegisterViewModel();
            return View(model);
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
                        // TODO : Model creation need to be in a other layer
                        if (newAccount.Roles == RolesEnum.Student)
                        {
                            Student newStudent = new Student();
                            newStudent.LastName = newAccount.LastName;
                            newStudent.FirstMidName = newAccount.FirstMidName;
                            newStudent.Login = newAccount.Login;
                            newStudent.Password = GenerateSHA256String(newAccount.Password);
                            newStudent.EnrollmentDate = DateTime.Now;
                            db.Students.Add(newStudent);


                        }
                        else
                        {
                            Instructor newInstructor = new Instructor();
                            newInstructor.LastName = newAccount.LastName;
                            newInstructor.FirstMidName = newAccount.FirstMidName;
                            newInstructor.Login = newAccount.Login;
                            newInstructor.Password = GenerateSHA256String(newAccount.Password);
                            newInstructor.HireDate = DateTime.Now;
                            db.Instructors.Add(newInstructor);
                        }

                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["LoginError"] = "This login is not available";
                        return RedirectToAction("Register", "Account");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            return RedirectToAction("Index", "Home");
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
                    if (personConnecting != null && personConnecting.Password == GenerateSHA256String(logInfos.Password))
                    {
                        Session["User"] = personConnecting;
                        ViewBag.User = Session["User"];
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["User"] = null;
                        TempData["LoginError"] = "Invalid Credentials";
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            Session["User"] = null;
            TempData["LoginError"] = "Invalid Credentials";
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        private bool IsLoginValid(string login)
        {
            Person personConnecting = this.db.People.FirstOrDefault(p => p.Login == login);
            return (personConnecting != null) ? false : true;

        }

        // Service encodage - Helper?
        // TODO : Move to external service
        private static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash);
        }
    }
}