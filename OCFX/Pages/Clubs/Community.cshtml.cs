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
        public Task<Gym> GymSub { get; private set; }

        public Gym CommunityDetail { get; private set; }
        public int MemberCount { get; private set; }
        public Membership Subscription { get; private set; }

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

            // Check for subscription
            Subscription = await _context.Memberships
                .SingleOrDefaultAsync(i => i.Member.Id == Visitor.ProfileId && 
                i.Status != Membership.MembershipType.Banned &&
                i.Club.Id == id);

            // Get the count of members in the club
            MemberCount = CommunityDetail.Members
                .Where(u => u.Status == Membership.MembershipType.Member).Count();
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
            var member = new Membership()
            {
                Club = gymSub,
                Member = pro,
                JoinDate = DateTime.Now,
                Status = Membership.MembershipType.Pending
            };

            _context.Memberships.Add(member);
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
            var member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);

            _context.Memberships.Remove(member);
            _context.SaveChanges();

            return RedirectToPage(pageName: "Community", pageHandler: "OnGetAsync", routeValues: new { id });
        }
    }
}