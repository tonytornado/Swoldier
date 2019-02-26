using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Clubs
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Gym> Gyms { get; private set; }

        public void OnGet() => Gyms = _context.Gyms.ToList();
    }
}