using System.Collections.Generic;
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
        public ICollection<Skill> SkillSet { get; set; }
    }
}