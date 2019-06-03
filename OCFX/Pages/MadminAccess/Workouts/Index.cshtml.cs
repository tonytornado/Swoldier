using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCFX.Pages.MadminAccess.Workouts
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context;
        }

        public IList<Workout> Workout { get; set; }

        public async Task OnGetAsync()
        {
            Workout = await _context.Workouts.ToListAsync();
        }
    }
}
