using Microsoft.AspNetCore.Mvc;
using StockAPI.Models;
using StockAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Controllers
{
   
    public class StockController : ControllerBase
    {
       

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(StockController));
        private readonly IStockrepo _stockrepo;
       
        
        public StockController(IStockrepo stockrepo)
        {
            _stockrepo = stockrepo;
        }
            
        [HttpGet("MedicineStockInformation")]
        public IActionResult GetStock()
        {   
            List<MedicineStock> stock = _stockrepo.GetStockDetails();
            if (stock != null)
            {
                _log4net.Info("Medicine Stock Retrived");
                return Ok(stock);
            }
            else
            {
                _log4net.Info("No stock available");
                return BadRequest("No stock available");
            }
        }

        
    }
}
