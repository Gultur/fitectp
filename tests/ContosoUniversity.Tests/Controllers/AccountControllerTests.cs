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

        [Test]
        public void Register_With_Existing_Login_Fail()
        {
            Assert.That(false);
        }

        [Test]
        public void Register_With_Bag_Password_Fail()
        {
            Assert.That(false);
        }

        [Test]
        public void Register_With_Non_Existing_Login_And_Good_Password_Success()
        {
            Assert.That(false);
        }

        [Test]
        public void Hashing_Same_word_Twice_Result_Two_Identical_Sha()
        {
            Assert.That(false);
        }
    }
}
