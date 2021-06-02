using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Models
{
    public class PharmacyMedicineSupply
    {
        public string PharmacyName { get; set; }
        public List<MedicineNameAndSupply> medicineAndSupply { get; set; }
    }
}
