using Microsoft.AspNetCore.Identity;

namespace nz_walks.Repositories;

public interface ITokenRepository
{
    string CreatJwtToken(IdentityUser user, List<string> roles); 
}