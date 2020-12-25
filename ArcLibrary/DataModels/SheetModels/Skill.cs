namespace ArcLibrary.Data.DataModels
{
    /// <summary>
    /// Skill class.
    /// Universal across builds and disciplines
    /// </summary>
    public class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Lvl { get; set; }

    }
}