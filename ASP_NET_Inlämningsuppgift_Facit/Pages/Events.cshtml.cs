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
    public class EventsModel : PageModel
    {
        private readonly ASP_NET_Inlämningsuppgift_Facit.Data.EventDbContext _context;

        public EventsModel(ASP_NET_Inlämningsuppgift_Facit.Data.EventDbContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Event.ToListAsync();
        }
    }
}
