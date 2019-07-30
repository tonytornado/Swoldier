using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Fitness
{
    public class HistoryModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public List<WorkoutSetLog> History { get; set; }
        public int FullTime { get; private set; }

        public HistoryModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async void OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            History = _context.WorkoutSetLogs.Include(p => p.Workout).OrderByDescending(c => c.Date).Where(p => p.Profile.Id == user.ProfileId).ToList();
            FullTime = Stopwatch(History);
        }

        private int Stopwatch(List<WorkoutSetLog> X)
        {
            int FullTime = 0;
            for (int i = 0; i < X.Count; i++)
            {
                WorkoutSetLog time = X[i];
                FullTime += time.Duration;
            }
            return FullTime;
        }
    }
}