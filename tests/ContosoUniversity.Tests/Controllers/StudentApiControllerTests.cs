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
    class StudentApiControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentApiController controllerToTest;
        private SchoolContext dbContext;

        [Test]
        public void Call_Api_With_BadID_Send_Error_Message()
        {
            Assert.That(false);
        }

        [Test]
        public void Call_Api_With_GoodID_Success()
        {
            Assert.That(false);
        }

    }
}
