using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Programs
{
    public class ExerciseModel : PageModel
    {
        private readonly OCFXContext _context;

        public ExerciseModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException("Where's my database??");
        }

        public Exercise Ex { get; set; }

        public void OnGet(int Id)
        {
            Ex = _context.Exercises
                .Single(e => e.Id == Id);
        }
    }
}