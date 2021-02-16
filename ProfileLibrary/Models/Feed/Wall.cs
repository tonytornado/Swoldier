using System.Collections.Generic;
using SocialLibrary.Profile;

namespace SocialLibrary.Feed
{
    public class NewsFeed
    {
        public static List<Post> GetAllPosts()
        {
            List<Post> ContentFeed = new List<Post>();
            return ContentFeed;
        }
    }

    public class Wall
    {
        public int WallId { get; set; }
        public List<Post> Posts { get; set; }

        public ProfileData Profile { get; set; }
    }
}