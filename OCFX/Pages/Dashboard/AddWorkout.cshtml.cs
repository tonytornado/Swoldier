using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Dashboard
{
    public class AddWorkoutModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public AddWorkoutModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList WL { get; set; }
        public List<Workout> WorkoutList { get; set; }

        public void OnGet()
        {
            var listing = _context.Workouts.ToList();
            WL = new SelectList(listing, "Id", "Title", null);
        }

        public async Task<ActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            // Create the workout log
            var Workout = new WorkoutSetLog
            {
                Date = DateTime.Now,
                Profile = _context.Profiles.Single(c => c.Id == user.ProfileId),
                Workout = _context.Workouts.Single(c => c.Id == Input.WorkoutId),
                Duration = Input.Time,
                Notes = Input.Notes
            };
            _context.WorkoutSetLogs.Add(Workout);


            // Create the post for the workout log on the user's profile page.
            Post Post = new Post
            {
                DatePosted = DateTime.Now,
                ProfileId = Workout.Profile.Id,
                EntryId = Workout.Profile.Id,
                Text = $"{Workout.Profile.FirstName} did {Workout.Duration} minutes of {Workout.Workout.Title}!",
            };
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            StatusMessage = "New workout logged!";
            return Redirect("./Index");
        }

        public class InputModel
        {
            [Display(Name = "Workout")]
            public int WorkoutId { get; set; }
            [Display(Name = "Notes")]
            public string Notes { get; set; }
            [Display(Name = "Duration")]
            public int Time { get; set; }
        }
    }
}