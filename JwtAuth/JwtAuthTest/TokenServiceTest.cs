using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;


namespace JwtAuthTest
{
    [TestFixture]
    public class TokenServiceTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CreateToken_Returns_Token()
        {
            
            string tokenKey = "super secret Testing Key";

            var mock = new Mock<IConfiguration>();
            mock.SetupGet(p => p["TokenKey"]).Returns(tokenKey);

            ITokenService tokenService = new TokenService(mock.Object);

            User user = new User { Username = "representative1", Password = "rep1", Role = "Representative" };
            string token = tokenService.CreateToken(user);

            Assert.IsNotNull(token);
        }

        [Test]
        public void CreateToken_Returns_Null()
        {
            string tokenKey = "super secret Testing Key";

            var mock = new Mock<IConfiguration>();
            mock.SetupGet(p => p["TokenKey"]).Returns(tokenKey);

            ITokenService tokenService = new TokenService(mock.Object);

            User user = null;
            string token = tokenService.CreateToken(user);

            Assert.IsNull(token);
        }

    }
}
