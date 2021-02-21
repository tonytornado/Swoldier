namespace ArcLibrary.Models.LoreModels
{
    public class LoreType
    {
        public int LoreTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{LoreTypeId} - {Name}";
    }
}