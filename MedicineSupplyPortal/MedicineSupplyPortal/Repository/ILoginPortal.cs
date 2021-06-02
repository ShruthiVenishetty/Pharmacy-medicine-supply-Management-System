using MedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Repository
{
    public interface ILoginPortal
    {
        Task<AuthResponse> AuthenticateUser(Login cred);
    }
}
