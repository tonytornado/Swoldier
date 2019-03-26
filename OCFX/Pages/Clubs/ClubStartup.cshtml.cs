using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SocialModels;
using OCFX.DataModels;

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

        public void OnGet()
        {
            GymEquipment = _context.GymAmenities.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _manager.GetUserAsync(User);
            Profile pro = user.Profile;

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