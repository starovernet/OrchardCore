using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OrchardCore.Admin
{
    /// <summary>
    /// Intercepts any request to check whether it applies to the admin site.
    /// If so it marks the request as such and ensures the user as the right to access it.
    /// </summary>
    public class AdminFilter : ActionFilterAttribute, IAsyncPageFilter
    {
        private readonly IAuthorizationService _authorizationService;

        public AdminFilter(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (AdminAttribute.IsApplied(context.HttpContext))
            {
                var authorized = await _authorizationService.AuthorizeAsync(context.HttpContext.User, Permissions.AccessAdminPanel);

                if (!authorized)
                {
                    context.Result = new ChallengeResult();
                    return;
                }
            }

            await next();
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (AdminAttribute.IsApplied(context.HttpContext))
            {
                var authorized = await _authorizationService.AuthorizeAsync(context.HttpContext.User, Permissions.AccessAdminPanel);

                if (!authorized)
                {
                    context.Result = new ChallengeResult();
                    return;
                }
            }

            await next();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) => Task.CompletedTask;
    }
}
