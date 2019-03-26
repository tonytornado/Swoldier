using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SocialModels;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
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
        public List<GymRelation> EquipmentDetail { get; private set; }
        public GymRelation GymDetail { get; private set; }
        public List<Session> Events { get; private set; }
        public List<Membership> GymMembers { get; private set; }
        public Membership Subscription { get; private set; }
        public int MemberCount { get; private set; }
        public bool ClubAllegiance { get; private set; }
        public int MessageBoardPosts { get; private set; }
        
        [TempData]
        public string StatusMessage { get; set; }

        public async Task OnGetAsync(int id)
        {
            Visitor = await _userManager.GetUserAsync(User);

            // Get the gym
            EquipmentDetail = await _context.RelativeGyms.Include(g => g.Equipment).Where(g => g.GymId == id).ToListAsync();
            GymDetail = await _context.RelativeGyms
                .Include(g => g.Gym)
                    .ThenInclude(g => g.Meetings)
                .FirstOrDefaultAsync(g => g.GymId == id);
            GymMembers = _context.Memberships
                .Include(m => m.Member)
                    .ThenInclude(m => m.FitStyle)
                .Include(m => m.Member)
                    .ThenInclude(m => m.Photos)
                .Where(g => g.Club == GymDetail.Gym).ToList();

            // Check for subscription
            Subscription = await _context.Memberships
                .SingleOrDefaultAsync(i => i.Member.Id == Visitor.ProfileId && 
                i.Status != Membership.MembershipType.Banned &&
                i.Club.Id == id);

            // Get the count of members in the club
            MemberCount = GymDetail.Gym.Members != null ? GymDetail.Gym.Members
                .Where(u => u.Status == Membership.MembershipType.Member).Count() : 0; 

            ClubAllegiance = _context.Memberships.Where(m => m.Member.Id == Visitor.ProfileId).Count() is 0;

            // Get the message board posts
            MessageBoardPosts = _context.MessageBoardPosts.Where(c => c.Board == GymDetail.Gym).Count();

            // Grab any events/meetings
            Events = GymDetail.Gym.Meetings.ToList();

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

            StatusMessage = "Your membership is being processed. In the meantime, why don't you just browse around?";

            return RedirectToPage("Community", new { id });
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
            Membership member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);

            _context.Memberships.Remove(member);
            _context.SaveChanges();

            StatusMessage = "Member has been removed!";

            return RedirectToPage("Community", new { id });
        }

        /// <summary>
        /// Accepts a new member into the club
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="gymId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAcceptSubmissionAsync(int memberId, int gymId)
        {
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == gymId);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == memberId);

            Membership member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);
            member.Status = Membership.MembershipType.Member;
            await _context.SaveChangesAsync();

            StatusMessage = "Member has been confirmed! Better hope they aren't trouble.";

            return RedirectToPage("Community", new { gymId });
        }

        /// <summary>
        /// Denies a member entry into the club
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="gymId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDenySubmissionAsync(int memberId, int gymId)
        {
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == gymId);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == memberId);

            Membership member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);
            _context.Memberships.Remove(member);
            await _context.SaveChangesAsync();

            StatusMessage = "Membership denied! That'll show them.";

            return RedirectToPage("Community", new { gymId });
        }

        /// <summary>
        /// Banishes a member from the club into the shadow realm
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="gymId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostBanMemberAsync(int memberId, int gymId)
        {
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == gymId);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == memberId);

            Membership member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);
            member.Status = Membership.MembershipType.Banned;
            await _context.SaveChangesAsync();

            StatusMessage = "Member has been banned.";

            return RedirectToPage("Community", new { gymId });
        }

        /// <summary>
        /// Promotes a member to mentor. They cannot be made a leader.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="gymId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostPromoteMemberAsync(int memberId, int gymId)
        {
            Gym gymSub = await _context.Gyms.SingleOrDefaultAsync(i => i.Id == gymId);
            Profile pro = await _context.Profiles.SingleOrDefaultAsync(i => i.Id == memberId);

            Membership member = await _context.Memberships.SingleOrDefaultAsync(u => u.Club == gymSub && u.Member == pro);
            member.Status = Membership.MembershipType.Mentor;
            await _context.SaveChangesAsync();

            StatusMessage = "Member has been promoted!";

            return RedirectToPage("Community", new { gymId });
        }
    }
}