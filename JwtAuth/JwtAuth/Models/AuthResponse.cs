﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
