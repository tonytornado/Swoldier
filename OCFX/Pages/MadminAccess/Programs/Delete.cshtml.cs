using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataRepo;
using OCFX.DataModels;

namespace OCFX.Pages.MadminAccess.Programs
{
    public class DeleteModel : PageModel
    {
        private readonly OCFXContext _context;

        public DeleteModel(OCFXContext context)
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
                .Include(w => w.Workout).FirstOrDefaultAsync(m => m.WorkoutProgramId == id);

            if (WorkoutProgram == null)
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

            WorkoutProgram = await _context.WorkoutPrograms.FindAsync(id);

            if (WorkoutProgram != null)
            {
                _context.WorkoutPrograms.Remove(WorkoutProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
