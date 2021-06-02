using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using StockAPI.Controllers;
using StockAPI.Models;
using StockAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace StockTesting.Controllers
{
    [TestFixture]
    class StockControllerTest
    {
        [TestCase]
        public void Test_GetMedicineStock()
        {

            Stockrepo sr = new Stockrepo();

            var controller = new StockController(sr);
            var actionResult = controller.GetStock();

            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}