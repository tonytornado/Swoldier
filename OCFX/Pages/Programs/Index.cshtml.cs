using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX.Pages.Programs
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Variables
        public List<Workout> Workouts { get; private set; }

        public void OnGet()
        {
            // Get a list of workouts
            Workouts = _context.Workouts.ToList();
        }
    }
}