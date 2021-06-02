using JwtAuth.Models;
using JwtAuth.Repository;
using NUnit.Framework;
using System.Collections.Generic;

namespace JwtAuthTest
{
    [TestFixture]
    public class AuthRepoTest
    {
        User _user = new User { Username = "representative1", Password = "rep1", Role = "Representative" };
        
        [SetUp]
        public void Setup()
        {
        }


        [TestCase("representative1","rep1")]
        public void AuthenticateUser_Return_User_Test(string username,string password)
        {
            Login cred = new Login { Username = username, Password = password };

            IAuthRepo auth = new AuthRepo();

            User user = auth.AuthenticateUser(cred);

            Assert.AreEqual(_user.Username, user.Username);
            Assert.AreEqual(_user.Password, user.Password);
            Assert.AreEqual(_user.Role, user.Role);
        }

        [TestCase("InvalidUsername", "InvalidPassword")]
        public void AuthenticateUser_Returns_Null_Test(string username, string password)
        {
            Login cred = new Login { Username = username, Password = password };

            IAuthRepo auth = new AuthRepo();

            User user = auth.AuthenticateUser(cred);

            Assert.IsNull(user);
        }

    }
}