using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.AccountManagement.Token
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public DateTime? NotBefore => DateTime.UtcNow;
        public DateTime? Expires => DateTime.UtcNow.Add(ValidFor);
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; }

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
