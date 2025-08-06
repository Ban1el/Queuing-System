using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Principal;
using System.Configuration;
using QueueDashboard.Models.DTO;

namespace QueueDashboard.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["AuthToken"];
            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
                return false;

            var token = authCookie.Value;
            var user = JWTAuthentication.ValidateToken(token);
            if (user == null)
                return false;

            var identity = new GenericIdentity(user.Username);
            var principal = new GenericPrincipal(identity, user.Roles.ToArray());
            httpContext.User = principal;

            return _roles.Any(r => principal.IsInRole(r));
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { success = false, message = "Unauthorized access." },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}
