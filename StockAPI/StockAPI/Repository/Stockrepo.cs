using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace StockAPI.Repository
{
    public class Stockrepo : IStockrepo
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(Stockrepo));
       
        private static readonly List<MedicineStock> MedicineStockInformation = new List<MedicineStock>() {
           new MedicineStock
            {
                Name = "Dolo 650",
                ChemicalComposition = new List<string> { "Paracetamol", "Acetaminophen" },
                TargetAilment = "General",
                DateOfExpiry = DateTime.Parse("31-12-2022"),
                NumberOfTabletsInStock = 300
            },
            new MedicineStock
            {
                Name = "Orthoherb",
                ChemicalComposition = new List<string> { "Castor Plant", ",Adulsa","Neem","Guggul"},
                TargetAilment = "Orthopaedics",
                DateOfExpiry = DateTime.Parse("26-11-2021"),
                NumberOfTabletsInStock = 190
            },
            

            new MedicineStock
            {
                Name = "Cholecalciferol",
                ChemicalComposition = new List<string> { "ergocalciferol", "cholecalciferol","22-dihydroergocalciferol" },
                TargetAilment = "Orthopaedics",
                DateOfExpiry = DateTime.Parse("10-11-2023"),
                NumberOfTabletsInStock = 200
            },
           
            new MedicineStock
            {
                Name = "Gaviscon",
                ChemicalComposition = new List<string> { "Magnesium", "Oxide", "Silicon", "Sodium" },
                TargetAilment = "General",
                DateOfExpiry = DateTime.Parse("20-08-2022"),
                NumberOfTabletsInStock = 150
            },
            new MedicineStock
            {
                Name = "Hilact",
                ChemicalComposition = new List<string> { "Pancreatin", "Dimethicone","Polydimethylsiloxane" },
                TargetAilment = "Gynaecology",
                DateOfExpiry = DateTime.Parse("11-06-2024"),
                NumberOfTabletsInStock = 400
            },
           
             new MedicineStock
            {
                Name = "Cyclopam",
                ChemicalComposition = new List<string> { "Dicyclomine", "hydrochloride", "paracetamol" },
                TargetAilment = "Gynaecology",
                DateOfExpiry = DateTime.Parse("02-11-2025"),
                NumberOfTabletsInStock = 500
            }

        };

        public List<MedicineStock> GetStockDetails()
        {
            _log4net.Info("Data is being read "); 
            List<MedicineStock> stock = new List<MedicineStock>();
            try
            {
                foreach(MedicineStock ms in MedicineStockInformation)
                {
                    stock.Add(ms);
                }
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("No Values Found " + e);
                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("No Values Found" + e);
                return null;
            }
            return stock.ToList();
        }
    
    }
}
