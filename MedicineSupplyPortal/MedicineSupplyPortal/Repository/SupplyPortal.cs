using MedicineSupplyPortal.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MedicineSupplyPortal.Repository
{
    public class SupplyPortal  : ISupplyPortal
    {
        private readonly Uri BaseAddressSupplies;
        private readonly IHttpContextAccessor _httpContext;

        private string Token { get => _httpContext.HttpContext.Session.GetString("Token"); }
        public SupplyPortal(IConfiguration configuration,IHttpContextAccessor httpContext)
        {
            BaseAddressSupplies = new Uri(configuration.GetSection("ApiBaseAddresses:Supplies").Value);
            _httpContext = httpContext;
        }

        
        private async Task<List<MedicineStock>> GetMedicineStocks()
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddressSupplies;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);

                HttpResponseMessage res = await client.GetAsync("Stock");

                if (res.IsSuccessStatusCode)
                {
                    var apiResponse = res.Content.ReadAsStringAsync().Result;
                    List<MedicineStock> medicineStock = JsonConvert.DeserializeObject<List<MedicineStock>>(apiResponse);
                    return medicineStock;
                }
                else
                {
                    return null;
                }
            }
        }


        public async Task<List<MedicineDemand>> GetMedicineDemand()
        {
            var medicineStocks = await GetMedicineStocks();

            List<MedicineDemand> medicineDemand = new List<MedicineDemand>();

            if (medicineStocks != null)
            {
                foreach (var item in medicineStocks)
                {
                    medicineDemand.Add(new MedicineDemand { MedicineName = item.Name, DemandCount = 0 });
                }

                return medicineDemand;
            }
            else
            {
                return null;
            }
        }


        public async Task<List<PharmacyMedicineSupply>> GetSupplies(List<MedicineDemand> medicineDemand)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddressSupplies;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);

                StringContent content = new StringContent(JsonConvert.SerializeObject(medicineDemand), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync("PharmacySupply", content);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await httpResponse.Content.ReadAsStringAsync();
                    List<PharmacyMedicineSupply> supplies = JsonConvert.DeserializeObject<List<PharmacyMedicineSupply>>(apiResponse);

                    return supplies;
                }
            }

            return null;
        }


    }
}
