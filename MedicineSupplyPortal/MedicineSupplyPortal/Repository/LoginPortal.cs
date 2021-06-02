using MedicineSupplyPortal.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MedicineSupplyPortal.Repository
{
    public class LoginPortal : ILoginPortal
    {
        private readonly Uri BaseAddressJWT;

        public LoginPortal(IConfiguration configuration)
        {
            BaseAddressJWT = new Uri(configuration.GetSection("ApiBaseAddresses:Jwt").Value);

        }

        public async Task<AuthResponse> AuthenticateUser(Login cred)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddressJWT;

                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await client.PostAsync("Login", content);


                if (httpResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await httpResponse.Content.ReadAsStringAsync();

                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(apiResponse);

                    return authResponse;
                }
            }

            return null;
        }

    }
}
