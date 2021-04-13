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
    public class JoinEventModel : PageModel
    {
        private readonly EventDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public JoinEventModel(
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
        // Vad är id:t på eventet vi ska gå med i?
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Vilket event ska vi gå med i?
            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            // Vad är user id på den som ska gå med?
            var userId = _userManager.GetUserId(User);
            // Vem är det som ska gå med?
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.AttendingEvents)
                .FirstOrDefaultAsync();

            if (!user.AttendingEvents.Contains(Event))
            {
                // Om vi har en användare och ett event så är det
                // enkelt att lägga till eventet till användarens
                // lista på planerade events.
                user.AttendingEvents.Add(Event);
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
