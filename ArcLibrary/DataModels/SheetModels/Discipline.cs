using System.Collections.Generic;

namespace ArcLibrary.Data.DataModels
{
    public class Discipline
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int STRmod { get; set; }
        public int DEXmod { get; set; }
        public int CONmod { get; set; }
        public int INTmod { get; set; }
        public int MOTmod { get; set; }
        public int CHAmod { get; set; }
        
        public Skill AddedSkill { get; set; }
    }

    public class MorphBuild : Discipline
    {
        public string Description { get; set; }
    }

    public class ClassBuild : Discipline
    {
        public string Backstory { get; set; }
        public List<Trait> Traits { get; set; }
    }
}