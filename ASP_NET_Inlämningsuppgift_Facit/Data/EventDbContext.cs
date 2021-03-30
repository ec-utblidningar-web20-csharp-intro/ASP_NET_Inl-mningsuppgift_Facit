using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ASP_NET_Inlämningsuppgift_Facit.Data
{
    public class EventDbContext : IdentityDbContext<MyUser>
    {
        public EventDbContext (DbContextOptions<EventDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public async Task ResetAndSeedAsync(UserManager<MyUser> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            MyUser user = new MyUser()
            {
                UserName = "test_user",
                Email = "test@hotmail.com",
            };
            await userManager.CreateAsync(user, "Passw0rd!");

            /*
            Organizer[] organizers = new Organizer[] {
                new Organizer(){
                    Name = "Funcorp",
                    Email = "info@funcorp.com",
                    PhoneNumber = "+1 203 43 234",
                },
            };
            */

            Event[] events = new Event[] { 
                new Event(){ 
                    Title="Summer camp", 
                    Description="Have a fun time chilling in the sun", 
                    Place="Colorado springs", 
                    Address="515 S Cascade Ave Colorado Springs, CO 80903", 
                    Date=DateTime.Now.AddDays(34), 
                    SpotsAvailable=234,
                    //Organizer= organizers[0],
                },
                new Event(){
                    Title="Moonhaven",
                    Description="Best lazertag in the world",
                    Place="Blackpark",
                    Address="510 N McPherson Church Rd Fayetteville, NC 28303",
                    Date=DateTime.Now.AddDays(12),
                    SpotsAvailable=23,
                    //Organizer= organizers[0],
                },
            };

            await AddRangeAsync(events);

            await SaveChangesAsync();
        }
    }
}
