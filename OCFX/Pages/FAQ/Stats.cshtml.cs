using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OCFX.Pages.FAQ
{
    public class StatsModel : PageModel
    {
        private readonly OCFXContext _context;

        public StatsModel(OCFXContext context)
        {
            _context = context;
        }

        public List<Profile> ProfileStats { get; private set; }
        public double AgeAverage { get; private set; }

        public void OnGet()
        {
            ProfileStats = _context.Profiles
                .Include(p => p.Photos)
                .Include(p => p.Campaign).ThenInclude(c => c.Nutrition)
                .Include(c => c.Campaign).ThenInclude(c => c.Quests)
                .Include(p => p.ClubMemberShip)
                .Include(p => p.Addresses)
                .ToList();

            AgeAverage = ProfileStats.Average(c => c.Age);
        }
    }
}