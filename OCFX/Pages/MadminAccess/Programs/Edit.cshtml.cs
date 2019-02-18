using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataRepo;
using OCFX.DataModels;

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
				.FirstOrDefaultAsync(m => m.WorkoutProgramId == id);

            if (WorkoutProgram == null)
            {
                return NotFound();
            }
           ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExName");
           ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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
