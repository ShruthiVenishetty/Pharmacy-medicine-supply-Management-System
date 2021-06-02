using MedicineSupplyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Data
{
    public class SaveData:ISaveData
    {
        private SupplyContext context;

        public SaveData(SupplyContext _context)
        {
            context = _context;
        }
        public void AddStockData(List<StockSupply> stock)
        {
            foreach (StockSupply s in stock)
            {
                context.StockSupplies.Add(s);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
