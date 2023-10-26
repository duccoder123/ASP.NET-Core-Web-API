using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Core_Web_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
