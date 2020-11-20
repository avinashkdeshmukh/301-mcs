using Microsoft.IdentityModel.Tokens;
using System;

namespace MT.OnlineRestaurant.ValidateUserHandler
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public DateTime? NotBefore => DateTime.UtcNow;
        public DateTime? Expires => DateTime.UtcNow.Add(ValidFor);
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; }

    }
}
