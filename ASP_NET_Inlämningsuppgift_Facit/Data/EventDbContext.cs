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

        public DbSet<Event> Events { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        public void Seed()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            Attendee[] attendees = new Attendee[] { 
                new Attendee()
                {
                    Name = "Ian Holmes",
                    PhoneNumber = "+1 203 42 564",
                    Email = "ian.holmes@gmail.com",
                }, 
            };

            Organizer[] organizers = new Organizer[] {
                new Organizer(){
                    Name = "Funcorp",
                    Email = "info@funcorp.com",
                    PhoneNumber = "+1 203 43 234",
                },
            };

            Event[] events = new Event[] { 
                new Event(){ 
                    Title="Summer camp", 
                    Description="Have a fun time chilling in the sun", 
                    Place="Colorado springs", 
                    Address="515 S Cascade Ave Colorado Springs, CO 80903", 
                    Date=DateTime.Now.AddDays(34), 
                    SpotsAvailable=234,
                    Organizer= organizers[0],
                },
                new Event(){
                    Title="Moonhaven",
                    Description="Best lazertag in the world",
                    Place="Blackpark",
                    Address="510 N McPherson Church Rd Fayetteville, NC 28303",
                    Date=DateTime.Now.AddDays(12),
                    SpotsAvailable=23,
                    Organizer= organizers[0],
                },
            };

            AddRange(attendees);
            AddRange(organizers);
            AddRange(events);

            SaveChanges();
        }
    }
}
