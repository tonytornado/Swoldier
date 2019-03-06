using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;

namespace OCFX.Pages.Dashboard
{
    [Authorize]
    public class DashboardModel : PageModel
    {
		private readonly UserManager<OCFXUser> _userManager;
		private readonly OCFXContext _context;

		public DashboardModel(UserManager<OCFXUser> userManager, OCFXContext context)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public Profile Profiler { get; private set; }
		public Photo ProfilePhoto { get; private set; }
        public List<int> CompletedQuests { get; private set; }

        public string UserTitle { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            // Get the logged-in user information
			Task<OCFXUser> user = _userManager.GetUserAsync(User);

            // Get the profile and profile photo
            Profiler = await ProfileMethods.GetProfileAsync(_context, user.Result.ProfileId);
			ProfilePhoto = await ProfileMethods.GetProfilePhoto(_context, Profiler.Id);

            // Get user's full name
            UserTitle = Profiler.FirstName + " " + Profiler.LastName;

            // Get the completed quests
            CompletedQuests = QuestMethods.CheckCompletedQuests(_context, Profiler.Id);

            // Check for a status message?
#pragma warning disable CS0168 // Variable is declared but never used
            string StatusMessage;
#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}