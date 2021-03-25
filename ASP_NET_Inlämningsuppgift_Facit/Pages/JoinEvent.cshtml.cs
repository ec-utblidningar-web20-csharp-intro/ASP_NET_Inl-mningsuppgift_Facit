using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP_NET_Inlämningsuppgift_Facit.Data;
using ASP_NET_Inlämningsuppgift_Facit.Models;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages
{
    public class JoinEventModel : PageModel
    {
        private readonly ASP_NET_Inlämningsuppgift_Facit.Data.EventDbContext _context;

        public JoinEventModel(ASP_NET_Inlämningsuppgift_Facit.Data.EventDbContext context)
        {
            _context = context;
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
    }
}
