using JwtAuth.Models;
using JwtAuth.Repository;
using JwtAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JwtAuth.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAuthRepo _authRepo;
        private ITokenService _tokenService;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountController));

        public AccountController(IAuthRepo authRepo,ITokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public IActionResult Login(Login cred)
        {
            _log4net.Info("[AccountController]=>Login");

            var user = _authRepo.AuthenticateUser(cred);

            if (user == null)
            {
                _log4net.Info("[AccountController]=>Login : User Not Found");
                return Unauthorized();
            }
            else
            {
                string token = _tokenService.CreateToken(user);

                _log4net.Info("[AccountController]=>Login : Token Created");

                return Ok(new AuthResponse{ Token=token, Role=user.Role});
            }
        }

    }
}
