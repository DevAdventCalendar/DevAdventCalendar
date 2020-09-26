using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DevAdventCalendarCompetition.Authorization
{
    public class CorrectGooglePermissionsHandler : AuthorizationHandler<CorrectGooglePermissions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrectGooglePermissionsHandler(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CorrectGooglePermissions requirement)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var userHasPermissions = await this._httpContextAccessor.HttpContext.GetTokenAsync("Calendar", "access_token") != null;

            if (userHasPermissions)
            {
                context.Succeed(requirement);
            }
        }
    }
}
