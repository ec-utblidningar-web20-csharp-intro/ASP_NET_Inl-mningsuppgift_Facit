using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Inlämningsuppgift_Facit.Data;
using ASP_NET_Inlämningsuppgift_Facit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages.Organizer
{
    public class LoungeModel : PageModel
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly EventDbContext _context;
        public LoungeModel(
            UserManager<MyUser> userManager,
            EventDbContext context
            )
        {
            _userManager = userManager;
            _context = context;
        }

        public List<MyUser> Users;
        public async Task OnGet()
        {
            /* 
             * Ett hemskt dåligt sätt att bara hämta de användare som
             * tillhör rollen Organizer från databasen.
             * 
             * Ett enkelt sätt är att skapa en ny Role klass som ärver av IdentityRole och
             * har en referens till MyUsers, så att vi kan använda enkel EFCore syntax för
             * att hämta alla users för en roll. (Samma som MyUser : IdentityUser)
             * 
             * Ett annat sätt är att skriva en ordentlig query till databasen
             * som joinar User, UserRole och Role tabellerna.
             */
            var users = await _userManager.Users.ToListAsync();

            Users = users;
        }

        [BindProperty]
        public bool CheckBox { get; set; }
        public async Task OnPost(string? id)
        {
            if (!CheckBox)
                await _userManager.RemoveFromRoleAsync(await _userManager.FindByIdAsync(id), "Organizer");
            else
                await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(id), "Organizer");

            await OnGet();
        }
    }
}
