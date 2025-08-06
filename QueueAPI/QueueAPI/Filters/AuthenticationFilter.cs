using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QueueAPI.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Bearer")
            {
                var token = authHeader.Parameter;
                var user = QueueAPI.Models.DTO.JWTAuthentication.ValidateToken(token);
                if (user != null)
                {
                    // Optionally, set the user identity for the current context
                    var identity = new System.Security.Principal.GenericIdentity(user.Username);
                    var principal = new System.Security.Principal.GenericPrincipal(identity, user.Roles.ToArray());
                    System.Threading.Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;

                    return; 
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid or missing token");
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}