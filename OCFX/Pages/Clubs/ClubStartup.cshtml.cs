using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Clubs
{
    [Authorize]
    public class ClubStartupModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _manager;

        public ClubStartupModel(OCFXContext context, UserManager<OCFXUser> manager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        [BindProperty]
        public Gym Gym { get; set; }
        public Session Event { get; set; }
        public List<Equipment> GymEquipment { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            GymEquipment = _context.GymAmenities.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _manager.GetUserAsync(User);
            Profile pro = _context.Profiles.Include(G => G.ClubMemberShip).SingleOrDefault(p => p.Id == user.ProfileId);

            // Check if the user is already with a gym/club
            if (pro.ClubMemberShip != null)
            {
                StatusMessage = "Error: You're already in a club, man. You gotta leave one to make one.";
                return Page();
            }

            // Add the membership to the membership table with the user as the leader.
            Membership lead = new Membership
            {
                Club = new Gym
                {
                    Description = Gym.Description,
                    Title = Gym.Title,
                    Leader = user.Profile,
                    Status = ApprovalStatus.Pending,
                    MeetingFrequency = Gym.MeetingFrequency,
                    MeetingDate = Gym.MeetingDate,
                    MeetingTime = Gym.MeetingTime
                },
                JoinDate = DateTime.Now,
                Member = user.Profile,
                Status = Membership.MembershipType.Leader,
            };

            if (Gym.Amenities != null)
            {
                foreach (var item in Gym.Amenities)
                {
                    lead.Club.Amenities.Add(item);
                }
            }

            _context.Memberships.Add(lead);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Community", new { lead.Club.Id });
        }
    }
}