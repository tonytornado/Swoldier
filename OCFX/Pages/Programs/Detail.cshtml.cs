using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Programs
{
    public class DetailModel : PageModel
    {
        private readonly OCFXContext _context;

        public DetailModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Workout WorkoutDetail { get; private set; }
        public List<WorkoutProgram> Exercises { get; private set; }

        public void OnGet(int id)
        {
            WorkoutDetail = _context.Workouts.SingleOrDefault(w => w.Id == id);

            Exercises = _context.WorkoutPrograms.Include(w => w.Exercise).Where(w => w.WorkoutId == id).ToList();
        }
    }
}