using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SocialLibrary.Feed
{

    public class PostBase
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Text { get; set; }
        public Boolean Edited { get; set; }

        [JsonIgnore]
        public Wall Wall { get; set; }
    }

    /// <summary>
    /// Wall Post Class
    /// </summary>
    public class Post : PostBase
    {
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }
    }

    /// <summary>
    /// Comment class for wall posts
    /// </summary>
    public class Comment : PostBase
    {
        public int CommentId { get; set; }
        public List<Reply> Replies { get; set; }
    }

    public class Reply : PostBase
    {
        public int ReplyId { get; set; }
    }
}
