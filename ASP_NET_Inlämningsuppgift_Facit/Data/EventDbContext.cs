using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_NET_Inlämningsuppgift_Facit.Models;

namespace ASP_NET_Inlämningsuppgift_Facit.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext (DbContextOptions<EventDbContext> options)
            : base(options)
        {
        }

        public DbSet<ASP_NET_Inlämningsuppgift_Facit.Models.Event> Event { get; set; }
    }
}
