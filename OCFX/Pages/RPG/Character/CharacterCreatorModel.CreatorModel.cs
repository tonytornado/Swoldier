namespace OCFX.Pages.RPG
{
    public partial class CharacterCreatorModel
    {
        public class CreatorModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int STR { get; set; }
            public int DEX { get; set; }
            public int CON { get; set; }
            public int VIT { get; set; }
            public int SPD { get; set; }
            public int MVN { get; set; }
            public string Backstory { get; set; }
            public string Drive { get; set; }
            public string Goal { get; set; }
            public int Class { get; set; }
            public int Primary { get; set; }
            public int Secondary { get; set; }
            public int Tertiary { get; set; }
        }
    }
}