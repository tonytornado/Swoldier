using System.Collections.Generic;

namespace FitLibrary.Models.Community
{
    public class ClubBoard
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<ClubPosts> ForumPosts { get; set; }
    }
}
