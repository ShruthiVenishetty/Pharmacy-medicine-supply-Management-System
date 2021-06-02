using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRepresentativeScheduleAPI.Models;

namespace MedicalRepresentativeScheduleAPI
{
    public class DoctorData
    {
        public static List<Schedule> DoctorList = new List<Schedule>
        {
            new Schedule{DoctorName="D1", TreatingAilment = "Orthopaedics", DoctorContactNo = "9884122113" },
            new Schedule{DoctorName="D2", TreatingAilment = "General", DoctorContactNo = "9876543210" },
            new Schedule{DoctorName="D3", TreatingAilment = "General", DoctorContactNo = "9876543211" },
            new Schedule{DoctorName="D4", TreatingAilment = "Orthopaedics", DoctorContactNo = "9876543212" },
            new Schedule{DoctorName="D5", TreatingAilment = "Gynaecology", DoctorContactNo = "9876543213" }
        };
    }
}