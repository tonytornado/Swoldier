namespace FitLibrary.Models.Community
{
    public class ClubPosts
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public int ProfileId { get; set; }
        //public int MyProperty { get; set; }
    }
}
