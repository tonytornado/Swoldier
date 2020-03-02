using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Dashboard
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly UserManager<OCFXUser> _userManager;
        private readonly OCFXContext _context;

        public DashboardModel(UserManager<OCFXUser> userManager, OCFXContext context, string userTitle)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            UserTitle = userTitle;
        }

        public ProfileSheet Profiler { get; private set; }
        public List<int> CompletedQuests { get;  set; }

        public string UserTitle { get; }
        public Dictionary<int, string> Ad { get; private set; }
        public double CalorieBurn { get; private set; }
        public double WeightChange { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }
        

        public async Task OnGetAsync()
        {
            // Get the logged-in user information
            var user = await _userManager.GetUserAsync(User);

            // Get the profile and profile photo
            Profiler = await ProfileMethods.GetProfileAsync(_context, user.ProfileId);

            // Get the completed quests
            CompletedQuests = QuestMethods.CheckCompletedQuests(_context, Profiler.Id);

            // Get advice!
            var bodyFat = Math.Round(ProfileMethods.BodyFat(Profiler, Profiler.Height, Profiler.Weight, Profiler.NeckMeasurement, Profiler.WaistMeasurement, Profiler.HipMeasurement), 1);
            Ad = ProfileMethods.Consultation(bodyFat, Profiler.Weight, Profiler.Height);
            CalorieBurn = CalorieTasker(Profiler.Weight, Profiler.Height, Profiler.Age);

            WeightChange = ShowWeightChange(Profiler.Weights);

        }

        /// <summary>
        /// Compares first weight and last weight
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        private static double ShowWeightChange(Collection<WeightMeasurement> weights)
        {
            if (weights == null)
            {
                throw new ArgumentNullException("This user has no weight... how??");
            }

            double firstWeight = weights.First().Weight;
            double secondWeight = weights.Last().Weight;

            var change = firstWeight == secondWeight ? 0.0 : secondWeight - firstWeight;

            return change;
        }

        /// <summary>
        /// Gets the calorie details for someone.
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        private static double CalorieTasker(int weight, int height, int age)
        {
            var weightc = weight / 2.2;
            var heightc = height * 2.54;
            var value = (10 * weightc) + (6.25 * heightc) - (5 * age) + 5;

            return Math.Round(value, 0);
        }
    }
}