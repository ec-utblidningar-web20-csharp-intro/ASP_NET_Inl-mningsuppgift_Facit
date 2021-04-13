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
        // DI (Dependency Injection) är något vi kan göra inne i denna hanteraren också!
        // Vi ber om de services vi behöver för att kolla kravet.
        private EventDbContext _context;
        private UserManager<MyUser> _userManager;
        public EventOwnershipRequirementHandler(
            EventDbContext context,
            UserManager<MyUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            EventOwnershipRequirement requirement)
        {
            // --- Här väljer vi hur kravet ska fungera
            HttpContext httpContext = context.Resource as HttpContext; // Hämta httpContext

            // Kolla om vi har ett event id i URL:en (tex "/MyEvent/4" id = 4)
            string eventId = httpContext.Request.RouteValues["id"] as string;
            // Annars om det inte fanns så kollar vi search params (tex "/MyEvent?id=4" id = 4)
            if (eventId == null || eventId == "")
                eventId = httpContext.Request.Query["id"];

            bool eventBelongsToOrg = await _context.Events // Bland alla event
                .Where(e => e.Id == int.Parse(eventId)) // Hitta det relevanta eventet
                .Select(e => e.Organizer) // Kolla organisatören för det eventet
                 // Kolla att organisatören är samma som den inloggade användaren
                .AnyAsync(o => o.Id == _userManager.GetUserId(httpContext.User));
            // ----------------------------------------

            if (eventBelongsToOrg)
                context.Succeed(requirement); // Kravet har uppfyllts
            else
                context.Fail(); // Kravet ej har uppfyllts
        }
    }
}
