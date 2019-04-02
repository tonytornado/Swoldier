using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.MadminAccess.Programs
{
    public class CreateModel : PageModel
    {
        private readonly OCFXContext _context;

        public CreateModel(OCFXContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {

        Exerlist = new SelectList(_context.Exercises.ToList(), "Id", "ExName");
        Worklist = new SelectList(_context.Workouts.ToList(), "Id", "Title");
        CampaignList = new SelectList(_context.Campaigns.ToList(), "Id", "CampaignName");

		ExerciseSet = await _context.Exercises.ToListAsync();

		return Page();
        }

        [BindProperty]
        public WorkoutProgram WorkoutProgram { get; set; }

		public SelectList Exerlist { get; set; }
		public SelectList Worklist { get; set; }
        public SelectList CampaignList { get; set; }

        public List<Exercise> ExerciseSet { get; set; }

		public async Task<IActionResult> OnPostAsync(string[] selectedExercises)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

			if (selectedExercises != null)
			{
                // Begin adding all selected exercises in order of choice
				var programs = new List<WorkoutProgram>();
                int Counter = 0;
                foreach (var exercise in selectedExercises)
				{
                    Counter++;
					var clod = new WorkoutProgram
					{
						ExerciseId = int.Parse(exercise),
						WorkoutId = WorkoutProgram.WorkoutId,
                        CampaignId = int.Parse(CampaignList.DataValueField),
                        Sets = WorkoutProgram.Sets,
                        Repetitions = WorkoutProgram.Repetitions,
                        Order = Counter
					};
					programs.Add(clod);
				}

				foreach (var item in programs)
				{
					_context.WorkoutPrograms.Add(item);
				}
				await _context.SaveChangesAsync();
				return RedirectToPage("./Index");
			}

			Exerlist = new SelectList(_context.Exercises, "Id", "ExName");
			Worklist = new SelectList(_context.Workouts, "Id", "Title");
            CampaignList = new SelectList(_context.Campaigns.ToList(), "Id", "CampaignName");

            ExerciseSet = await _context.Exercises.ToListAsync();

			return Page();
		}
    }
}