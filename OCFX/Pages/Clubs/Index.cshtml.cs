using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels;
using OCFX.DataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Clubs
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool CurrentSearch { get; set; }
        public IQueryable<Gym> ClubListing { get; private set; }
        public PaginatedList<Gym> Gyms { get; private set; }

        public async Task OnGetAsync(string searchString, int? pageIndex)
        {
            ClubListing = from s in _context.Gyms select s;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                CurrentSearch = true;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                ClubListing = ClubListing.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            int pageSize = 5;
            Gyms = await PaginatedList<Gym>.CreateAsync(ClubListing.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}