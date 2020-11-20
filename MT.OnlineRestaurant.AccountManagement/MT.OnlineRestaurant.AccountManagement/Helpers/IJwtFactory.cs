using System.Security.Claims;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.AccountManagement.Helpers
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GetClaimsIdentity(string userName, string id = null);
    }
}