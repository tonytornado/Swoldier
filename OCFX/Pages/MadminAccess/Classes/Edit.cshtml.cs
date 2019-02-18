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

namespace OCFX.Pages.MadminAccess.Classes
{
    public class EditModel : PageModel
    {
        private readonly OCFXContext _context;

        public EditModel(OCFXContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FitClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FitClassExists(FitClass.Id))
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

        private bool FitClassExists(int id)
        {
            return _context.Archetypes.Any(e => e.Id == id);
        }
    }
}
