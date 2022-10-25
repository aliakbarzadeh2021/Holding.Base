using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Holding.Base.Security.Handlers
{
    public class DefaultAuthHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("Token"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
          
            request.GetOwinContext().Authentication.User = new ClaimsPrincipal(new ClaimsIdentity(new[] 
            {
                new Claim(ClaimTypes.Actor , "Masoud"),
                new Claim(ClaimTypes.Name , "Masoud1"),
            }));

            return base.SendAsync(request, cancellationToken);
        }
    }
}
