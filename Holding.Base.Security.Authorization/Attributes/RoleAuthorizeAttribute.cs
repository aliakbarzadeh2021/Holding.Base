using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Holding.Base.Security.Authorization.Installer;
using Holding.Base.Security.Authorization.Protectors;
using Holding.Base.Security.Authorization.Seedwork;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.OAuth;
using Holding.Base.Infrastructure.UserPermission;

namespace Holding.Base.Security.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
	    /* public int Order { get; set; }

        public string Action { get; set; }

        public string Title { get; set; }*/


		public RoleAuthorizeAttribute()
        {
	        if (string.IsNullOrEmpty(Roles))
		        Roles = "*";
		}

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
	        var token = actionContext.Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var machineKeyDataProtector = new MachineKeyDataProtector(typeof(OAuthBearerAuthenticationMiddleware).Namespace, "Access_Token", "v1");
                var secureDataFormat = new TicketDataFormat(machineKeyDataProtector);
                AuthenticationTicket ticket = secureDataFormat.Unprotect(token);

                if (ticket?.Identity == null)
                    return false;

                var claims = ticket.Identity.Claims.ToList();

                // چک کردن سطح دستری رول
                var isRoleAuthorized = IsRoleAuthorized(claims);
                
                // چک کردن سطح دسترسی کلاینت
                bool isClientAuthorized = false;
//                if (AuthorizeConfig.ClientAuthorizeEnabled)
//                    isClientAuthorized = IsClientAuthorized(claims, actionContext);

                // اگر نه سطح دسترسی رول داشت و نه سطح دسترسی کلاینت
                // ----> بازگشت false
                if (!isRoleAuthorized && !isClientAuthorized)
                    return false;

                // بررسی سطح دسترسی کاربری
                var hasPermission = HasPermission(claims, actionContext);
                if (hasPermission)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private bool IsClientAuthorized(List<Claim> claims, HttpActionContext actionContext)
        {
            //If Request Sent from WebApp Need to return true
            if (actionContext.Request.Headers.Contains("Client"))
            {
//                var allWebClients = "SamimDesktop,TeacherOffice,TeacherPanel,StudentPanel,ParentPanel,SupportPanel,ExamBank,QuestionBank,WebSiteMaker,PreRegistration,Hasht,Poll,CommunicationNetwork,Consultant,Complex";
                if (AuthorizeConfig.AllWebClients.Contains(actionContext.Request.Headers.GetValues("Client").First()))
                    return true;
            }

            return false;
        }

        private bool IsRoleAuthorized(List<Claim> claims)
        {
            Claim roles = claims.SingleOrDefault(c => c.Type.Equals("role", StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(roles?.Value))
                return false;

            var b = IsRolesAuthorized(roles.Value);
            return b;
        }

        /// <summary>
        /// چک کردن حق دسترسی به اکشن و متد مورد نظر
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool HasPermission(List<Claim> claims, HttpActionContext actionContext)
        {
            // بدست آوردن سرویس حق دسترسی
            IUserPermissionService permissionService = ResolveUserPermissionService();
            if (permissionService == null)
                return true;

            // بدست آوردن نام کنترلر
            string controllerName = GetControllerName(actionContext);
            if (string.IsNullOrEmpty(controllerName))
                return true;

            // بدست آوردن نام متد یا اکشن
            string action = GetActionName(actionContext);


            // تبدیل کلایم ها به دیکشنری
            IDictionary<string, string> claimsDic = ConvertClaimsToDic(claims);


            var hasPermission = permissionService.HasPermission(claimsDic, controllerName, action);
            return hasPermission;
        }

        private IDictionary<string, string> ConvertClaimsToDic(List<Claim> claims)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var claim in claims)
                result.Add(claim.Type, claim.Value);

            return result;
        }

        private string GetActionName(HttpActionContext actionContext)
        {
            var m = actionContext.ControllerContext?.Request?.Method?.Method;
            return m;
        }

        private string GetControllerName(HttpActionContext actionContext)
        {
            var c = actionContext.ControllerContext?.ControllerDescriptor?.ControllerName;
            
            return c;
        }

        private IUserPermissionService ResolveUserPermissionService()
        {
            if (AuthorizeConfig.Container == null)
                return null;

            try
            {
                var userPermissionService = AuthorizeConfig.Container.Resolve<IUserPermissionService>();
                return userPermissionService;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// چک کردن دسترسی برای رول مورد نظر نسبت به رولهای تعیین شده
        /// </summary>
        /// <param name="checkingRoles"></param>
        /// <returns></returns>
        private bool IsRolesAuthorized(string checkingRoles)
        {
	        if (Roles.Contains("*"))
		        return true;

            if (string.IsNullOrEmpty(checkingRoles))
                return false;

            var roles = checkingRoles.ToLower().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            bool any = roles.Any(r => Roles.ToLower().Contains(r));

            return any;
        }
    }
}