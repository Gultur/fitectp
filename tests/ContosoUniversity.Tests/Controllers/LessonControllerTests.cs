using ContosoUniversity.Controllers;
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
        public void Edit_Lesson_With_Inalid_Value_Fail()
        {
            Assert.That(false);
        }

        // A completer
    }
}
