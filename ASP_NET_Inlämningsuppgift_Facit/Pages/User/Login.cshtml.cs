using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_NET_Inlämningsuppgift_Facit.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
                _userManager = userManager;
        }

        [BindProperty]
        public LoginUserForm LoginUser { get; set; }
        public class LoginUserForm
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager.PasswordSignInAsync(LoginUser.UserName, LoginUser.Password, false, false);
            //var result = await _signInManager.SignInAsync(LoginUser.UserName);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
