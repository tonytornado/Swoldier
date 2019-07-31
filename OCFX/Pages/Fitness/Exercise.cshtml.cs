using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Linq;

namespace OCFX.Pages.Programs
{
    public class ExerciseModel : PageModel
    {
        private readonly OCFXContext _context;

        public ExerciseModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException("Where's my database??");
        }

        public Exercise Excel { get; set; }

        public void OnGet(int id)
        {
            Excel = _context.Exercises
                .SingleOrDefault(e => e.Id == id);
        }
    }
}