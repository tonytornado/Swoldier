using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCFX.Pages.Campaigns
{
    public class DetailsModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailsModel(OCFXContext context)
        {
            _context = context;
        }

        public Campaign Campaign { get; private set; }
        public List<Quest> QuestList { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campaign = await _context.Campaigns
                .Include(c => c.Nutrition)
                .Include(c => c.Quests)
                .FirstOrDefaultAsync(m => m.Id == id);

            QuestList = Campaign.Quests;

            if (Campaign == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
