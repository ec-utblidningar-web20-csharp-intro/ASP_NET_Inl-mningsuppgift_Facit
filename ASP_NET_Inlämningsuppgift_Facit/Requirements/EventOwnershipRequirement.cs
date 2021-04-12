using ASP_NET_Inlämningsuppgift_Facit.Data;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Requirements
{
    public class EventOwnershipRequirement : IAuthorizationRequirement
    {
    }

    public class EventOwnershipRequirementHandler : AuthorizationHandler<EventOwnershipRequirement>
    {

        private EventDbContext _context;
        private UserManager<MyUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        public EventOwnershipRequirementHandler(
            EventDbContext context,
            UserManager<MyUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EventOwnershipRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            string eventId = httpContext.Request.RouteValues["id"] as string;
            if (eventId == null || eventId == "")
                eventId = httpContext.Request.Query["id"];

            bool eventBelongsToOrg = await _context.Events
                .Where(e => e.Id == int.Parse(eventId))
                .Select(e => e.Organizer)
                .AnyAsync(o => o.Id == _userManager.GetUserId(httpContext.User));

            if (eventBelongsToOrg)
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}
