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
    public class ScheduleControllerTest
    {
        private Mock<ISchedulePortal> _mockSchedulePortal;

        [Test]
        public void SetSchedule_Role_Representative_Test()
        {
            _mockSchedulePortal = new Mock<ISchedulePortal>();

            var controller = new ScheduleController(_mockSchedulePortal.Object);

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

            var result = controller.SetSchedule();

            var viewresult = result as ViewResult;

            Assert.AreEqual("SetSchedule", viewresult.ViewName);

        }

        [Test]
        public void SetSchedule_Not_Role_Representative_Test()
        {
            _mockSchedulePortal = new Mock<ISchedulePortal>();

            var controller = new ScheduleController(_mockSchedulePortal.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "NotRepresentative";

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

            var result = controller.SetSchedule();

            var viewresult = result as ViewResult;

            Assert.AreEqual("Unauthorized", viewresult.ViewName);

        }

        [Test]
        public void SetSchedule_Null_Token_Test()
        {
            _mockSchedulePortal = new Mock<ISchedulePortal>();

            var controller = new ScheduleController(_mockSchedulePortal.Object);

            //string tokenValue = "Token";

            byte[] Tvalue = null;

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

            var result = controller.SetSchedule();

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectToAction.ActionName);

        }

        [Test]
        public async Task Schedule_Role_Representative_Test()
        {
            var mockSchedule = new List<Schedule>
            {
                new Schedule{DoctorName="D1"}
            };

            var mockStartDate = new StartDate { Date = DateTime.Now };

            _mockSchedulePortal = new Mock<ISchedulePortal>();
            _mockSchedulePortal.Setup(x => x.CreateSchedule(mockStartDate.Date)).ReturnsAsync(mockSchedule);

            var controller = new ScheduleController(_mockSchedulePortal.Object);

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

            var result = await controller.Schedule(mockStartDate);

            var viewresult = result as ViewResult;

            Assert.AreEqual("Schedule", viewresult.ViewName);

        }

        [Test]
        public async Task Schedule_Role_Not_Representative_Test()
        {
            var mockSchedule = new List<Schedule>
            {
                new Schedule{DoctorName="D1"}
            };

            var mockStartDate = new StartDate { Date = DateTime.Now };

            _mockSchedulePortal = new Mock<ISchedulePortal>();
            _mockSchedulePortal.Setup(x => x.CreateSchedule(mockStartDate.Date)).ReturnsAsync(mockSchedule);

            var controller = new ScheduleController(_mockSchedulePortal.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "NotRepresentative";

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

            var result = await controller.Schedule(mockStartDate);

            var viewresult = result as ViewResult;

            Assert.AreEqual("Unauthorized", viewresult.ViewName);

        }

        [Test]
        public async Task Schedule_Null_Token_Test()
        {
            _mockSchedulePortal = new Mock<ISchedulePortal>();

            var controller = new ScheduleController(_mockSchedulePortal.Object);

            //string tokenValue = "Token";

            byte[] Tvalue = null;

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

            var result = await controller.Schedule(new StartDate());

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectToAction.ActionName);

        }

    }
}
