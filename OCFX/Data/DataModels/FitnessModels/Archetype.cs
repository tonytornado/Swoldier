using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Archetype
    {
        [Key]
		[Display(Name = "Character Class")]
        public int Id { get; set; }
		[Display(Name = "Skill Level")]
		public SkillType SkillMod { get; set; }
		[Display(Name = "Fit Type")]
        public ClassType FitType { get; set; }

		// Modifications to the base stats of the character, builds over each level up.
		[Display(Name = "STR")]
		[Range(1, 5)]
		public int StrengthMod { get; set; }
		[Display(Name = "SPD")]
		[Range(1, 5)]
		public int SpeedMod { get; set; }
		[Display(Name = "VIT")]
		[Range(1, 5)]
		public int ConstitutionMod { get; set; }
		[Display(Name = "DEX")]
		[Range(1, 5)]
		public int DexterityMod { get; set; }
		[Display(Name = "CON")]
		[Range(1, 5)]
		public int ConcentrationMod { get; set; }
		[Display(Name = "MVN")]
		[Range(1, 5)]
		public int MotivationMod { get; set; }
    }

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