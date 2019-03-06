using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Profiles
{
	public class EditProfileModel : PageModel
	{
		private readonly OCFXContext _context;
		private readonly UserManager<OCFXUser> _userManager;

		public EditProfileModel(OCFXContext context, UserManager<OCFXUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

        public OCFXUser Player { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync()
		{
			Player = await _userManager.GetUserAsync(User);

			Profile = await _context.Profiles
				.Include(p => p.FitStyle).FirstOrDefaultAsync(m => m.Id == Player.ProfileId);

			if (Profile == null)
			{
				return NotFound();
			}
			ViewData["ClassId"] = new SelectList(_context.Archetypes, "Id", "Id");
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
                StatusMessage = "Error: Well, something must have happened. Let's check that information again.";
				return Page();
			}

			_context.Attach(Profile).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProfileExists(Profile.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

            StatusMessage = "Profile changed!";

			return RedirectToPage("Index");
		}

		private bool ProfileExists(int id)
		{
			return _context.Profiles.Any(e => e.Id == id);
		}
	}
}
