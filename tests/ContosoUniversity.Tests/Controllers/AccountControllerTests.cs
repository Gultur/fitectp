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
    class AccountControllerTests: IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private AccountController controllerToTest;
        private SchoolContext dbContext;

        [Test]
        public void Login_InvalidCredential_Fail()
        {
            Assert.That(false);
        }

        [Test]
        public void Login_ValidLoginInvalidPawword_Fail()
        {
            Assert.That(false);
        }

        // A completer
    }
}
