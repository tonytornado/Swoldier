using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using System.Collections;
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

        public ICollection Stats { get; private set; }

        public void OnGet()
        {
            Stats = _context.Profiles
                .Include(p => p.Photos)
                .Include(p => p.Age)
                .Include(p => p.Campaign).ThenInclude(c => c.Nutrition)
                .Include(c => c.Campaign).ThenInclude(c => c.Quests)
                .Include(p => p.Gym)
                .Include(p => p.Addresses)
                .ToList();
        }
    }
}