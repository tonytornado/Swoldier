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
        public double WeightChange { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        

        public async Task OnGetAsync()
        {
            // Get the logged-in user information
            Task<OCFXUser> user = _userManager.GetUserAsync(User);

            // Get the profile and profile photo
            Profiler = await ProfileMethods.GetProfileAsync(_context, user.Result.ProfileId);

            // Get the completed quests
            CompletedQuests = QuestMethods.CheckCompletedQuests(_context, Profiler.Id);

            // Get advice!
            double bodyFat = Math.Round(ProfileMethods.BodyFat(Profiler, Profiler.Height, Profiler.Weight, Profiler.NeckMeasurement, Profiler.WaistMeasurement, Profiler.HipMeasurement), 1);
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
            if (weights != null)
            {
                double change;

                double FirstWeight = weights.First().Weight;
                double SecondWeight = weights.Last().Weight;

                change = FirstWeight == SecondWeight
                    ? 0.0
                    : SecondWeight - FirstWeight;

                return change;
            }
            throw new ArgumentNullException("This user has no weight... how??");
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
            var value = (10 * (weight / 2.2)) + (6.25 * (height * 2.54)) - (5 * age) + 5;

            return Math.Round(value, 0);
        }
    }
}