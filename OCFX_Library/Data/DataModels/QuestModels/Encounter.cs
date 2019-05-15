using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Encounter
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "HP (Hit Points)")]
        public int HP { get; set; }
        [Display(Name = "STR")]
        public int STR { get; set; }
        [Display(Name = "VIT")]
        public int VIT { get; set; }
        [Display(Name = "SPD")]
        public int SPD { get; set; }
        [Display(Name = "DEX")]
        public int DEX { get; set; }
        [Display(Name = "CON")]
        public int CON { get; set; }
        [Display(Name = "MVN")]
        public int MVN { get; set; }
        [Display(Name = "Story")]
        public long Background { get; set; }

        public Skills[] SkillSet { get; set; }
    }

    public class PersonalEncounter : Encounter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BossEncounter: Encounter
    {
        public string Name { get; set; }
        public int Armor { get; set; }
        public Skills BurstSkill { get; set; }
    }
}