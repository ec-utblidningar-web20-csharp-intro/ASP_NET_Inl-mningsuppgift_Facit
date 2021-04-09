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
        private readonly ILogger<IndexModel> _logger;
        private readonly EventDbContext _context;
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<MyUser> _signInManager;

        public IndexModel(
            ILogger<IndexModel> logger,
            EventDbContext context,
            UserManager<MyUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<MyUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync(bool? resetDb)
        {
            if(resetDb ?? false)
            {
                await _signInManager.SignOutAsync();
                await _context.ResetAndSeedAsync(_userManager, _roleManager);
            }
        }
    }
}
