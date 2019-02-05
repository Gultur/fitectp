using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class LessonController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Lesson
        public ActionResult Index()
        {
            return View(db.Lessons.ToList());
        }

        // GET: Lesson/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lesson/Create
        public ActionResult Create()
        {
            CreateLessonViewModel model = new CreateLessonViewModel();
            // TODO : Add a query to select only Department Course for the instructor
            // in a data layer
            model.Day = new Enum.DayOfCourse();
            model.Course = db.Courses.ToList();

            return View(model);
        }

        // POST: Lesson/CreateLesson
        [HttpPost]
        public ActionResult CreateLesson(Enum.DayOfCourse Day, string Course, int StartHour, int EndHour)
        {
            
            try
            {
                if (StartHour >= EndHour)
                {
                    TempData["LoginError"] = "Endhour must be after StartHour";
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");

                }
                else
                {
                    int CourseId = int.Parse(Course);
                    Lesson lesson = new Lesson();
                    lesson.InstructorID = 10;
                    lesson.Course = db.Courses.FirstOrDefault(c => c.CourseID == CourseId);
                    lesson.Day = Day;
                    lesson.StartHour = StartHour;
                    lesson.EndHour = EndHour;
                    db.Lessons.Add(lesson);


                    db.SaveChanges();
                    return RedirectToAction(nameof(LessonController.Index), "Lesson");
                }  

                

            }
            catch (Exception)
            {

                ModelState.AddModelError("Error", "Error");
            }
            return RedirectToAction(nameof(LessonController.Index), "Lesson");
        }

        // GET: Lesson/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lesson/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lesson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lesson/DeleteLesson/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                Lesson lesson = db.Lessons.Find(id);
                db.Lessons.Remove(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", "Lesson", new {id}); ;
            }
        }

    }
}
