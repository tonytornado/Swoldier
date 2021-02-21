namespace CoreLibrary.Models
{
    public class History
    {
        public int Id { get; set; }
        public int HistoryTypeId { get; set; }
        public string EditDescription { get; set; }

        public override string ToString() => $"[{HistoryTypeId}] {EditDescription}";
    }
}
