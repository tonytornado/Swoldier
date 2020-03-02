using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Dashboard
{
    public class AddWeightModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<OCFXUser> _userManager;

        public AddWeightModel(OCFXContext context,
                              IWebHostEnvironment environment,
                              UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IFormFile Image { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public OCFXUser Player { get; private set; }

        public async void OnGetAsync()
        {
            Player = await _userManager.GetUserAsync(User).ConfigureAwait(true);
        }

        /// <summary>
        /// Submits the form
        /// </summary>
        /// <param name="Id">The user's Id</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            Photo ProgressPhoto = new Photo
            {
                Type = Photo.PhotoType.Progress,
                DateAdded = DateTime.Now,
                ProfileId = Id,
            };

            if (Image == null)
            {
                ProgressPhoto.Url = "../images/default.jpg";
                ProgressPhoto.Caption = "No Photo Data";
            }
            else
            {
                if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                {
                    StatusMessage = "Error: That doesn't look like a photo file to us!";
                    RedirectToPage("AddWeight");
                }
                else
                {
                    UploadImageToFolder(_environment,
                        Image,
                        ProgressPhoto,
                        Id,
                        "progressPhoto",
                        ProgressPhoto.Caption);
                }
            }
            _context.Photos.Add(ProgressPhoto);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            WeightMeasurement Weight = new WeightMeasurement
            {
                Date = DateTime.Now,
                Weight = Input.Weight,
                ProgressPhoto = ProgressPhoto,
                Profile = _context.Profiles.SingleOrDefault(c => c.Id == Id)
            };
            _context.Weights.Add(Weight);
            Weight.Profile.Weight = (int)Weight.Weight;
            await _context.SaveChangesAsync().ConfigureAwait(true);

            Post Post = new Post
            {
                DatePosted = DateTime.Now,
                ProfileId = Weight.Profile.Id,
                EntryId = Weight.Profile.Id,
                Text = $"{Weight.Profile.FirstName} now weighs {Weight.Weight}!",
            };
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            

            StatusMessage = "New weight added!";
            return Redirect("./Index");
        }

        /// <summary>
        /// Uploads an image to a designated folder on the server
        /// </summary>
        /// <param name="environment">The Hosting Environment</param>
        /// <param name="Image">IFormFile Image</param>
        /// <param name="PhotoType">An instance of a Photo type object</param>
        /// <param name="Id">An ID</param>
        /// <param name="PhotoFolder">Name of the folder</param>
        /// <param name="Caption">Optional caption for the photo</param>
        private static void UploadImageToFolder(IWebHostEnvironment environment, IFormFile Image, Photo PhotoType, int Id, string PhotoFolder, string Caption)
        {
            if (environment is null)
                {
                    throw new ArgumentNullException(nameof(environment));
                }
                // Create the filename and folder path
                string fileName = ImageFileManagement.GetUniqueName(Image.FileName);
                string folderPath = $"images/{Id}/{PhotoFolder}";
                string upload = Path.Combine(environment.WebRootPath, folderPath);

                // Check if the folder already exists
                ImageFileManagement.CheckFolderPath(upload);
                string filePath = Path.Combine(upload, fileName);

                // Get ready to upload and add some things.
                Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                PhotoType.Url = $"../images/{Id}/{PhotoFolder}/{fileName}";
                PhotoType.Caption = Caption;
        }

        public class InputModel
        {
            public double Weight { get; set; }
            public string Filename { get; set; }
            public string Caption { get; set; }
        }
    }
}