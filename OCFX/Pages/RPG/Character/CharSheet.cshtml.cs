using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.RPG.Character
{
    public class CharSheetModel : PageModel
    {
        private readonly OCFXContext _context;

        public CharSheetModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CharacterModel Sheet { get; private set; }

        public async void OnGet(int Id)
        {
            Sheet = await _context.Characters
                .Include(p => p.CharacterProfile)
                .Include(p => p.Campaign)
                .Include(p => p.Quests)
                .Include(p => p.SkillList)
                .Include(p => p.FitStyle)
                .SingleOrDefaultAsync(c => c.Id == Id);

        }
    }
}