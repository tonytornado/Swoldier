﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                ProgressPhoto.URL = "../images/default.jpg";
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
                    ImageFileManagement.UploadImageToFolder(_environment, Image, ProgressPhoto, Id, "progressPhoto", ProgressPhoto.Caption);
                }
            }
            _context.Photos.Add(ProgressPhoto);
            await _context.SaveChangesAsync();

            WeightMeasurement Weight = new WeightMeasurement
            {
                Date = DateTime.Now,
                Weight = Input.Weight,
                ProgressPhoto = ProgressPhoto,
                Profile = _context.Profiles.SingleOrDefault(c => c.Id == Id)
            };
            _context.Weights.Add(Weight);
            Weight.Profile.Weight = (int)Weight.Weight;
            await _context.SaveChangesAsync();

            Post Post = new Post
            {
                DatePosted = DateTime.Now,
                ProfileId = Weight.Profile.Id,
                EntryId = Weight.Profile.Id,
                Text = $"{Weight.Profile.FirstName} now weighs {Weight.Weight}!",
            };
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            

            StatusMessage = "New weight added!";
            return Redirect("./Index");
        }

        public class InputModel
        {
            public double Weight { get; set; }
            public string Filename { get; set; }
            public string Caption { get; set; }
        }
    }
}