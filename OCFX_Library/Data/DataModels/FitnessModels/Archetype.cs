using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// Character archetypes. This provides boosts and such.
    /// </summary>
    public class Archetype
    {
        [Key]
        [Display(Name = "Character Class")]
        public int Id { get; set; }
        [Display(Name = "Skill Level")]
        public SkillType SkillMod { get; set; }
        [Display(Name = "Fit Type")]
        public ClassType FitType { get; set; }

        [NotMapped]
        public string SubTitle => $"{SkillMod} {FitType}";

        // Modifications to the base stats of the character.
        [Display(Name = "STR")]
        [Range(0, 5)]
        [Required]
        public int StrengthMod { get; set; }
        [Display(Name = "SPD")]
        [Range(0, 5)]
        [Required]
        public int SpeedMod { get; set; }
        [Display(Name = "VIT")]
        [Range(0, 5)]
        [Required]
        public int ConstitutionMod { get; set; }
        [Display(Name = "DEX")]
        [Range(0, 5)]
        [Required]
        public int DexterityMod { get; set; }
        [Display(Name = "CON")]
        [Range(0, 5)]
        [Required]
        public int ConcentrationMod { get; set; }
        [Display(Name = "MVN")]
        [Range(0, 5)]
        [Required]
        public int MotivationMod { get; set; }

        [Display(Name = "Class Details")]
        public string Story { get; set; }
        [Display(Name = "Class Background")]
        public string Background { get; set; }
        [Display(Name = "Class Strengths")]
        public string Strengths { get; set; }
        [Display(Name = "Class Weaknesses")]
        public string Weakness { get; set; }

        [Display(Name = "Available Skill Set")]
        public Skills[] SkillSet { get; set; }
    }

    /// <summary>
    /// Levels of skill for a certain set. Adds small boosts after some time.
    /// </summary>
	public enum SkillType
    {
        [Display(Name = "Basic")]
        Basic = 1,
        [Display(Name = "Intermediate")]
        Intermediate = 2,
        [Display(Name = "Advanced")]
        Advanced = 3,
        [Display(Name = "Elite")]
        Elite = 4,
        [Display(Name = "Legendary")]
        Legendary = 5
    }

    /// <summary>
    /// The different class types, each with their own set of stat boosts.
    /// </summary>
	public enum ClassType
    {
        [Display(Name = "Hobbyist")]
        Hobbyist = 1,
        [Display(Name = "Runner")]
        Runner = 2,
        [Display(Name = "Powerlifter")]
        Powerlifter = 3,
        [Display(Name = "Bodybuilder")]
        Bodybuilder = 4,
        [Display(Name = "Crossfitter")]
        Crossfit = 5,
        [Display(Name = "Olympian")]
        Olympian = 6,
        [Display(Name = "Fighter")]
        Fighter = 7,
        [Display(Name = "Dancer")]
        Dancer = 8,
        [Display(Name = "Yogi")]
        Yoga = 9
    }
}