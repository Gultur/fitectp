using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using ContosoUniversity.ViewModels;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;


namespace ContosoUniversity.Tests.Controllers
{
    class AccountControllerTests: IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private AccountController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new AccountController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

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
            PersonRegisterViewModel newAccount = new PersonRegisterViewModel();
            Assert.That(false);
        }

        // TODO : Validation viewmodel stop the test
        [Test]
        public void Register_Student_With_Bad_Password_Fail()
        {
            // Arrange
            //PersonRegisterViewModel newAccount = new PersonRegisterViewModel();
            //newAccount.FirstMidName = "Stu";
            //newAccount.LastName = "Stu";
            //newAccount.Roles = EnumRoles.Student;
            //newAccount.Login = "Stu";
            //newAccount.Password = "Stustu";
            //newAccount.PasswordConfirmation = "Jemesuistrompe";

            //Act
            //BAL.StudentBAL bal = new BAL.StudentBAL();
            //bal.CreateStudentRegistering(newAccount, dbContext);

            //Student stu = this.dbContext.Students.FirstOrDefault(e => e.Login == newAccount.Login);

            //Assert
            //Assert.That(stu == null);

            Assert.That(false);
        }

        // TODO : Validation viewmodel stop the test
        [Test]
        public void Register_Instructor_With_Bad_Password_Fail()
        {
            //Arrange
            //PersonRegisterViewModel newAccount = new PersonRegisterViewModel();
            //newAccount.FirstMidName = "Inst";
            //newAccount.LastName = "Inst";
            //newAccount.Roles = EnumRoles.Instructor;
            //newAccount.Login = "Inst";
            //newAccount.Password = "InstInst";
            //newAccount.PasswordConfirmation = "Jemesuistrompe";

            //ACt
            //BAL.InstructorBAL bal = new BAL.InstructorBAL();
            //bal.CreateInstructorRegistering(newAccount, dbContext);

            //Instructor inst = this.dbContext.Instructors.FirstOrDefault(e => e.Login == newAccount.Login);

            //Assert
            //Assert.That(inst == null);

            Assert.That(false);
        }

        // TODO : find why this test fail
        // Instructor is being added in base with lower, why?
        [Test]
        public void Register_Instructor_With_Non_Existing_Login_And_Good_Password_Success()
        {
            PersonRegisterViewModel newAccount = new PersonRegisterViewModel();
            newAccount.FirstMidName = "Inst";
            newAccount.LastName = "Inst";
            newAccount.Roles = EnumRoles.Instructor;
            newAccount.Login = "Inst";
            newAccount.Password = "InstInst";
            newAccount.PasswordConfirmation = "InstInst";

            BAL.InstructorBAL bal = new BAL.InstructorBAL();
            bal.CreateInstructorRegistering(newAccount, dbContext);

            Person inst = this.dbContext.Instructors.FirstOrDefault(e => e.Login == newAccount.Login);


            Assert.That(inst != null && inst.LastName == "Inst");
        }

        [Test]
        public void Register_Student_With_Non_Existing_Login_And_Good_Password_Success()
        {
            #region Arrange
            PersonRegisterViewModel newAccount = new PersonRegisterViewModel();
            newAccount.FirstMidName = "Stu";
            newAccount.LastName = "Stu";
            newAccount.Roles = EnumRoles.Student;
            newAccount.Login = "Stu";
            newAccount.Password = "Stustu";
            newAccount.PasswordConfirmation = "Stustu";

            BAL.StudentBAL bal = new BAL.StudentBAL();
            #endregion

            #region Act
            bal.CreateStudentRegistering(newAccount, dbContext);
            #endregion

            #region Assert


            Person stu = this.dbContext.Students.FirstOrDefault(e => e.Login == newAccount.Login);

            Assert.That(stu != null && stu.LastName == "Stu");
            #endregion
        }


        // TODO : move this test in a test controller for services
        [Test]
        public void Hashing_Same_word_Twice_Result_Two_Identical_Sha()
        {
            #region Arrange Act
            string pass1 = Services.HashService.GenerateSHA256String("word");
            string pass2 = Services.HashService.GenerateSHA256String("word");
            #endregion
            #region Assert
            Assert.That(pass1 == pass2);
            #endregion
        }

        [Test]
        public void Hashing_Different_word_Result_Two_Different_Sha()
        {
            #region Arrange Act
            string pass1 = Services.HashService.GenerateSHA256String("firstword");
            string pass2 = Services.HashService.GenerateSHA256String("otherword");
            #endregion
            #region Assert
            Assert.That(pass1 != pass2);
            #endregion
        }
    }
}
