using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class RoleAuthorizeAttribute : AuthorizeAttribute
{
    private readonly string[] _allowedRoles;

    public RoleAuthorizeAttribute(params string[] roles)
    {
        _allowedRoles = roles;
    }

    protected override bool IsAuthorized(HttpActionContext actionContext)
    {
        var user = actionContext.ControllerContext.RequestContext.Principal;

        if (user == null || !user.Identity.IsAuthenticated)
            return false;

        return _allowedRoles.Any(user.IsInRole);
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Access denied. Required role(s): " + string.Join(", ", _allowedRoles));
    }
}
