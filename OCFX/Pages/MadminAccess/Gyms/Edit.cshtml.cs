using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Clubs
{
    public class EditModel : PageModel
    {
        private readonly OCFXContext _context;

        public EditModel(OCFXContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Gym Club { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Gyms.FirstOrDefaultAsync(m => m.Id == id);

            if (Club == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FitCharacterExists(Club.Id))
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

        private bool FitCharacterExists(int id)
        {
            return _context.Gyms.Any(e => e.Id == id);
        }
    }
}
