using ASP_NET_Inlämningsuppgift_Facit.Data;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        // Ett alternativt sätt att ta in services
        public async Task OnGetAsync(bool? resetDb,
            [FromServices] EventDbContext context,
            [FromServices] UserManager<MyUser> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] SignInManager<MyUser> signInManager)
        {
            if(resetDb ?? false)
            {
                await signInManager.SignOutAsync();
                await context.ResetAndSeedAsync(userManager, roleManager);
            }
        }
    }
}
