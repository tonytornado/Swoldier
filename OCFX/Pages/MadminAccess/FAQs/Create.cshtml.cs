using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SiteModels;
using OCFX.Data.DataRepo;

namespace OCFX.Pages.MadminAccess.FAQs
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
        public Facts Facts { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FAQs.Add(Facts);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}