using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP_NET_Inlämningsuppgift_Facit.Data;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages
{
    [Authorize(Policy = "OrganizerOwnsEvent")]
    public class EditEventModel : PageModel
    {
        private readonly EventDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public EditEventModel(
            EventDbContext context,
            UserManager<MyUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.AttendingEvents)
                .FirstOrDefaultAsync();

            if (!user.AttendingEvents.Contains(Event))
            {
                user.AttendingEvents.Add(Event);
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
