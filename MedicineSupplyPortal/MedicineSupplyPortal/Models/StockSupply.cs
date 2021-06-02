using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Models
{
    
    public class StockSupply
    {
        [Key]
        public int Sno { get; set; }
        public string PharmacyName { get; set; }
        public string medicineName { get; set; }
        public int supplyCount { get; set; }

    }
}
