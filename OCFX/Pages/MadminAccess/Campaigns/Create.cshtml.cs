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

namespace OCFX.Pages.MadminAccess.Campaigns
{
    public class CreateModel : PageModel
    {
        private readonly OCFXContext _context;

		public SelectList DietListing { get; set; }
		public SelectList ProgramListing { get; set; }

		public void PopulateProgramList(OCFXContext _context, object selectedProgram = null)
		{
			var AlistQuery = from d in _context.Workouts
							orderby d.Title
							select d;

			ProgramListing = new SelectList(AlistQuery.AsNoTracking(), "Id", "Title", selectedProgram);
		}

		public void PopulateDietList(OCFXContext _context, object selectedDiet = null)
		{
			var BlistQuery = from d in _context.Diets
							orderby d.DietName
							select d;

			DietListing = new SelectList(BlistQuery.AsNoTracking(), "Id", "DietName", selectedDiet);
		}

		public CreateModel(OCFXContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
			PopulateDietList(_context);
			PopulateProgramList(_context);
			return Page();
        }

        [BindProperty]
        public Campaign Campaign { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
				PopulateDietList(_context);
				PopulateProgramList(_context);
				return Page();
            }

            _context.Campaigns.Add(Campaign);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}