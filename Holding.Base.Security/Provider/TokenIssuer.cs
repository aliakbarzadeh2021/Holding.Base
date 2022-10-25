using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using Holding.Base.Security.Audiences;
using Holding.Base.Security.WebToken;
using Microsoft.Owin.Security;

namespace Holding.Base.Security.Provider
{
    public class TokenIssuer
    {
        public static string Provide(ClaimsIdentity identity)
        {
            if (identity == null)
                throw new SecurityException("Claims cannot be null...");

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "audience", TrustedAudience.Identifier
                }
            })
            {
                AllowRefresh = true,
                IssuedUtc = new DateTimeOffset(DateTime.Now.ToUniversalTime()),
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMinutes(30).ToUniversalTime())
            };

            var ticket = new AuthenticationTicket(identity, props);
            var token = new JwtFormat(TrustedAudience.Name).Protect(ticket);
            return token;
        }
    }
}