using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleAPI.Models
{
    public class Schedule
    {
        public string RepName { get; set; }
        public string DoctorName { get; set; }
        public string TreatingAilment { get; set; }
        public string Slot { get; set; }
        public string DoctorContactNo { get; set; }

        public List<string> MedicineName { get; set; }
        public DateTime date { get; set; }

    }
}
