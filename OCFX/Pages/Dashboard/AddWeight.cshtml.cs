using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.IO;
using System.Linq;

namespace OCFX.Pages.Dashboard
{
    public class AddWeightModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly IHostingEnvironment _environment;

        public AddWeightModel(OCFXContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        [BindProperty]
        public InputModel Input { get; set; }
        public Photo ProgressPhoto { get; set; }
        public IFormFile Image { get; set; }

        [TempData]
        public string StatusMessage { get; private set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost(int? Id)
        {
            if (ModelState.IsValid && Id != null)
            {

                ProgressPhoto = new Photo
                {
                    Type = Photo.PhotoType.Progress,
                    DateAdded = DateTime.Now,
                    ProfileId = ProgressPhoto.ProfileId,
                };

                if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                {
                    StatusMessage = "Error: That doesn't look like a photo file to us!";
                    return Page();
                }
                else
                {
                    string fileName = GetUniqueName(Image.FileName);
                    string folderPath = $"images/{ProgressPhoto.ProfileId}/profilePhoto";
                    string upload = Path.Combine(_environment.WebRootPath, folderPath);
                    CheckFolderPath(upload);
                    string filePath = Path.Combine(upload, fileName);
                    Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    ProgressPhoto.URL = $"../images/{ProgressPhoto.ProfileId}/profilePhoto/{fileName}";
                    ProgressPhoto.Caption = ProgressPhoto.Caption;

                    _context.Photos.Add(ProgressPhoto);
                    StatusMessage = "Profile Image changed!";
                }


                var Weight = new WeightMeasurement
                {
                    Date = DateTime.Now,
                    Weight = Input.Weight,
                    ProgressPhoto = ProgressPhoto,
                    Profile = _context.Profiles.SingleOrDefault(c => c.Id == Id)
                };

                var Post = new Post
                {
                    DatePosted = DateTime.Now,
                    Profile = Weight.Profile,
                    ProfileId = Weight.Profile.Id,
                    Text = $"{Weight.Profile.FirstName} now weighs {Weight.Weight}!",
                };

                StatusMessage = "New weight added!";

                RedirectToPage("Index");
            }

            return Page();
        }

        /// <summary>
        /// Checks for folder on the server; and creates it if necessary
        /// </summary>
        /// <param name="v">The folder path</param>
        private void CheckFolderPath(string v)
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
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{Path.GetExtension(fileName)}";
        }
    }

    public class InputModel
    {
        public double Weight { get; internal set; }
    }
}