using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using OCFX_SPA.Data;

namespace OCFX_SPA.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<OCFXUser> _signInManager;
        private readonly UserManager<OCFXUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        //private readonly OCFXContext _context2;

        public RegisterModel(ApplicationDbContext context,
                             //OCFXContext context2,
                             UserManager<OCFXUser> userManager,
                             SignInManager<OCFXUser> signInManager,
                             ILogger<RegisterModel> logger,
                             IEmailSender emailSender)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_context2 = context2 ?? throw new ArgumentNullException(nameof(context2));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public Profile Profiler { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Email")]
        //    public string Email { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Password")]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }
        //}

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

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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


                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
