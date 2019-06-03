using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Programs
{
    public class DetailsModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailsModel(OCFXContext context)
        {
            _context = context;
        }

        public WorkoutProgram WorkoutProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkoutProgram = await _context.WorkoutPrograms
                .Include(w => w.Exercise)
                .Include(w => w.Workout).FirstOrDefaultAsync(m => m.WorkoutProgramId == id);

            if (WorkoutProgram == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
