using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// Character archetypes. This provides boosts and such.
    /// </summary>
    public class Archetype
    {
        /// <summary>
        /// Simple implementation
        /// </summary>
        public Archetype()
        {
        }

        /// <summary>
        /// Six aspect implementation
        /// </summary>
        /// <param name="fitType"></param>
        /// <param name="strengthMod"></param>
        /// <param name="speedMod"></param>
        /// <param name="constitutionMod"></param>
        /// <param name="dexterityMod"></param>
        /// <param name="concentrationMod"></param>
        /// <param name="motivationMod"></param>
        public Archetype(ClassType fitType,
                         int strengthMod,
                         int speedMod,
                         int constitutionMod,
                         int dexterityMod,
                         int concentrationMod,
                         int motivationMod)
        {
            FitType = fitType;
            StrengthMod = strengthMod;
            SpeedMod = speedMod;
            ConstitutionMod = constitutionMod;
            DexterityMod = dexterityMod;
            ConcentrationMod = concentrationMod;
            MotivationMod = motivationMod;
            SkillMod = SkillType.Basic;
        }

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
        public Skill[] SkillSet { get; set; }
    }

    /// <summary>
    /// Levels of skill for a certain set. Adds small boosts after some time.
    /// </summary>
	public enum SkillType
    {
        [Display(Name = "Basic")]
        Basic,
        [Display(Name = "Intermediate")]
        Intermediate,
        [Display(Name = "Advanced")]
        Advanced,
        [Display(Name = "Elite")]
        Elite,
        [Display(Name = "Legendary")]
        Legendary
    }

    /// <summary>
    /// The different class types, each with their own set of stat boosts.
    /// </summary>
	public enum ClassType
    {
        [Display(Name = "Hobbyist")]
        Hobbyist,
        [Display(Name = "Runner")]
        Runner,
        [Display(Name = "Powerlifter")]
        Powerlifter,
        [Display(Name = "Bodybuilder")]
        Bodybuilder,
        [Display(Name = "Crossfitter")]
        Crossfit,
        [Display(Name = "Olympian")]
        Olympian,
        [Display(Name = "Fighter")]
        Fighter,
        [Display(Name = "Dancer")]
        Dancer,
        [Display(Name = "Yogi")]
        Yoga
    }
}