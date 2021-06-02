using MedicineSupplyPortal.Controllers;
using MedicineSupplyPortal.Models;
using MedicineSupplyPortal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineSupplyPortalTest.UnitTests.Controllers
{
    [TestFixture]
    public class LoginControllerTest
    {
        private Mock<ILoginPortal> _mockLoginPortal;

        [Test]
        public void Login_Role_Representative_Test()
        {
            _mockLoginPortal = new Mock<ILoginPortal>();

            var controller = new LoginController(_mockLoginPortal.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "Representative";

            var Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token", out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = controller.Login();

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("SetSchedule", redirectToAction.ActionName);
        }

        [Test]
        public void Login_Role_Supplier_Test()
        {
            _mockLoginPortal = new Mock<ILoginPortal>();

            var controller = new LoginController(_mockLoginPortal.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "Supplier";

            var Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token", out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = controller.Login();

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("MedicineDemand", redirectToAction.ActionName);
        }

        [Test]
        public void Login_Null_Token_Test()
        {
            _mockLoginPortal = new Mock<ILoginPortal>();

            var controller = new LoginController(_mockLoginPortal.Object);

            //string tokenValue = "Token";

            byte[] Tvalue = null;

            string RoleValue = "Supplier";

            var Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token", out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = controller.Login();

            var viewResult = result as ViewResult;
            
            Assert.AreEqual("Login", viewResult.ViewName);
        }

        [Test]
        public void LogOutTest()
        {
            _mockLoginPortal = new Mock<ILoginPortal>();

            var controller = new LoginController(_mockLoginPortal.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "Supplier";

            var Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token", out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = controller.LogOut();

            Assert.AreEqual(0,controller.ControllerContext.HttpContext.Session.Keys.Count());

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectToAction.ActionName);

        }

        [Test]
        public async Task LoginPost_Role_Representative_Test()
        {
            Login mockCred = new Login
            {
                Username = "user",
                Password = "pass"
            };

            AuthResponse mockAuthResponse = new AuthResponse
            {
                Token = "Token",
                Role ="Representative"
            };

            _mockLoginPortal = new Mock<ILoginPortal>();
            _mockLoginPortal.Setup(x => x.AuthenticateUser(mockCred)).ReturnsAsync(mockAuthResponse);

            var controller = new LoginController(_mockLoginPortal.Object);

            Mock<ISession> mockSession = new Mock<ISession>();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = await controller.Login(mockCred);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("SetSchedule", redirectToAction.ActionName);

        }

        [Test]
        public async Task LoginPost_Role_Supplier_Test()
        {
            Login mockCred = new Login
            {
                Username = "user",
                Password = "pass"
            };

            AuthResponse mockAuthResponse = new AuthResponse
            {
                Token = "Token",
                Role = "Supplier"
            };

            _mockLoginPortal = new Mock<ILoginPortal>();
            _mockLoginPortal.Setup(x => x.AuthenticateUser(mockCred)).ReturnsAsync(mockAuthResponse);

            var controller = new LoginController(_mockLoginPortal.Object);

            Mock<ISession> mockSession = new Mock<ISession>();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = await controller.Login(mockCred);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("MedicineDemand", redirectToAction.ActionName);

        }

        [Test]
        public async Task LoginPost_InvalidCredentials_Test()
        {
            Login mockCred = new Login
            {
                Username = "user",
                Password = "pass"
            };

            AuthResponse mockAuthResponse = null;

            _mockLoginPortal = new Mock<ILoginPortal>();
            _mockLoginPortal.Setup(x => x.AuthenticateUser(mockCred)).ReturnsAsync(mockAuthResponse);

            var controller = new LoginController(_mockLoginPortal.Object);

            Mock<ISession> mockSession = new Mock<ISession>();
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = await controller.Login(mockCred);

            var viewResult = result as ViewResult;

            Assert.AreEqual("Login",viewResult.ViewName);

        }

    }
}
