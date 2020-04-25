namespace OCReLibrary
{
    /// <summary>
    /// An ability used by someone's character
    /// </summary>
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
    }
}
