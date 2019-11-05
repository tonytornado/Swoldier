using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CharacterCreatorModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private string fileName;

        public CharacterCreatorModel(OCFXContext context, UserManager<OCFXUser> userManager, IWebHostEnvironment environment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public List<Archetype> Classes { get; set; }
        public List<Skill> Skills { get; set; }
        public SelectList ClassList { get; set; }
        public SelectList SkillList { get; set; }

        [BindProperty]
        public CreatorModel Input { get; set; }
        public IFormFile Avatar { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            Classes = _context.Archetypes.ToList();
            Skills = _context.Skills.ToList();

            ClassList = new SelectList(Classes, "Id", "FitType", null);
            SkillList = new SelectList(Skills, "Id", "Name", null);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Classes = _context.Archetypes.ToList();
                Skills = _context.Skills.ToList();

                ClassList = new SelectList(Classes, "Id", "FitType", null);
                SkillList = new SelectList(Skills, "Id", "Name", null);
                return Page();
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);

                // Check for other characters
                CharacterCheck(user.ProfileId);

                // Create the character using character data
                CharacterModel Character = new CharacterModel()
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
                    MainCharacter = true,
                    SkillList = new Collection<Skill>()
                    {
                        _context.Skills.FirstOrDefault(c => c.Id == Input.Primary),
                        _context.Skills.FirstOrDefault(c => c.Id == Input.Secondary),
                        _context.Skills.FirstOrDefault(c => c.Id == Input.Tertiary),
                    }
                };

                const string simpleCaption = "My avatar photo";
                Photo avatar = new Photo()
                {
                    Type = Photo.PhotoType.Avatar,
                    Caption = simpleCaption,
                    DateAdded = DateTime.Now,
                    ProfileId = Character.CharacterProfile.Id
                };

                if(Avatar != null)
                {
                    if (Avatar.ContentType == "image/jpeg" || Avatar.ContentType == "image/png")
                    {
                        // Add the photo for the avatar
                        fileName = GetUniqueName(Avatar.FileName);
                        string folderPath = $"images/{avatar.ProfileId}/avatarPhoto";
                        string upload = Path.Combine(_environment.WebRootPath, folderPath);
                        CheckFolderPath(upload);
                        string filePath = Path.Combine(upload, fileName);
                        await Avatar.CopyToAsync(new FileStream(filePath, FileMode.Create)).ConfigureAwait(false);
                        avatar.URL = $"../images/{avatar.ProfileId}/avatarPhoto/{fileName}";

                        _context.Photos.Add(avatar);
                        StatusMessage = "Your avatar has been created!";
                    }
                    else
                    {
                        const string errorMess = "This doesn't look like an image to us!";
                        throw new BadImageFormatException(errorMess);
                    }
                }
                

                // Put into the DB
                _context.Characters.Add(Character);
                _context.SaveChanges();
            }
            catch (Exception t)
            {
                StatusMessage = $"ERROR: {t.Message}";
                Classes = _context.Archetypes.ToList();
                Skills = _context.Skills.ToList();

                ClassList = new SelectList(Classes, "Id", "FitType", null);
                SkillList = new SelectList(Skills, "Id", "Name", null);

                return Page();
            }

            return RedirectToPage("././Dashboard/Index");
        }

        /// <summary>
        /// Checks for all characters connected to the supplied profile
        /// </summary>
        /// <param name="profileId"></param>
        private void CharacterCheck(int profileId)
        {
            var OtherChars = _context.Characters.Where(c => c.MainCharacter && c.CharacterProfile.Id == profileId).ToList();

            if (OtherChars.Count == 3)
            {
                throw new OverflowException("You can only have three characters in the system. You'll have to delete one of them. Someone's gotta go.");
            }
            else if (OtherChars.Count > 1)
            {
                foreach (var item in OtherChars)
                {
                    item.MainCharacter = false;
                    _context.SaveChanges();
                }
            }
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