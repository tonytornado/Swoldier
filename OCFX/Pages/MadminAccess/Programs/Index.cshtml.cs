using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataRepo;
using OCFX.DataModels;

namespace OCFX.Pages.MadminAccess.Programs
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context;
        }

        public IList<WorkoutProgram> WorkoutProgram { get;set; }

        public async Task OnGetAsync()
        {
            WorkoutProgram = await _context.WorkoutPrograms
                .Include(w => w.Exercise)
                .Include(w => w.Workout).ToListAsync();
        }
    }
}
