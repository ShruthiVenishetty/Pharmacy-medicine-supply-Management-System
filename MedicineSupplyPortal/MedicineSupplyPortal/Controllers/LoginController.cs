using MedicineSupplyPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MedicineSupplyPortal.Repository;
using System.Text;

namespace MedicineSupplyPortal.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginPortal _loginPortal;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        private string Token { get => HttpContext.Session.GetString("Token"); }
        private string Role { get => HttpContext.Session.GetString("Role"); }
        public LoginController(ILoginPortal loginPortal)
        {
            _loginPortal = loginPortal;
        }

        public IActionResult Login()
        {
            _log4net.Info("[LoginController]=>Login");

            if (Token != null && Role == "Supplier")
            {
                _log4net.Info("[LoginController]=>Login : MedicineDemand");
                return RedirectToAction("MedicineDemand","Stock");
            }
            else if(Token != null && Role == "Representative")
            {
                _log4net.Info("[LoginController]=>Login : SetSchedule");
                return RedirectToAction("SetSchedule","Schedule");
            }
            else
            {
                _log4net.Info("[LoginController]=>Login : Login(View)");
                return View("Login");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(Login cred)
        {

            _log4net.Info("[LoginController]=>Login POST : Login");

            var authResponse = await _loginPortal.AuthenticateUser(cred);

            if (authResponse != null)
            {
                HttpContext.Session.SetString("Token", authResponse.Token);
                HttpContext.Session.SetString("Role", authResponse.Role);

                if (authResponse.Role == "Supplier")
                {
                    _log4net.Info("[LoginController]=>Login POST : Role=Supplier MedicineDemand");

                    return RedirectToAction("MedicineDemand", "Stock");
                }
                else if (authResponse.Role == "Representative")
                {
                    _log4net.Info("[LoginController]=>Login POST : Role=Representative SetSchedule");

                    return RedirectToAction("SetSchedule", "Schedule");
                }
                else
                {
                    _log4net.Info("[LoginController]=>Login POST : Not a Valid Role");
                    return BadRequest();
                }
                    
            }
            else
            {
                _log4net.Info("[LoginController]=>Login POST : Incorrect Credentials");
                ViewBag.Message = "Invalid Credentials";
                return View("Login");
            }
        }


        public IActionResult LogOut()
        {
            _log4net.Info("[LoginController]=>LogOut");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
