using MedicineSupplyPortal.Models;
using MedicineSupplyPortal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Controllers
{
    public class ScheduleController : Controller
    {
        private ISchedulePortal _schedulePortal;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ScheduleController));

        public string Token { get => HttpContext.Session.GetString("Token"); }
        public string Role { get => HttpContext.Session.GetString("Role"); }

        public ScheduleController(ISchedulePortal schedulePortal)
        {
            _schedulePortal = schedulePortal;
        }
        public IActionResult SetSchedule()
        {
            _log4net.Info("[ScheduleController]=>SetSchedule");

            if (Token== null)
            {
                _log4net.Info("[ScheduleController]=>SetSchedule : Token is NULL");
                return RedirectToAction("Login", "Login");
            }
                

            if (Role == "Representative")
            {
                _log4net.Info("[ScheduleController]=>SetSchedule : Role is Representative");
                StartDate startDate = new StartDate { Date = DateTime.Now };
                return View("SetSchedule",startDate);
            }
            else
            {
                _log4net.Info("[ScheduleController]=>SetSchedule : Unauthorized");
                return View("Unauthorized");
            }    

        }

        [HttpPost]
        public async Task<IActionResult> Schedule(StartDate startDate)
        {
            _log4net.Info("[ScheduleController]=>Schedule");

            if (Token == null)
            {
                _log4net.Info("[ScheduleController]=>Schedule : Token is NULL");
                return RedirectToAction("Login", "Login");
            }
                

            if (Role == "Representative")
            {
                _log4net.Info("[ScheduleController]=>Schedule : Role is Representative");

                var schedule = await _schedulePortal.CreateSchedule(startDate.Date);

                if (schedule.Count != 0 && schedule != null)
                {
                    _log4net.Info("[ScheduleController]=>Schedule : Schedule (View)");
                    return View("Schedule",schedule);
                }
                else
                {
                    _log4net.Info("[ScheduleController]=>Schedule : Unauthorized");
                    return BadRequest();
                }
                    
            }
            else
            {
                _log4net.Info("[ScheduleController]=>Schedule : Schedule (View)");
                return View("Unauthorized");
            }  

        } 
    }
}
