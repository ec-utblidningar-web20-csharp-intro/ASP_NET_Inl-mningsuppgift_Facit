﻿using ASP_NET_Inlämningsuppgift_Facit.Data;
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

        public IndexModel(
            ILogger<IndexModel> logger,
            EventDbContext context,
            UserManager<MyUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public UserRole Role { get; set; }
        public async Task OnGetAsync(bool? resetDb)
        {
            if(resetDb ?? false)
            {
                await _context.ResetAndSeedAsync(_userManager);
            }

            Role = await _context.Users
                .Where(u => u.Id == _userManager.GetUserId(User))
                .Select(u => u.Role)
                .FirstOrDefaultAsync();
        }
    }
}
