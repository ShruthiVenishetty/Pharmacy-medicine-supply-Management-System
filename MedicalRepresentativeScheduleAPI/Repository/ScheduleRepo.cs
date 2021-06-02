using MedicalRepresentativeScheduleAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;

namespace MedicalRepresentativeScheduleAPI.Repository
{
    public class ScheduleRepo : IScheduleRepo

    {
        
       static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ScheduleRepo));
       // string baseurl = "https://localhost:44364/";

        public List<Schedule> GetSchedules(DateTime scheduleStartDate)
        {
            _log4net.Info("GetSchedules is invoked");
            List<MedicineStock> medStock = new List<MedicineStock>();
            List<Schedule> schedule = new List<Schedule>();

            _log4net.Info("Data is being Read.");
            try
            {
                foreach (Schedule s in DoctorData.DoctorList)
                {
                    schedule.Add(s);
                }

                schedule[0].RepName = "R1";
                schedule[1].RepName = "R2";
                schedule[2].RepName = "R3";
                schedule[3].RepName = "R1";
                schedule[4].RepName = "R2";


                DateTime da = scheduleStartDate;
                for (int i = 0; i < schedule.Count; i++)
                {
                    schedule[i].Slot = "1PM to 2PM";

                    if (da.DayOfWeek != DayOfWeek.Sunday)
                    {
                        schedule[i].date = da;
                    }
                    else if (da.DayOfWeek == DayOfWeek.Sunday)
                    {
                        da = da.AddDays(1);
                        schedule[i].date = da;
                    }
                    da = da.AddDays(1);

                }

                List<MedicineStock> ms = new List<MedicineStock>();
                ms = GetAllStock().Result;
                foreach (Schedule sc in schedule)
                {
                    sc.MedicineName = new List<string>();

                    foreach (MedicineStock m in ms)
                    {

                        if (sc.TreatingAilment == m.TargetAilment)
                        {
                            string str = m.Name;
                            sc.MedicineName.Add(str);

                        }

                    }

                }
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("No Values Found " + e);
                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("No Values Found" + e);
                return null;
            }


            return schedule;
        }
     
        public async Task<List<MedicineStock>> GetAllStock()
        {
            _log4net.Info("GetAllStock is invoked");
            List<MedicineStock> li = new List<MedicineStock>();
            _log4net.Info("Data is being Read from MedicineStock api");
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://localhost:44397/MedicineStockInformation"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var response = await httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                       li  = JsonConvert.DeserializeObject<List<MedicineStock>>(result);
                    }
                }
            }
           
            return li;
        }

    }

}

