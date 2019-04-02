using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.MadminAccess.Campaigns
{
    public class EditModel : PageModel
    {
        private readonly OCFXContext _context;

        public EditModel(OCFXContext context)
        {
            _context = context;
        }

		public SelectList DietListing { get; set; }
		public SelectList ProgramListing { get; set; }

		public void PopulateProgramList(OCFXContext _context, object selectedProgram = null)
		{
			var AlistQuery = from d in _context.Workouts
							 orderby d.Title
							 select d;

			ProgramListing = new SelectList(AlistQuery.AsNoTracking(), "Id", "Title", selectedProgram);
		}

		public void PopulateDietList(OCFXContext _context, object selectedDiet = null)
		{
			var BlistQuery = from d in _context.Diets
							 orderby d.Id
							 select d;

			DietListing = new SelectList(BlistQuery.AsNoTracking(), "Id", "DietName", selectedDiet);
		}

		[BindProperty]
        public Campaign Campaign { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Campaign = await _context.Campaigns.FirstOrDefaultAsync(m => m.Id == id);
			PopulateDietList(_context, Campaign.DietId);
			PopulateProgramList(_context, Campaign.CampaignQuest);

            if (Campaign == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
				PopulateDietList(_context, Campaign.DietId);
				PopulateProgramList(_context, Campaign.CampaignQuest);
				return Page();
            }

            _context.Attach(Campaign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(Campaign.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.Id == id);
        }
    }
}
