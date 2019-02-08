using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create(int id)
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.StudentID = id;
            return View();
        }

        // POST: Enrollments/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,StudentID")] Enrollment enrollment)
        {
            Person student = (Person)Session["User"];
            //Course already exists ?
            if (CheckCourse(student.ID, enrollment.CourseID))
            {
                TempData["CreateError"] = $"You have already subscribed at this course";
                return RedirectToAction(nameof(EnrollmentController.Create), "Enrollment");
            }
            //Many Occurences of courses?
            //if (CheckPlanCourse(student.ID, enrollment.CourseID))
            //{
            //    TempData["CreateError"] = $"overlapping hours";
            //    return RedirectToAction(nameof(EnrollmentController.Create), "Enrollment");
            //}


            if (ModelState.IsValid)
            {
                enrollment.StudentID = student.ID;
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Details", "Student", new { id = student.ID });
            }

            //Ce code à modifier après test cours existe ou chevauchement.
            return RedirectToAction("Details", "Student", new { id = enrollment.CourseID });
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.People, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewBag.StudentID = new SelectList(db.People, "ID", "LastName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool CheckCourse(int ID, int CourseID)
        {
            bool Exist = true;
            Enrollment CourseExist = db.Enrollments.FirstOrDefault(c => c.CourseID == CourseID && c.StudentID == ID);
            if (CourseExist == null)
            {
                Exist = false;
            }
            return Exist;
        }

        //public bool CheckPlanCourse(int StudentID, int CourseID)
        //{
        //    //retrouver les horaies des cours déja inscrits pour l'étudiant ID
        //    bool Chevauchement = false;
        //    List<Enrollment> enrollments = db.Enrollments.Where(e => e.StudentID == StudentID).ToList();
        //    List < Lesson > studentLessons= new List<Lesson>();
        //    foreach(Enrollment enrollment in enrollments){


        //        foreach(Course course in db.Courses.Where(c => c.CourseID == enrollment.CourseID).ToList())
        //        {
        //            studentLessons.AddRange(course.Lessons.ToList());
        //        }

        //    }

        //    List<Lesson> courseLessons = db.Lessons.Where(l => l.CourseID == CourseID).ToList();


        //if (CourseExist == null)
        //{
        //    Exist = false;
        //}
        //return Exist;




        // retrouver les horaies des cours déja inscrits pour l'étudiant ID


        // retrouver les horaies du cours à inscrire  CourseID


        // Comparer les horaires pour détecter les chauvauchements.
        //---------------------------------------------------------

        // Boucle sur les horaires du cours à inscrire
        //calculer hor_finN
        //bouclle sur les horaires des cours deja inscrits
        //calculer hor_finI
        // si même jour 
        // tester chevauchement
        // si chevauchement, mettre à true la variable et sortir de la boucle
        // fin




    }
}

