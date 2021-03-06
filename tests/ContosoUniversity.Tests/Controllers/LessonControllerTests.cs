﻿using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ContosoUniversity.Tests.Controllers
{
    class LessonControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private LessonController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new LessonController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void Create_Valid_Lesson_Success()
        {
            Assert.That(false);
        }

        [Test]
        public void Create_Invalid_Lesson_Fail()
        {
            Assert.That(false);
        }

        [Test]
        public void Edit_Lesson_With_Valid_Value_Success()
        {
            Assert.That(false);
        }
        [Test]
        public void Edit_Lesson_With_Invalid_Value_Fail()
        {
            Assert.That(false);
        }

        [Test]
        public void Edit_Non_Existing_Lesson_redirect()
        {
            #region Arrange Act
            ActionResult result = controllerToTest.Edit(9999999);
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;

            #endregion
            #region Assert
            //Assert.That(result is RedirectResult);
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
            #endregion
        }

        [Test]
        public void Edit_Existing_Lesson_return_View()
        {
            #region Arrange Act
            ActionResult result = controllerToTest.Details(1);

            #endregion
            #region Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
            #endregion
        }

        // Testing if an Instructor can create a lesson when he is not available at lesson hours
        // 8 case

        // Instructor has already some lessons in base
        // Same instructor, other day , courses hours differents
        [Test]
        public void Create_Lesson_SameInstr_OtherDay_Othershours_success()
        {
            Assert.That(false);
        }
        
        // Same instructor, same day , courses hours differents
        [Test]
        public void Create_Lesson_SameInstr_SameDay_Othershours_success()
        {
            Assert.That(false);
        } 
        
        // Same instructor, same day , courses hours presents in other lesson
        [Test]
        public void Create_Lesson_SameInstr_SameDay_ExistingHours_fail()
        {
            Assert.That(false);
        }

        // Same instructor, other day , courses hours presents in other lesson
        [Test]
        public void Create_Lesson_SameInstr_OtherDay_ExistingHours_success()
        {
            Assert.That(false);
        }
        
        // No existing lesson with this instructors
        [Test]
        public void Create_Lesson_FirstLesson_OtherDay_Othershours_success()
        {
            Assert.That(false);
        }
    }
}
