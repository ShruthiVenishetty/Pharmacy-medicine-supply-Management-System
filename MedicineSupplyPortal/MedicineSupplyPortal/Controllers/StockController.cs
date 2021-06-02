using MedicineSupplyPortal.Data;
using MedicineSupplyPortal.Models;
using MedicineSupplyPortal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Controllers
{
    public class StockController : Controller
    {

        //Dependency Injection
        private ISupplyPortal _supplyPortal;
        private ISaveData _saveData;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(StockController));

        private string Token { get => HttpContext.Session.GetString("Token"); }
        private string Role { get => HttpContext.Session.GetString("Role"); }

        public StockController(ISupplyPortal supplyPortal,ISaveData saveData)
        {
            _supplyPortal = supplyPortal;
            _saveData = saveData;
        }
        
        
        

        public async Task<IActionResult> MedicineDemand()
        {
            _log4net.Info("[StockController]=>MedicineDemand");

            if (Token == null)
            {
                _log4net.Info("[StockController]=>MedicineDemand : Token is NULL");
                return RedirectToAction("Login", "Login");
            }
                

            if(Role == "Supplier")
            {
                _log4net.Info("[StockController]=>MedicineDemand : Role is Supplier");

                var medicineDemand = await _supplyPortal.GetMedicineDemand();

                if (medicineDemand == null)
                {
                    _log4net.Info("[StockController]=>MedicineDemand : Medicine Demand is NULL");
                    return BadRequest();
                }

                _log4net.Info("[StockController]=>MedicineDemand : MedicineDemand(View)");
                return View("MedicineDemand",medicineDemand);
            }
            else
            {
                _log4net.Info("[StockController]=>MedicineDemand : Unauthorized");
                return View("Unauthorized");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> PharmacySupplies(List<MedicineDemand> medicineDemand)
        {
            _log4net.Info("[StockController]=>PharmacySupplies");

            if (Token == null)
            {
                _log4net.Info("[StockController]=>PharmacySupplies : Token is NULL");
                return RedirectToAction("Login", "Login");
            }
                

            if (Role == "Supplier")
            {
                _log4net.Info("[StockController]=>PharmacySupplies : Role is Supplier");

                if (medicineDemand.Count != 0 && medicineDemand != null)
                {
                    List<PharmacyMedicineSupply> supplies = await _supplyPortal.GetSupplies(medicineDemand);

                    if (supplies.Count != 0 && supplies != null)
                    {
                       
                        List<StockSupply> li = new List<StockSupply>();
                        foreach(PharmacyMedicineSupply pms in supplies)
                        {    
                           foreach(MedicineNameAndSupply x in pms.medicineAndSupply)
                            {
                                StockSupply s = new StockSupply();
                                s.PharmacyName = pms.PharmacyName;
                                s.medicineName = x.medicineName;
                                s.supplyCount = x.supplyCount;
                                li.Add(s);
                            }
                           
                        } 
                        _saveData.AddStockData(li);
                        _saveData.Save();

                        _log4net.Info("[StockController]=>PharmacySupplies : PharmacySupplies(View)");

                        return View("PharmacySupplies", supplies);

                    }

                    else
                    {
                        _log4net.Info("[StockController]=>PharmacySupplies : Supplies List empty or NULL");
                        return BadRequest();
                    }
                        
                }
                else
                {
                    _log4net.Info("[StockController]=>PharmacySupplies : MedicineDemand List empty or NULL");
                    return BadRequest();
                }
                    
            }
            else
            {
                _log4net.Info("[StockController]=>PharmacySupplies : Unauthorized");
                return View("Unauthorized");
            }

        }

    }
}
