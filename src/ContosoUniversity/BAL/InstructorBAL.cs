using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BAL
{
    public class InstructorBAL
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public void CreateInstructorRegistering(PersonRegisterViewModel newAccount, SchoolContext db)
        {
            try
            {
                using (this.db)
                {
                    Instructor newInstructor = new Instructor();
                    newInstructor.LastName = newAccount.LastName;
                    newInstructor.FirstMidName = newAccount.FirstMidName;
                    newInstructor.Login = newAccount.Login;
                    newInstructor.Password = HashService.GenerateSHA256String(newAccount.Password);
                    newInstructor.HireDate = DateTime.Now;
                    db.Instructors.Add(newInstructor);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}