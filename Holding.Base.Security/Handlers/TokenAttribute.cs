using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Holding.Base.Security.WebToken;

namespace Holding.Base.Security.Handlers
{
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _isAuth;

        public TokenAuthorizeAttribute()
        {
            if (string.IsNullOrEmpty(Roles))
            {
                Roles = "*";
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return _isAuth;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var token = actionContext.Request.Headers.Authorization;
                if (token == null)
                {
                    _isAuth = false;
                    if (IsAuthorized(actionContext))
                        return;
                    HandleUnauthorizedRequest(actionContext);
                    return;
                }
                if (Roles == "*")
                {
                    _isAuth = true;
                    if (IsAuthorized(actionContext))
                        return;
                    HandleUnauthorizedRequest(actionContext);
                    return;
                }
                var userIdentity = JsonWebToken.Parse(token.Scheme);
                List<Claim> claimList = userIdentity.Claims.ToList();
                var roleList = Roles.Split(',');
                if (roleList.Contains("*"))
                {
                    _isAuth = true;
                    if (IsAuthorized(actionContext))
                        return;
                    HandleUnauthorizedRequest(actionContext);
                    return;
                }
                if (claimList.Count == 0)
                    _isAuth = false;

                foreach (var claim in claimList)
                {
                    if (claim.Type == ClaimTypes.Actor)
                    {
                        if (roleList.Contains(claim.Value))
                        {
                            _isAuth = true;
                            break;
                        }
                    }
                }

                if (IsAuthorized(actionContext))
                    return;
                HandleUnauthorizedRequest(actionContext);

            }
            catch (Exception)
            {
                _isAuth = false;
                HandleUnauthorizedRequest(actionContext);
            }

        }

        public static string GetUserName(string token)
        {
            var userIdentity = JsonWebToken.Parse(token);
            return userIdentity.FindFirst(ClaimTypes.Name).Value;
        }

        public static List<Claim> GetUserCliams(string token)
        {
            var userIdentity = JsonWebToken.Parse(token);
            List<Claim> claimList = userIdentity.Claims.ToList();
            return claimList;
        }
    }
}