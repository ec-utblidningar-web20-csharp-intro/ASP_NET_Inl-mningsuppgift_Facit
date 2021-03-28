using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Inlämningsuppgift_Facit.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages.User
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public NewUserForm NewUser { get; set; }
        public class NewUserForm
        {
            public string userName { get; set; }
            public string password { get; set; }
        }
        public async Task<IActionResult> OnPost()
        {
            IdentityUser newUser = new IdentityUser() { 
                UserName = NewUser.userName,
            };
            
            var result = await _userManager.CreateAsync(newUser, NewUser.password);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
