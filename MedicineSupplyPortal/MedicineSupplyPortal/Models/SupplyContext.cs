using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Models
{
    public class SupplyContext:DbContext
    {

        public SupplyContext(DbContextOptions<SupplyContext> options):base(options)
        {

        }
        public DbSet<StockSupply> StockSupplies { get; set; }
       
    }
}
