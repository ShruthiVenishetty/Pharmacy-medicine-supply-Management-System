using JwtAuth.Controllers;
using JwtAuth.Models;
using JwtAuth.Repository;
using JwtAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace JwtAuthTest
{
    [TestFixture]
    public class AccountControllerTest
    {  

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoginTestPass()
        {
            //Arrange
            Login cred = new Login { Username = "user", Password = "pass" };

            User user = new User { Username = "user", Password = "pass", Role = "Supplier" };

            var mockAuthRepo = new Mock<IAuthRepo>();
            mockAuthRepo.Setup(x => x.AuthenticateUser(cred)).Returns(user);

            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(x => x.CreateToken(user)).Returns("token string");

            var controller = new AccountController(mockAuthRepo.Object, mockTokenService.Object);

            //Act
            var result = controller.Login(cred);
            var okResult = result as OkObjectResult;    

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
           
        }

        [Test]
        public void LoginTestFail()
        {
            //Arrange
            Login cred = new Login { Username = "invalid", Password = "invalid" };

            User user = null;

            var mockAuthRepo = new Mock<IAuthRepo>();
            mockAuthRepo.Setup(x => x.AuthenticateUser(cred)).Returns(user);

            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(x => x.CreateToken(user)).Returns("token string");

            var controller = new AccountController(mockAuthRepo.Object, mockTokenService.Object);

            //Act
            var result = controller.Login(cred);
            var unauthResult = result as Microsoft.AspNetCore.Mvc.UnauthorizedResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(401, unauthResult.StatusCode);

        }


    }
}