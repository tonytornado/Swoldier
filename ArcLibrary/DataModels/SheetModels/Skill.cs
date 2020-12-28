using System;

namespace ArcLibrary.Data.DataModels
{
    /// <summary>
    /// Skill class.
    /// Universal across builds and disciplines
    /// </summary>
    public class Skill
    {
        public Skill()
        {
        }

        public Skill(string name, string tagline, string description, int cost, int lvl)
        {
            Name = name;
            Tagline = tagline;
            Description = description;
            Cost = cost;
            Lvl = lvl;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Lvl { get; set; }

        public override string ToString() => $"Lvl {Lvl} {Name} [{Tagline}]";
    }
}