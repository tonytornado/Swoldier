using System.Collections.Generic;

namespace OCReLibrary
{
    /// <summary>
    /// A character created by a user to link to their profile
    /// </summary>
    public class CharacterSheet
    {
        /* Basics */
        public int Id { get; set; }
        public string Name { get; set; }
        public string Backstory { get; set; }

        /* Stats */
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Flexibility { get; set; }
        public int Constitution { get; set; }
        public int Speed { get; set; }
        /// <summary>
        /// Size of the character
        /// </summary>
        public FrameSize Frame { get; set; }
        public List<Skill> SkillSet => new List<Skill>(3);
    }
}
