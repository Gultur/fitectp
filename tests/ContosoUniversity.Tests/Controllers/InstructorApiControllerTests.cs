using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ContosoUniversity.Tests.Controllers
{
    class InstructorApiControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private InstructorApiController controllerToTest;
        private SchoolContext dbContext;

        [Test]
        public void Call_Api_With_BadID_Send_Error_Message()
        {
            var api_response = controllerToTest.Get(-1);
            //var awaited_response = JsonConvert.SerializeObject(myDictionary)new JObject( "Status":"Error", "Message":"There is no instructor with that Id" );

            Assert.That(false);
        }

        [Test]
        public void Call_Api_With_GoodID_Success()
        {
            Assert.That(false);
        }

    }
}
