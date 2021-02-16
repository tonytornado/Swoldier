using SocialLibrary.Profile;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArcLibrary.DataModels.CharacterModels
{
    public class Sheet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Backstory { get; set; }
        public string Bonds { get; set; }

        // Points
        public int Lp { get; set; }
        public int MaxLp { get; set; }
        public int O2 { get; set; }
        public int MaxO2 { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        // Aspects
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Int { get; set; }
        public int Mot { get; set; }
        public int Cha { get; set; }

        // Character Skills and Traits
        public ClassBuild FitClass { get; set; }
        public MorphBuild FitBuild { get; set; }

        // Equipment
        public List<Skill> SkillSet { get; set; }
        public List<Accessory> Equipment { get; set; }

        // Add it to a profile
        public int AssociatedProfileId { get; set; }
        [ForeignKey("AssociatedProfileId")]
        public ProfileData AssociatedProfile { get; set; }
    }
}