using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.RPG
{
    public class CharacterCreatorModel : PageModel
    {
        private readonly OCFXContext _context;

        public List<Archetype> Classes { get; set; }
        public List<Skill> Skills { get; set; }
        [BindProperty]
        public CreatorModel Input { get; set; }

        public class CreatorModel
        {
            public string FirstName { get; internal set; }
            public string LastName { get; internal set; }
            public int STR { get; internal set; }
            public int DEX { get; internal set; }
            public int CON { get; internal set; }
            public int VIT { get; internal set; }
            public int SPD { get; internal set; }
            public int MVN { get; internal set; }
            public string Backstory { get; internal set; }
            public string Drive { get; internal set; }
            public string Goal { get; internal set; }
        }

        public void OnGet()
        {
            Classes = _context.Archetypes.ToList();
            Skills = _context.Skills.ToList();
        }

        public IActionResult OnPost()
        {
            var Character = new CharacterModel()
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                StrengthStat = Input.STR,
                DexterityStat = Input.DEX,
                ConcentrationStat = Input.CON,
                ConstitutionStat = Input.VIT,
                SpeedStat = Input.SPD,
                MotivationStat = Input.MVN,
                BackStory = Input.Backstory,
                DriveStory = Input.Drive,
                Goals = Input.Goal,
            };
            _context.Characters.Add(Character);
            _context.SaveChanges();

            return RedirectToPage("./Dashboard/");
        }
    }
}