using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            model.CourseList = db.Courses.ToList();

            return View(model);
        }

        // POST: Lesson/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lesson/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
