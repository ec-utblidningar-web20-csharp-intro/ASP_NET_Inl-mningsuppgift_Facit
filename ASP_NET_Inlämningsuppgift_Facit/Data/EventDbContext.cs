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

        public async Task ResetAndSeedAsync(
            UserManager<MyUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            await roleManager.CreateAsync(new IdentityRole("Attendee"));
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Organizer"));

            MyUser admin = new MyUser()
            {
                UserName = "admin",
                Email = "admin@hotmail.com",
            };
            await userManager.CreateAsync(admin, "Passw0rd!");
            await userManager.AddToRoleAsync(admin, "Admin");

            MyUser user = new MyUser()
            {
                UserName = "test_user",
                Email = "test@hotmail.com",
            };
            await userManager.CreateAsync(user, "Passw0rd!");

            MyUser[] organizers = new MyUser[] {
                new MyUser(){
                    UserName = "Funcorp",
                    Email = "info@funcorp.com",
                    PhoneNumber = "+1 203 43 234",
                },
                new MyUser(){
                    UserName = "Funcorp1",
                    Email = "info1@funcorp.com",
                    PhoneNumber = "+1 203 43 234",
                },
                new MyUser(){
                    UserName = "Funcorp2",
                    Email = "info2@funcorp.com",
                    PhoneNumber = "+1 203 43 234",
                },
            };
            foreach(var org in organizers)
            {
                await userManager.CreateAsync(org, "Passw0rd!");
                await userManager.AddToRoleAsync(org, "Organizer");
            }

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

            await AddRangeAsync(events);

            await SaveChangesAsync();
        }
    }
}
