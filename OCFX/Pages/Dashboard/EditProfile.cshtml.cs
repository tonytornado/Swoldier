using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Profiles
{
    public class EditProfileModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public EditProfileModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public OCFXUser Player { get; private set; }

        [BindProperty]
        public Profile Profile { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Player = await _userManager.GetUserAsync(User);

            Profile = await _context.Profiles
                .Include(p => p.Photos)
                .Include(p => p.FitStyle)
                .FirstOrDefaultAsync(m => m.Id == Player.ProfileId);

            if (Profile == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Archetypes, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                StatusMessage = "Error: Well, something must have happened. Let's check that information again.";
                return Page();
            }

            _context.Attach(Profile).State = EntityState.Modified;

            if (Profile.Weight != Profile.Weights.OrderByDescending(c => c.Date).First().Weight)
            {
                Profile.Weights.Add(new WeightMeasurement
                {
                    Date = DateTime.Now,
                    Weight = Profile.Weight,
                    //Profile = Profile
                });

                StatusMessage = "Weight Added and ";
            }

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

            StatusMessage += "Profile changed!";

            return RedirectToPage("Index");
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }


    }
}
