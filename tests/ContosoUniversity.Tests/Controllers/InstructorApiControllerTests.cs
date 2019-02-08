using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
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
        public void Call_Instructor_Api_With_BadID_Send_Error_Message()
        {
            var api_response = controllerToTest.Get(-1);
            Dictionary<string, string> json = new Dictionary<string, string>();
            json["Status"] = "Error";
            json["Message"] = "There is no instructor with that Id";

            var awaited_response = JsonConvert.SerializeObject(json);
            

            Assert.That(api_response == awaited_response);
        }

        [Test]
        public void Call_Instructor_Api_With_GoodID_Success()
        {
            Assert.That(false);
        }

    }
}
