using MedicalRepresentativeScheduleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleAPI.Repository
{
    public interface IScheduleRepo
    {
        public  List<Schedule> GetSchedules(DateTime scheduleStartDate);
    }
}