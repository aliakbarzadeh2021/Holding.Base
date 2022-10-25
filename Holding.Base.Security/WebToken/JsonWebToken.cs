using System.Security.Claims;
using Holding.Base.Security.Audiences;

namespace Holding.Base.Security.WebToken
{
    public class JsonWebToken
    {
        public static ClaimsIdentity Parse(string jwt)
        {
            var ticket = new JwtFormat(TrustedAudience.Name).Unprotect(jwt);
            return ticket.Identity;
        }
    }
}