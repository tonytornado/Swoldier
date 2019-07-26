using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Programs
{
    public class EditModel : PageModel
    {
        private readonly OCFXContext _context;

        public EditModel(OCFXContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkoutProgram WorkoutProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkoutProgram = await _context.WorkoutPrograms
                .Include(w => w.Exercise)
                .Include(w => w.Workout)
                //.Include(w => w.Campaign)
                .FirstOrDefaultAsync(m => m.WorkoutProgramId == id);

            if (WorkoutProgram == null)
            {
                return NotFound();
            }

            ViewData["ExerciseId"] = new SelectList(_context.Exercises.ToList(), "Id", "Name");
            ViewData["WorkoutId"] = new SelectList(_context.Workouts.ToList(), "Id", "Title");
            ViewData["CampaignId"] = new SelectList(_context.Campaigns.ToList(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ExerciseId"] = new SelectList(_context.Exercises.ToList(), "Id", "Name");
                ViewData["WorkoutId"] = new SelectList(_context.Workouts.ToList(), "Id", "Title");
                ViewData["CampaignId"] = new SelectList(_context.Campaigns.ToList(), "Id", "Name");
                return Page();
            }

            _context.Attach(WorkoutProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutProgramExists(WorkoutProgram.WorkoutProgramId))
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

        private bool WorkoutProgramExists(int id)
        {
            return _context.WorkoutPrograms.Any(e => e.WorkoutProgramId == id);
        }
    }
}
