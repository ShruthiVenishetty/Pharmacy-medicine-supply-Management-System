using JwtAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private List<User> users = new List<User>()
        {
            new User{ Username = "representative1", Password = "rep1", Role="Representative" },
            new User{ Username = "representative2", Password = "rep2", Role="Representative" },
            new User{ Username = "supplier1", Password = "supp1", Role="Supplier" },
            new User{ Username = "supplier1", Password = "supp2", Role="Supplier" }
        };

        public User AuthenticateUser(Login cred)
        {
            if(cred == null)
            {
                return null;
            }
            else
            {
                var user = users.FirstOrDefault(x => x.Username == cred.Username && x.Password == cred.Password);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
                
            }
            
        }


    }
}
