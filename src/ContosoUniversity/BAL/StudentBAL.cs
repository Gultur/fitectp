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
    public class StudentBAL
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public void CreateStudentRegistering(PersonRegisterViewModel newAccount)
        {
            try
            {
                using (this.DbContext)
                {
                    Student newStudent = new Student();
                    newStudent.LastName = newAccount.LastName;
                    newStudent.FirstMidName = newAccount.FirstMidName;
                    newStudent.Login = newAccount.Login;
                    newStudent.Password = HashService.GenerateSHA256String(newAccount.Password);
                    newStudent.EnrollmentDate = DateTime.Now;
                    db.Students.Add(newStudent);
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