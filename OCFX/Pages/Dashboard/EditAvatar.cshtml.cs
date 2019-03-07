using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Profiles
{
    public class EditAvatarModel : PageModel
    {
        private readonly OCFXContext _context;
        private IHostingEnvironment _environment;
        private string fileName;
        private readonly UserManager<OCFXUser> _userManager;

        public EditAvatarModel(OCFXContext context, IHostingEnvironment environment, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public OCFXUser Player { get; private set; }

        [BindProperty]
        public Photo Photo { get; set; }
        public IFormFile Image { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Player = await _userManager.GetUserAsync(User);

            Photo = await _context.Photos
                .Where(p => p.Type == Photo.PhotoType.Profile)
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId);

            if (Photo == null)
            {
                StatusMessage = "Let's try on a new face! Enter a litle bit of information so we can get rid of that Beerus photo. It looks weird, okay?";
                Photo.ProfileId = Player.ProfileId;
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                StatusMessage = "Error: Something's wrong! We can't change your picture!";
                Player = await _userManager.GetUserAsync(User);

                Photo = await _context.Photos
                    .Where(p => p.Type == Photo.PhotoType.Profile)
                    .OrderByDescending(d => d.DateAdded)
                    .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId);
                return Page();
            }

            var photograph = new Photo
            {
                Type = Photo.PhotoType.Profile,
                DateAdded = DateTime.Now,
                ProfileId = Photo.ProfileId,
            };

            // Check the filename

            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg" || Image.ContentType == "image/png")
                {
                    fileName = GetUniqueName(Image.FileName);
                    var uploads = Path.Combine(_environment.WebRootPath, "images/uploads");
                    var filePath = Path.Combine(uploads, fileName);
                    await Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    photograph.URL = fileName;
                    photograph.Caption = Photo.Caption;

                    _context.Photos.Add(photograph);
                    StatusMessage = "Profile Image changed!";
                }
                else
                {
                    StatusMessage = "Error: That doesn't look like a photo file to us!";
                    Player = await _userManager.GetUserAsync(User);
                    Photo = await _context.Photos
                        .Where(p => p.Type == Photo.PhotoType.Profile)
                        .OrderByDescending(d => d.DateAdded)
                        .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId);
                    return Page();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index", new { id = photograph.ProfileId });

        }

        /// <summary>
        /// Create a unique file name for the file being uploaded.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
