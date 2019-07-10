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

namespace OCFX.Pages.Dashboard
{
    public class AddWeightModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<OCFXUser> _userManager;

        public AddWeightModel(OCFXContext context, IHostingEnvironment environment, UserManager<OCFXUser> userManager)
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
            Player = await _userManager.GetUserAsync(User);
        }

        /// <summary>
        /// Submits the form
        /// </summary>
        /// <param name="Id">The user's Id</param>
        /// <returns></returns>
        public IActionResult OnPost(int Id)
        {
            if (ModelState.IsValid)
            {
                Photo ProgressPhoto = new Photo
                {
                    Type = Photo.PhotoType.Progress,
                    DateAdded = DateTime.Now,
                    ProfileId = Id,
                };

                if (Input.Filename == null)
                {
                    ProgressPhoto.URL = "../images/default.jpg";
                    ProgressPhoto.Caption = "No Photo Data";
                }
                else
                {
                    if (Image.ContentType != "image/jpeg" && Image.ContentType != "image/png")
                    {
                        StatusMessage = "Error: That doesn't look like a photo file to us!";
                        return Page();
                    }
                    else
                    {
                        ImageFileManagement.UploadImageToFolder(_environment, Image, ProgressPhoto, Id, "progressPhoto", ProgressPhoto.Caption);
                    }
                }
                _context.Photos.Add(ProgressPhoto);
                _context.SaveChanges();

                WeightMeasurement Weight = new WeightMeasurement
                {
                    Date = DateTime.Now,
                    Weight = Input.Weight,
                    ProgressPhoto = ProgressPhoto,
                    Profile = _context.Profiles.SingleOrDefault(c => c.Id == Id)
                };
                _context.Weights.Add(Weight);
                _context.SaveChanges();

                Post Post = new Post
                {
                    DatePosted = DateTime.Now,
                    Profile = Weight.Profile,
                    ProfileId = Weight.Profile.Id,
                    Text = $"{Weight.Profile.FirstName} now weighs {Weight.Weight}!",
                };
                _context.Posts.Add(Post);
                _context.SaveChanges();
                
                StatusMessage = "New weight added!";
                RedirectToPage("Index");
            }

            return Page();
        }
    }

    public class InputModel
    {
        public double Weight { get; set; }
        public string Filename { get; set; }
        public string Caption { get; set; }
    }
}