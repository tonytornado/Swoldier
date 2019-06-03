using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Classes
{
    public class CreateModel : PageModel
    {
        private readonly OCFXContext _context;

        public CreateModel(OCFXContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Archetype FitClass { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Archetypes.Add(FitClass);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}