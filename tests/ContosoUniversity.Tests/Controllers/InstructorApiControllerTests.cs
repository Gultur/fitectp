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
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;


namespace ContosoUniversity.Tests.Controllers
{
    class InstructorApiControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private InstructorApiController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new InstructorApiController();
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            //controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void Call_Instructor_Api_With_BadID_Send_Error_Message()
        {
            #region Arrange
            Dictionary<string, string> json = new Dictionary<string, string>();
            json["Status"] = "Error";
            json["Message"] = "There is no instructor with that Id";
            var awaited_response = JsonConvert.SerializeObject(json);
            #endregion

            #region Act
            var api_response = controllerToTest.Get(999999);
            var response = api_response as OkNegotiatedContentResult<Dictionary<string, string>>;
            var json_response = JsonConvert.SerializeObject(response.Content);
            #endregion


            #region assert
            Assert.That(api_response.GetType() == typeof(OkNegotiatedContentResult<Dictionary<string, string>>));
            Assert.That(json_response == awaited_response);
            #endregion

        }

        //TODO : need to mock the response
        [Test]
        public void Call_Instructor_Api_With_GoodID_Success()
        {
            #region Arrange
            Dictionary<string, string> json = new Dictionary<string, string>();
            json["Status"] = "Error";
            json["Message"] = "There is no student with that Id";
            var awaited_response = JsonConvert.SerializeObject(json);
            #endregion

            #region Act
            var api_response = controllerToTest.Get(1);
            var response = api_response as OkNegotiatedContentResult<Dictionary<string, string>>;
            var json_response = JsonConvert.SerializeObject(response.Content);
            #endregion


            #region assert
            Assert.That(api_response.GetType() == typeof(OkNegotiatedContentResult<Dictionary<string, string>>));
            Assert.That(json_response != awaited_response);
            #endregion
        }

    }
}
