using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace QueueDashboard.Filters
{
    public class AuthorizationFilter: ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var authCookie = request.Cookies["AuthToken"];

            if (authCookie == null || string.IsNullOrEmpty(authCookie.Value))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "controller", "Login" },
                            { "action", "Index" }
                    });
            }
        }
    }
}