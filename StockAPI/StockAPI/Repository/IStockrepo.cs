using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Repository
{
    public interface IStockrepo
    {
        public List<MedicineStock> GetStockDetails();
    }
}
