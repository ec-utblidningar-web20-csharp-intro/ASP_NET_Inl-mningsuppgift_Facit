using ASP_NET_Inlämningsuppgift_Facit.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EventDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(
            ILogger<IndexModel> logger,
            EventDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync(bool? resetDb)
        {
            if(resetDb ?? false)
            {
                await _context.ResetAndSeedAsync(_userManager);
            }
        }
    }
}
