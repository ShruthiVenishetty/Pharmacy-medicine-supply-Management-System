using MedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Repository
{
    public interface ISchedulePortal
    {
        Task<List<Schedule>> CreateSchedule(DateTime date);
    }
}
