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

namespace OCFX.Pages.MadminAccess.Workouts
{
    public class DeleteModel : PageModel
    {
        private readonly OCFXContext _context;

        public DeleteModel(OCFXContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Workout Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Workout = await _context.Workouts.FirstOrDefaultAsync(m => m.Id == id);

            if (Workout == null)
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

            Workout = await _context.Workouts.FindAsync(id);

            if (Workout != null)
            {
                _context.Workouts.Remove(Workout);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
