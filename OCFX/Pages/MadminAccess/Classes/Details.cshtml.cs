using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Classes
{
    public class DetailsModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailsModel(OCFXContext context)
        {
            _context = context;
        }

        public Archetype FitClass { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FitClass = await _context.Archetypes.FirstOrDefaultAsync(m => m.Id == id);

            if (FitClass == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
