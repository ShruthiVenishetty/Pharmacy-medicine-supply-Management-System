//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using NUnit.Framework;
//using MedicalRepresentativeScheduleAPI.Controllers;
//using MedicalRepresentativeScheduleAPI.Models;
//using MedicalRepresentativeScheduleAPI.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;



//namespace StockTesting.Controllers
//{
//    [TestFixture]
//    class StockControllerTest
//    {
//        [TestCase]
//        public void Test_GetMedicineStock()
//        {

//            ScheduleRepo sr = new ScheduleRepo();

//            var controller = new ScheduleController(sr);
//            var actionResult = controller.GetRepSchedule(DateTime.Now);

//            var okResult = actionResult as OkObjectResult;




//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//        }
//    }
//}