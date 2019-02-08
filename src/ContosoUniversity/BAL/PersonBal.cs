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
    public class PersonBAL
    {
        // Obselete, add db in parameter (easier for testing)
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        public Person GetPersonByLogin(string login, SchoolContext db)
        {
            try
            {
                using (db)
                {
                    Person person = db.People.FirstOrDefault(p => p.Login == login);
                    return person;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool IsLoginValid(string login, SchoolContext db)
        {
            Person personConnecting = GetPersonByLogin(login, db);
            return (personConnecting != null) ? false : true;

        }

    }
}