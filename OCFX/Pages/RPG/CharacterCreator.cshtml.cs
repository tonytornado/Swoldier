using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.RPG
{
    public class CharacterCreatorModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;
        private readonly IHostingEnvironment _environment;
        private string fileName;

        public CharacterCreatorModel(OCFXContext context, UserManager<OCFXUser> userManager, IHostingEnvironment environment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public List<Archetype> Classes { get; set; }
        public List<Skill> Skills { get; set; }
        public SelectList ClassList { get; set; }

        [BindProperty]
        public CreatorModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class CreatorModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int STR { get; set; }
            public int DEX { get; set; }
            public int CON { get; set; }
            public int VIT { get; set; }
            public int SPD { get; set; }
            public int MVN { get; set; }
            public string Backstory { get; set; }
            public string Drive { get; set; }
            public string Goal { get; set; }
            public int Class { get; set; }

            public IFormFile Avatar { get; set; }
            public int Primary { get; internal set; }
            public int Secondary { get; internal set; }
            public int Tertiary { get; internal set; }
        }

        public void OnGet()
        {
            Classes = _context.Archetypes.ToList();
            Skills = _context.Skills.ToList();

            ClassList = new SelectList(Classes, "Id", "FitType", null);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            // Create the character using character data
            var Character = new CharacterModel()
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                StrengthStat = Input.STR,
                DexterityStat = Input.DEX,
                ConcentrationStat = Input.CON,
                ConstitutionStat = Input.VIT,
                SpeedStat = Input.SPD,
                MotivationStat = Input.MVN,
                BackStory = Input.Backstory,
                DriveStory = Input.Drive,
                Goals = Input.Goal,
                FitStyle = _context.Archetypes.FirstOrDefault(c => c.Id == Input.Class),
                CharacterProfile = _context.Profiles.FirstOrDefault(c => c.Id == user.ProfileId),
                PrimarySkill = _context.Skills.FirstOrDefault(c => c.Id == Input.Primary),
                SecondarySkill = _context.Skills.FirstOrDefault(c => c.Id == Input.Secondary),
                TertiarySKill = _context.Skills.FirstOrDefault(c => c.Id == Input.Tertiary),
            };

            Photo avatar = new Photo()
            {
                Type = Photo.PhotoType.Avatar,
                Caption = "My avatar photo",
                DateAdded = DateTime.Now,
                ProfileId = Character.CharacterProfile.Id
            };
            

            // Add the photo for the avatar
            fileName = GetUniqueName(Input.Avatar.FileName);
            string folderPath = $"images/{avatar.ProfileId}/avatarPhoto";
            string upload = Path.Combine(_environment.WebRootPath, folderPath);
            CheckFolderPath(upload);
            string filePath = Path.Combine(upload, fileName);
            await Input.Avatar.CopyToAsync(new FileStream(filePath, FileMode.Create));
            avatar.URL = $"../images/{avatar.ProfileId}/avatarPhoto/{fileName}";

            StatusMessage = "Your avatar has been created!";

            // Put into the DB
            _context.Photos.Add(avatar);
            _context.Characters.Add(Character);
            _context.SaveChanges();

            return RedirectToPage("./Dashboard/");
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
}