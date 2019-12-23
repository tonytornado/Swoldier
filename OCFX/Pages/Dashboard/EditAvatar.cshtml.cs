using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Profiles
{
    public class EditAvatarModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly IWebHostEnvironment _environment;
        private string fileName;
        private readonly UserManager<OCFXUser> _userManager;

        public EditAvatarModel(OCFXContext context, IWebHostEnvironment environment, UserManager<OCFXUser> userManager)
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
            Player = await _userManager.GetUserAsync(User).ConfigureAwait(false);

            Photo = await _context.Photos
                .Where(p => p.Type == Photo.PhotoType.Profile)
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId).ConfigureAwait(false);

            if (Photo.URL == "../images/default.jpg")
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
                Player = await _userManager.GetUserAsync(User).ConfigureAwait(false);

                Photo = await _context.Photos
                    .Where(p => p.Type == Photo.PhotoType.Profile)
                    .OrderByDescending(d => d.DateAdded)
                    .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId).ConfigureAwait(false);
                return Page();
            }

            Photo photograph = new Photo
            {
                Type = Photo.PhotoType.Profile,
                DateAdded = DateTime.Now,
                ProfileId = Photo.ProfileId,
            };

            // Check the filename

            if (Image != null)
            {
                if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                {
                    StatusMessage = "Error: That doesn't look like a photo file to us!";
                    Player = await _userManager.GetUserAsync(User).ConfigureAwait(false);
                    Photo = await _context.Photos
                        .Where(p => p.Type == Photo.PhotoType.Profile)
                        .OrderByDescending(d => d.DateAdded)
                        .FirstOrDefaultAsync(p => p.ProfileId == Player.ProfileId).ConfigureAwait(false);
                    return Page();
                }
                else
                {
                    fileName = GetUniqueName(Image.FileName);
                    string folderPath = $"images/{Photo.ProfileId}/profilePhoto";
                    string upload = Path.Combine(_environment.WebRootPath, folderPath);
                    CheckFolderPath(upload);
                    string filePath = Path.Combine(upload, fileName);
                    await Image.CopyToAsync(new FileStream(
                        path: filePath,
                        mode: FileMode.Create)).ConfigureAwait(false);
                    photograph.URL = $"../images/{Photo.ProfileId}/profilePhoto/{fileName}";
                    photograph.Caption = Photo.Caption;

                    _context.Photos.Add(photograph);
                    StatusMessage = "Profile Image changed!";
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return RedirectToPage("./Index", new { id = photograph.ProfileId });

        }

        /// <summary>
        /// Checks for folder on the server; and creates it if necessary
        /// </summary>
        /// <param name="v">The folder path</param>
        private static void CheckFolderPath(string v)
        {
            if (!Directory.Exists(v))
            {
                Directory.CreateDirectory(v);
            }
        }

        /// <summary>
        /// Create a unique file name for the file being uploaded.
        /// </summary>
        /// <param name="fileName">A filename string</param>
        /// <returns></returns>
        private static string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileName)}";
        }
    }
}
