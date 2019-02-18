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

namespace OCFX.Pages.MadminAccess.Clubs
{
    public class DeleteModel : PageModel
    {
        private readonly OCFXContext _context;

        public DeleteModel(OCFXContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club = await _context.Gyms.FindAsync(id);

            if (Club != null)
            {
                _context.Gyms.Remove(Club);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
