using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineSupplyPortal.Controllers;
using MedicineSupplyPortal.Data;
using MedicineSupplyPortal.Models;
using MedicineSupplyPortal.Repository;
using Moq;
using NUnit.Framework;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineSupplyPortalTest.UnitTests.Controllers
{
    [TestFixture]
    public class StockControllerTest
    {
        private Mock<ISupplyPortal> _supplyPortal;
        private Mock<ISaveData> _saveData;


        [Test]
        public async Task MedicineDemand_Role_Supplier_Test()
        {

            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetMedicineDemand()).ReturnsAsync(new List<MedicineDemand>());

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "Supplier";

            var Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token",out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = await controller.MedicineDemand();

            var viewresult = result as ViewResult;

            Assert.AreEqual("MedicineDemand", viewresult.ViewName);   
        }

        [Test]
        public async Task MedicineDemand_Role_Not_Supplier_Test()
        {

            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetMedicineDemand()).ReturnsAsync(new List<MedicineDemand>());

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "Not_Supplier";

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

            var result = await controller.MedicineDemand();

            var viewresult = result as ViewResult;

            Assert.AreEqual("Unauthorized", viewresult.ViewName);
        }

        [Test]
        public async Task MedicineDemand_Null_Token_Test()
        {
            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetMedicineDemand()).ReturnsAsync(new List<MedicineDemand>());

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

            //string tokenValue = "Token";

            byte[] Tvalue = null;

            string RoleValue = "Supplier";

            byte[] Rvalue = Encoding.ASCII.GetBytes(RoleValue);

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(x => x.TryGetValue("Token", out Tvalue)).Returns(true);
            mockSession.Setup(x => x.TryGetValue("Role", out Rvalue)).Returns(true);

            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.Session).Returns(mockSession.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            var result = await controller.MedicineDemand();

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result; 
            Assert.AreEqual("Login", redirectToAction.ActionName);

        }


        [Test]
        public async Task PharmacySupplies_Role_Supplier_Test()
        {
            List<MedicineDemand> testMedicineDemand = new List<MedicineDemand>
            {
                new MedicineDemand { MedicineName="TestMed",DemandCount=20 }
            };

            var testMedicineAndSupply = new List<MedicineNameAndSupply>();


            List<PharmacyMedicineSupply> testStockSupplies = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{PharmacyName="P1", medicineAndSupply=testMedicineAndSupply}
            };

            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetSupplies(testMedicineDemand)).ReturnsAsync(testStockSupplies);

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

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

            var result = await controller.PharmacySupplies(testMedicineDemand);

            var viewresult = result as ViewResult;

            Assert.AreEqual("PharmacySupplies", viewresult.ViewName);
        }


        [Test]
        public async Task PharmacySupplies_Role_Not_Supplier_Test()
        {
            List<MedicineDemand> testMedicineDemand = new List<MedicineDemand>
            {
                new MedicineDemand { MedicineName="TestMed",DemandCount=20 }
            };

            var testMedicineAndSupply = new List<MedicineNameAndSupply>();


            List<PharmacyMedicineSupply> testStockSupplies = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{PharmacyName="P1", medicineAndSupply=testMedicineAndSupply}
            };

            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetSupplies(testMedicineDemand)).ReturnsAsync(testStockSupplies);

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

            string tokenValue = "Token";

            var Tvalue = Encoding.ASCII.GetBytes(tokenValue);

            string RoleValue = "NotSupplier";

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

            var result = await controller.PharmacySupplies(testMedicineDemand);

            var viewresult = result as ViewResult;

            Assert.AreEqual("Unauthorized", viewresult.ViewName);
        }

        [Test]
        public async Task PharmacySupplies_Null_Token_Test()
        {
            List<MedicineDemand> testMedicineDemand = new List<MedicineDemand>
            {
                new MedicineDemand { MedicineName="TestMed",DemandCount=20 }
            };

            var testMedicineAndSupply = new List<MedicineNameAndSupply>();


            List<PharmacyMedicineSupply> testStockSupplies = new List<PharmacyMedicineSupply>
            {
                new PharmacyMedicineSupply{PharmacyName="P1", medicineAndSupply=testMedicineAndSupply}
            };

            _supplyPortal = new Mock<ISupplyPortal>();
            _supplyPortal.Setup(x => x.GetSupplies(testMedicineDemand)).ReturnsAsync(testStockSupplies);

            _saveData = new Mock<ISaveData>();

            var controller = new StockController(_supplyPortal.Object, _saveData.Object);

            //string tokenValue = "Token";

            byte[] Tvalue = null;

            string RoleValue = "NotSupplier";

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

            var result = await controller.PharmacySupplies(testMedicineDemand);

            Assert.IsAssignableFrom<RedirectToActionResult>(result);

            var redirectToAction = (RedirectToActionResult)result;
            Assert.AreEqual("Login", redirectToAction.ActionName);
        }
    }
}
