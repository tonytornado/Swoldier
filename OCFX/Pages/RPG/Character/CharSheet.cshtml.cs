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

        public CharacterModel Sheet { get; private set; }

        public void OnGet(int Id)
        {
            Sheet = _context.Characters
                .Include(p => p.CharacterProfile)
                .Include(p => p.Campaign)
                .Include(p => p.Quests)
                .Include(p => p.SkillList)
                .SingleOrDefault(c => c.Id == Id);

        }
    }
}