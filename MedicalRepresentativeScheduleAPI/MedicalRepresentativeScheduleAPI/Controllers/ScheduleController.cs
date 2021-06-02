using MedicalRepresentativeScheduleAPI.Models;
using MedicalRepresentativeScheduleAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRepresentativeScheduleAPI.Controllers
{
    
    public class ScheduleController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ScheduleController));
        private readonly IScheduleRepo _schedulerepo;


        public ScheduleController(IScheduleRepo schedulerepo)
        {
            _schedulerepo = schedulerepo;
        }

        [HttpGet("RepSchedule")]
        [Authorize(Roles = "Representative")]
        public ActionResult GetRepSchedule(DateTime d)
        {
            _log4net.Info("Controller GetRepSchedule is invoked");
            List<Schedule> schedules = new List<Schedule>();
            schedules = _schedulerepo.GetSchedules(d);
            if(schedules!=null)
            {
                _log4net.Info("Represntative Schedule is retrived");
                return Ok(schedules);
            }
            else
            {
                _log4net.Info("Schedule is not Created");
                return BadRequest("Schedule not Created");
            }
            
        }
    }
}
