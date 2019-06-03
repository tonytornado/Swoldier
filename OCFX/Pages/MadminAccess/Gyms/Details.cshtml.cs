using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Clubs
{
    public class DetailsModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailsModel(OCFXContext context)
        {
            _context = context;
        }

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
    }
}
