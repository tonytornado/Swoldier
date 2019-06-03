using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.DataModels;

namespace OCFX.Pages.MadminAccess
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private UserManager<OCFXUser> _userManager;

        public IndexModel(UserManager<OCFXUser> userManager)
        {
            _userManager = userManager;
        }

        public string UserName { get; private set; }

        public async void OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            UserName = _userManager.GetUserName(User);
        }
    }
}