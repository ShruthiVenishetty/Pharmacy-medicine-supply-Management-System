using JwtAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Repository
{
    public interface IAuthRepo
    {
        public User AuthenticateUser(Login cred);
    }
}
