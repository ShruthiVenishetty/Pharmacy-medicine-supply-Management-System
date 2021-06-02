using MedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Repository
{
    public interface ISupplyPortal
    {
        Task<List<MedicineDemand>> GetMedicineDemand();

        Task<List<PharmacyMedicineSupply>> GetSupplies(List<MedicineDemand> medicineDemand);
    }
}
