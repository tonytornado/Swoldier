using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    [NotMapped]
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

    [Table("Enemies")]
    public class PersonalEncounter : Encounter
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

    [Table("Bosses")]
    public class BossEncounter: Encounter
    {
        [Display(Name = "Boss Name")]
        public string Name { get; set; }
        [Display(Name = "Armor Level")]
        public int Armor { get; set; }
        [Display(Name = "Boss Skill")]
        public Skills BurstSkill { get; set; }
    }
}