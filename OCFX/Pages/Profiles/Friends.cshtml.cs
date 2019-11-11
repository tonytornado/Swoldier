using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCFX.Pages.Profiles
{
    [Authorize]
    public class FriendsModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public FriendsModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ProfileSheet Profiler { get; private set; }
        public List<FriendSheet> Friends { get; private set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Profiler = await _context.Profiles.FirstOrDefaultAsync(m => m.Id == user.ProfileId);
            Friends = await FriendlyMethods.GetFriendRequestsAsync(_context, Profiler.Id);
        }

        /// <summary>
        /// Adds a friend.
        /// </summary>
        /// <param name="user">User Id</param>
        /// <param name="friend">Friend's Id</param>
        /// <returns></returns>
        public IActionResult OnGetAddFriend(int user, int friend)
        {
            FriendlyMethods.AcceptFriend(_context, user, friend);
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetRemoveFriend(int user, int friend)
        {
            FriendlyMethods.RemoveFriend(_context, user, friend);
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetBlockFriend(int user, int friend)
        {
            FriendlyMethods.BlockFriend(_context, user, friend);
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetUnblockFriend(int user, int friend)
        {
            FriendlyMethods.RemoveFriend(_context, user, friend);
            return RedirectToPage("/Index");
        }
    }
}