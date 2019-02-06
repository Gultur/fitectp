﻿using ContosoUniversity.DAL;
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
            Lesson lesson = db.Lessons.FirstOrDefault(l => l.ID == id);
            return View(lesson);
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
        public ActionResult CreateLesson(Enum.DayOfCourse Day, string Course, int StartHour, int EndHour, DateTime Launch)
        {
            
            try
            {
                if (StartHour >= EndHour)
                {
                    TempData["CreateError"] = "Endhour must be after StartHour";
                    return RedirectToAction(nameof(LessonController.Create), "Lesson");

                }
                else
                {
                    int CourseId = int.Parse(Course);
                    // TODO : need to be in a differente layer, not in controller

                    Lesson lesson = new Lesson();
                    // TODO : Instructor ID must be ID of the connected Instructor

                    lesson.InstructorID = 10;
                    lesson.Course = db.Courses.FirstOrDefault(c => c.CourseID == CourseId);
                    lesson.Day = Day;
                    lesson.StartHour = StartHour;
                    lesson.EndHour = EndHour;
                    lesson.Launch = Launch;
                    // TODO : Test if the lesson is valid

                    if(!IsLessonValid(lesson))
                    {
                        TempData["CreateError"] = $"You have already a course between {lesson.StartHour} h and {lesson.EndHour} h {lesson.Day}";
                        return RedirectToAction(nameof(LessonController.Create), "Lesson");
                    }

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
            Lesson lesson = db.Lessons.FirstOrDefault(l => l.ID == id);
            EditLessonViewModel model = new EditLessonViewModel();
            ViewBag.CourseName = lesson.Course.Title;
            model.LessonID = lesson.ID;
            model.CourseID = lesson.Course.CourseID;
            model.Launch = lesson.Launch;
            model.Day = lesson.Day;
            model.StartHour = lesson.StartHour;
            model.EndHour = lesson.EndHour;
        
            return View(model);
        }

        // POST: Lesson/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult EditLesson(EditLessonViewModel newLesson)
        {
            // TODO Move entitie code to another layer

            try
            {
                if (newLesson.StartHour >= newLesson.EndHour)
                {
                    TempData["EditError"] = "Endhour must be after StartHour";
                    return RedirectToAction(nameof(LessonController.Edit), "Lesson", new { newLesson.LessonID });

                }

                Lesson lesson = db.Lessons.FirstOrDefault(l => l.ID == newLesson.LessonID);
                lesson.StartHour = newLesson.StartHour;
                lesson.EndHour = newLesson.EndHour;
                lesson.Launch = newLesson.Launch;
                lesson.Day = newLesson.Day;
                db.SaveChanges();

                return RedirectToAction(nameof(LessonController.Details), "Lesson", new { lesson.ID});
            }
            catch
            {
                return RedirectToAction(nameof(LessonController.Index), "Lesson");
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

        public bool IsLessonValid(Lesson lesson)
        {
            // We need to check if a lesson with the same Instructor existe between StartHour and EndHour

            List<Lesson> lessonsInstructors = db.Lessons.Where(l => l.InstructorID == lesson.InstructorID).ToList();
            List<Lesson> lessonsSameDay = lessonsInstructors.Where(l => l.Day == lesson.Day).ToList();
            List<Lesson> lessonsSameHours = lessonsSameDay.Where(l => (l.StartHour >= lesson.StartHour && l.StartHour <= lesson.EndHour)
                    || (l.EndHour <= lesson.EndHour && l.EndHour >= lesson.StartHour)).ToList();

            int count = db.Lessons.Where(l => l.InstructorID == lesson.InstructorID)
                .Where(l => l.Day == lesson.Day).Where(l => (l.StartHour >= lesson.StartHour && l.StartHour <= lesson.EndHour)
                    || (l.EndHour <= lesson.EndHour && l.EndHour >= lesson.StartHour)).ToList().Count;

            return (count == 0);
        }

    }
}
