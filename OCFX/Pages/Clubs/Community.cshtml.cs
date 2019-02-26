using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Clubs
{
    public class CommunityModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public CommunityModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public OCFXUser Visitor { get; private set; }
        public Task<Gym> gymSub { get; private set; }

        public Gym CommunityDetail { get; private set; }
        public int MemberCount { get; private set; }
        public bool Subscription { get; private set; }

        public async Task OnGetAsync(int id)
        {
            Visitor = await _userManager.GetUserAsync(User);

            // Get the gym
            CommunityDetail = await _context.Gyms
                .Include(m => m.Members)
                    .ThenInclude(m => m.Member)
                    .ThenInclude(m => m.FitStyle)
                .Include(m => m.Members)
                    .ThenInclude(m => m.Member)
                    .ThenInclude(m => m.Photos)
                .Include(m => m.Amenities)
                .SingleOrDefaultAsync(i => i.Id == id);

            // Get the count of members in the club
            MemberCount = CommunityDetail.Members.Count;

            // Check for subscription
            //Subscription = CommunityDetail.Members.Contains();
        }

        /// <summary>
        /// Sign up for a club
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostClubSignUpAsync(int id)
        {
            // Set up variables for visitor and gym.
            Visitor = await _userManager.GetUserAsync(User);
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == id);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == Visitor.ProfileId);

            // Set the current gym using EF
            pro.Gym = gymSub;
            _context.SaveChanges();

            return RedirectToPage(pageName: "Community", pageHandler: "OnGetAsync", routeValues: new { id });
        }

        /// <summary>
        /// Leave the club
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostClubRemoveAsync(int id)
        {
            // Set up variables for visitor and gym.
            Visitor = await _userManager.GetUserAsync(User);
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == id);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == Visitor.ProfileId);

            // Set current gym to NULL
            gymSub.Patrons.Remove(pro);
            _context.SaveChanges();

            return RedirectToPage(pageName: "Community", pageHandler: "OnGetAsync", routeValues: new { id });
        }
    }
}