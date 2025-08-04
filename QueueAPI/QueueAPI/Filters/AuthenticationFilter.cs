using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QueueAPI.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Bearer")
            {
                var token = authHeader.Parameter;
                var user = QueueAPI.Models.DTO.JWTAuthentication.ValidateToken(token);
                if (user != null)
                {
                    // Optionally, set the user identity for the current context
                    var identity = new System.Security.Principal.GenericIdentity(user);
                    var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                    System.Threading.Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;

                    return; 
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid or missing token");
        }
    }
}