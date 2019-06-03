using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Diets
{
    public class DeleteModel : PageModel
    {
        private readonly OCFXContext _context;

        public DeleteModel(OCFXContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Diet FitDiet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FitDiet = await _context.Diets.FirstOrDefaultAsync(m => m.Id == id);

            if (FitDiet == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FitDiet = await _context.Diets.FindAsync(id);

            if (FitDiet != null)
            {
                _context.Diets.Remove(FitDiet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
