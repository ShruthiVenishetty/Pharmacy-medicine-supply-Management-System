using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
