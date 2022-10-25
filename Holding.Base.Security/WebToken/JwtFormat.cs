using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;
using Holding.Base.Security.Audiences;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace Holding.Base.Security.WebToken
{
    internal class JwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "audience";
        private readonly string _issuer;

        public JwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;

            if (audienceId != TrustedAudience.Identifier) throw new SecurityTokenInvalidAudienceException("AuthenticationTicket.Properties does not include audience");

            var keyByteArray = TextEncodings.Base64Url.Decode(TrustedAudience.SymmetricKey);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            if (!issued.HasValue) throw new SecurityTokenInvalidIssuerException();
            if (!expires.HasValue) throw new SecurityTokenNoExpirationException();

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler { TokenLifetimeInMinutes = 30 };
            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var keyByteArray = TextEncodings.Base64Url.Decode(TrustedAudience.SymmetricKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = TrustedAudience.Name,
                ValidateIssuer = true,
                IssuerSigningToken = new BinarySecretSecurityToken(TrustedAudience.SymmetricKey, keyByteArray)
            };

            var claimPrincipal = handler.ValidateToken(protectedText, validationParameters, out securityToken);
            if (claimPrincipal == null)
                throw new SecurityTokenValidationException("Token is invalid...");
            return new AuthenticationTicket(new ClaimsIdentity(claimPrincipal.Claims), new AuthenticationProperties());
        }
    }
}