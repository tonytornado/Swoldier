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
        public Dictionary<int, string> Ad { get; private set; }
        public double CalorieBurn { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }
        

        public async Task OnGetAsync()
        {
            // Get the logged-in user information
			Task<OCFXUser> user = _userManager.GetUserAsync(User);

            // Get the profile and profile photo
            Profiler = await ProfileMethods.GetProfileAsync(_context, user.Result.ProfileId);
			ProfilePhoto = ProfileMethods.GetProfilePhoto(_context, Profiler.Id);

            // Get the completed quests
            CompletedQuests = QuestMethods.CheckCompletedQuests(_context, Profiler.Id);

            // Get advice!
            double bodyFat = Math.Round(ProfileMethods.BodyFat(Profiler, Profiler.Height, Profiler.Weight, Profiler.NeckMeasurement, Profiler.WaistMeasurement, Profiler.HipMeasurement), 1);
            Ad = ProfileMethods.Consultation(bodyFat, Profiler.Weight, Profiler.Height);
            CalorieBurn = CalorieTasker(Profiler.Weight, Profiler.Height, Profiler.Age);
        }

        private double CalorieTasker(int weight, int height, int age)
        {
            double weightc = weight / 2.2;
            double heightc = height * 2.54;
            double value = (10 * weightc) + (6.25 * heightc) - (5 * age) + 5;

            return Math.Round(value,0);
        }
    }
}