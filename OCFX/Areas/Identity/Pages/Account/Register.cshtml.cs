using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OCFX.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<OCFXUser> _signInManager;
        private readonly UserManager<OCFXUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly OCFXContext _context;

        public RegisterModel(OCFXContext context,
                             UserManager<OCFXUser> userManager,
                             SignInManager<OCFXUser> signInManager,
                             ILogger<RegisterModel> logger,
                             IEmailSender emailSender)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public Phone Phoney { get; set; }
        [BindProperty]
        public Address Addressing { get; set; }
        [BindProperty]
        public Profile Profiler { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// The input model for the Register Model
        /// </summary>
		public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Your passwords do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "First Name")]
            [Required]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [Required]
            public string LastName { get; set; }

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            [Required]
            public DateTime DOB { get; set; }

            [Display(Name = "Campaign Choice")]
            public int CampaignId { get; set; }

            [Display(Name = "Class Choice")]
            public int ClassId { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Posts the form
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // Create a user profile
                OCFXUser user = new OCFXUser
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email,
                    Email = Input.Email,
                    DOB = Input.DOB,
                    NameChangedDate = DateTime.Now,
                    Profile = new Profile
                    {
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        DOB = Input.DOB,
                        Gender = Profiler.Gender,
                        Weight = Profiler.Weight,
                        Height = Profiler.Height,
                        NeckMeasurement = Profiler.NeckMeasurement = 0,
                        WaistMeasurement = Profiler.WaistMeasurement = 0,
                        HipMeasurement = Profiler.HipMeasurement = 0,
                        BackStory = Profiler.BackStory,
                        DriveStory = Profiler.DriveStory,
                        Goals = Profiler.Goals,
                        StrengthStat = Profiler.StrengthStat,
                        DexterityStat = Profiler.DexterityStat,
                        MotivationStat = Profiler.MotivationStat,
                        ConstitutionStat = Profiler.ConstitutionStat,
                        SpeedStat = Profiler.SpeedStat,
                        ConcentrationStat = Profiler.ConcentrationStat,
                        FitStyle = await _context.Archetypes.FirstOrDefaultAsync(p => p.Id == Input.ClassId),
                        Campaign = await _context.Campaigns.FirstOrDefaultAsync(p => p.Id == 1),
                        Quest = await _context.Quests.FirstOrDefaultAsync(p => p.Id == 1),

                        Phones = new Collection<Phone>(),
                        Addresses = new Collection<Address>(),
                        Photos = new Collection<Photo>(),
                    }
                };

                // Add the phone number
                if (Phoney == null)
                {
                    user.Profile.Phones.Add(new Phone
                    {
                        AreaCode = Phoney.AreaCode,
                        PhoneTypeName = Phoney.PhoneTypeName,
                        PhoneNumber = Phoney.PhoneNumber
                    });
                }

                // Add the address
                if (Addressing == null)
                {
                    user.Profile.Addresses.Add(new Address
                    {
                        AddressTypeName = Addressing.AddressTypeName,
                        StreetName = Addressing.StreetName,
                        CityName = Addressing.CityName,
                        StateName = Addressing.StateName,
                        ZipCode = Addressing.ZipCode
                    });
                }

                // Add the default profile picture
                user.Profile.Photos.Add(new Photo
                {
                    DateAdded = DateTime.Now,
                    URL = "../images/default.jpg",
                    Caption = "Default Look",
                    Type = Photo.PhotoType.Profile
                });

                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);
                    if (user.ProfileId == 1)
                    {
                        await _userManager.AddToRoleAsync(user, "Administrator");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    // Add the newest quest to the quest book and start
                    _context.QuestLogs.Add(new QuestLog
                    {
                        Profile = user.Profile,
                        Campaign = user.Profile.Campaign,
                        QuestId = user.Profile.Quest.Id,
                        Completed = false,
                    });

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}