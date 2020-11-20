using MT.OnlineRestaurant.AccountManagement.Token;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.AccountManagement.Helpers
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtOptions _option;

        public JwtFactory(JwtOptions option)
        {
            this._option = option;
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            // claims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _option.JtiGenerator()));
            claims.Add(identity.FindFirst(Constants.JwtClaimIdentifiers.Id));
            claims.Add(identity.FindFirst(Constants.JwtClaimIdentifiers.Rol));


            var token = new JwtSecurityToken(
                issuer: _option.Issuer,
                audience: _option.Audience,
                claims: claims,
                notBefore: _option.NotBefore,
                expires: _option.Expires,
                signingCredentials: _option.SigningCredentials
                );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }

        public ClaimsIdentity GetClaimsIdentity(string userName, string id = null)
        {
            List<Claim> claims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(id))
            {
                claims.Add(new Claim(Constants.JwtClaimIdentifiers.Id, id));
            }
            claims.Add(new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));

            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), claims);

        }
    }
}
