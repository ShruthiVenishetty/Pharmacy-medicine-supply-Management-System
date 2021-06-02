using MedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Data
{
    public interface ISaveData
    {
        public void AddStockData(List<StockSupply> data);

        public void Save();
        
    }
}
