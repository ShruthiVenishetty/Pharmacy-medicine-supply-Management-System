using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq;
using MedicalRepresentativeScheduleAPI.Models;
using MedicalRepresentativeScheduleAPI.Repository;
using MedicalRepresentativeScheduleAPI.Controllers;
using System.Collections.Generic;
//using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System;
using System.Threading.Tasks;

namespace ScheduleTesting
{
    public class UnitTest1
    {
        public ScheduleRepo sr = new ScheduleRepo();
        MedicineStock ms = new MedicineStock();
        Schedule s = new Schedule();
        List<Schedule> dataObject = new List<Schedule>();

        [TestCase]
        public void RepoPassTest()
        {
            Mock<IScheduleRepo> supRepMock = new Mock<IScheduleRepo>();
            ScheduleRepo scheduleRepoObject = new ScheduleRepo();
            supRepMock.Setup(x => x.GetSchedules(DateTime.Now)).Returns(dataObject);
            List<Schedule> listOfPharma = scheduleRepoObject.GetSchedules(DateTime.Now);
            NUnit.Framework.Assert.IsNotNull(listOfPharma);
        }

        [TestCase]
        public void RepoFailTest()
        {
            Mock<IScheduleRepo> supRepMock = new Mock<IScheduleRepo>();
            ScheduleRepo scheduleRepoObject = new ScheduleRepo();
            supRepMock.Setup(x => x.GetSchedules(DateTime.Now)).Returns(dataObject);
            List<Schedule> listOfPharma = scheduleRepoObject.GetSchedules(DateTime.Now);
            NUnit.Framework.Assert.That(listOfPharma, Has.No.Member("D6"));
        }

        //[Test]
        //public async Task Test_GetMedicineName()
        //{
        //    Mock<IScheduleRepo> supplyMock = new Mock<IScheduleRepo>();
        //    ScheduleController sc = new ScheduleController(supplyMock.Object);
        //    var result = await (sr.GetAllStock() as Task<List<MedicineStock>>);
        //    var resultList = result.Result;
        //    NUnit.Framework.Assert.Multiple(() =>
        //    {
        //        NUnit.Framework.Assert.IsNotNull(result[0].Name);
        //        NUnit.Framework.Assert.IsNotNull(result[1].Name);
        //        NUnit.Framework.Assert.IsNotNull(result[2].Name);
        //        NUnit.Framework.Assert.IsNotNull(result[3].Name);
        //        NUnit.Framework.Assert.IsNotNull(result[4].Name);

        //    });
        //}

    }
}
