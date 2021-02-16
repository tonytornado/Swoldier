using System.Collections.Generic;
using ArcLibrary.DataModels.CharacterModels;

namespace ArcLibrary.DataModels.CharacterModels
{
    public class Mod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StrMod { get; set; }
        public int DexMod { get; set; }
        public int ConMod { get; set; }
        public int IntMod { get; set; }
        public int MotMod { get; set; }
        public int ChaMod { get; set; }
    }

    public class MorphBuild : Mod
    {
        public string Description { get; set; }
        public Skill AddedSkill { get; set; }

        public override string ToString() => $"{Name}";
    }

    public class ClassBuild : Mod
    {
        public string Backstory { get; set; }
        public Skill AddedSkill { get; set; }
        public List<Trait> Traits { get; set; }

        public override string ToString() => $"{Name}";
    }

    public class Accessory : Mod
    {
        public string Description { get; set; }
        public Skill AddedSkill { get; set; }
    }
}