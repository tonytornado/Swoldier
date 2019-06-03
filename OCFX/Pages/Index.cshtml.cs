using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.DataModels;
using System;

namespace OCFX.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<OCFXUser> _signInManager;

        public IndexModel(SignInManager<OCFXUser> signInManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public IActionResult OnGet()
        {
            bool userCheck = _signInManager.IsSignedIn(User);

            if (userCheck != false)
            {
                if (User.IsInRole("Administrator"))
                {
                    return RedirectToPage("/MadminAccess/Index");
                }
                if (User != null)
                {
                    return RedirectToPage("/Dashboard/Index");
                }
            }

            return Page();
        }
    }
}
