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

public class MyReq : IAuthorizationRequirement
{
}

public class MyReqHandler : AuthorizationHandler<MyReq>
{

    private EventDbContext _context;
    private UserManager<MyUser> _userManager;
    private IHttpContextAccessor _httpContextAccessor;
    public MyReqHandler(
        EventDbContext context,
        UserManager<MyUser> userManager,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyReq requirement)
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext;

        string eventId = httpContext.Request.Query["id"].ToString();

        bool eventBelongsToOrg = await _context.Events
            .Where(e => e.Id == int.Parse(eventId))
            .Select(e => e.Organizer)
            .AnyAsync(o => o.Id == _userManager.GetUserId(httpContext.User));

        if(eventBelongsToOrg) 
            context.Succeed(requirement);
        else
            context.Fail();
    }
}
