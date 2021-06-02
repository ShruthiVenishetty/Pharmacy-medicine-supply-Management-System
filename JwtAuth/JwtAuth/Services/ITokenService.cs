﻿using JwtAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
