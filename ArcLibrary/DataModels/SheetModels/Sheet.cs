using System;
using System.Collections.Generic;
using System.Text;

namespace ArcLibrary.Data.DataModels
{
    public class SheetBase
    {
        public int ID { get; set; }
        public Guid ProfileId { get; set; }

        // Aspects
        public int STR { get; set; }
        public int DEX { get; set; }
        public int CON { get; set; }
        public int INT { get; set; }
        public int MOT { get; set; }
        public int CHA { get; set; }

        // Character Skills and Traits
        public ClassBuild FitClass { get; set; }
        public MorphBuild FitBuild { get; set; }
        public List<Skill> SkillSet { get; set; }
    }
}
