﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class MedicineStock
    {
       
        public string Name { get; set; }
       public List<string> ChemicalComposition  { get; set; }
        public string TargetAilment { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int NumberOfTabletsInStock { get; set; }

    }
}
