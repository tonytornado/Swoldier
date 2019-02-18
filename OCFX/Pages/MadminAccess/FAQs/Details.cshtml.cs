﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SiteModels;
using OCFX.Data.DataRepo;

namespace OCFX.Pages.MadminAccess.FAQs
{
	public class DetailsModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailsModel(OCFXContext context)
        {
            _context = context;
        }

        public Facts Facts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Facts = await _context.FAQs.FirstOrDefaultAsync(m => m.Id == id);

            if (Facts == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
