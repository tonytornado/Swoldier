using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// Describes an encounter class
    /// </summary>
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
        public string Background { get; set; }

        /// <summary>
        /// The NPC's skill set
        /// </summary>
        public Skills[] SkillSet { get; set; }
    }

    /// <summary>
    /// These are the NPC's you may have to deal with
    /// </summary>
    [Table("NPCs")]
    public class PersonalEncounter : Encounter
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

    /// <summary>
    /// Represents a boss encounter that adds special elements to the fight
    /// </summary>
    [Table("Bosses")]
    public class BossEncounter : Encounter
    {
        [Display(Name = "Boss Name")]
        public string Name { get; set; }
        [Display(Name = "Armor Level")]
        public int Armor { get; set; }
        [Display(Name = "Boss Skill")]
        public Skills BurstSkill { get; set; }

        /// <summary>
        /// Bosses get armor, didn't you know? This adds more heft to the fight.
        /// </summary>
        public int BossArmor => Armor + CON;
        public int BossHP => HP * 2;
    }
}