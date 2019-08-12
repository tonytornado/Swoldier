using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OCFX_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<OCFXUser> _signInManager;
        private readonly UserManager<OCFXUser> _userManager;
        private readonly OCFXContext _context;
        private readonly ILogger _logger;

        public UsersController(SignInManager<OCFXUser> signInManager, UserManager<OCFXUser> userManager, OCFXContext context, ILogger logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="DOB"></param>
        /// <param name="gender"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="password"></param>
        /// <param name="backStory"></param>
        /// <param name="driveStory"></param>
        /// <param name="goals"></param>
        /// <param name="classId"></param>
        /// <param name="neckMeasurement"></param>
        /// <param name="waistMeasurement"></param>
        /// <param name="hipMeasurement"></param>
        /// <param name="strengthStat"></param>
        /// <param name="dexterityStat"></param>
        /// <param name="motivationStat"></param>
        /// <param name="constitutionStat"></param>
        /// <param name="speedStat"></param>
        /// <param name="concentrationStat"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, DateTime DOB, Profile.GenderSpectrum gender, int weight, int height, string password, string backStory, string driveStory, string goals, int classId, int neckMeasurement = 0, int waistMeasurement = 0, int hipMeasurement = 0, int strengthStat = 5, int dexterityStat = 5, int motivationStat = 5, int constitutionStat = 5, int speedStat = 5, int concentrationStat = 5)
        {

            OCFXUser user = new OCFXUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                DOB = DOB,
                NameChangedDate = DateTime.Now,
                Profile = new Profile
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DOB = DOB,
                    Gender = gender,
                    Weight = weight,
                    Height = height,
                    NeckMeasurement = neckMeasurement = 0,
                    WaistMeasurement = waistMeasurement = 0,
                    HipMeasurement = hipMeasurement = 0,
                    BackStory = backStory,
                    DriveStory = driveStory,
                    Goals = goals,
                    StrengthStat = strengthStat,
                    DexterityStat = dexterityStat,
                    MotivationStat = motivationStat,
                    ConstitutionStat = constitutionStat,
                    SpeedStat = speedStat,
                    ConcentrationStat = concentrationStat,
                    FitStyle = await _context.Archetypes.FirstOrDefaultAsync(p => p.Id == classId),
                    Campaign = await _context.Campaigns.FirstOrDefaultAsync(p => p.Id == 1),
                    Quest = await _context.Quests.FirstOrDefaultAsync(p => p.Id == 1),

                    Photos = new Collection<Photo>(),
                    Weights = new Collection<WeightMeasurement>()
                }
            };


            // Add the default profile picture
            Photo FirstProfilePhoto = new Photo
            {
                DateAdded = DateTime.Now,
                URL = "../images/default.jpg",
                Caption = "Default Look",
                Type = Photo.PhotoType.Profile
            };
            user.Profile.Photos.Add(FirstProfilePhoto);


            // Add the first quest to the quest book and start
            // Add the newest quest to the quest book and start
            _context.QuestLogs.Add(new QuestLog
            {
                Profile = user.Profile,
                Campaign = user.Profile.Campaign,
                Quest = user.Profile.Quest,
                Completed = false,
            });
            IdentityResult result = await _userManager.CreateAsync(user, password);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(OCFXUser username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return Ok();
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }
    }
}