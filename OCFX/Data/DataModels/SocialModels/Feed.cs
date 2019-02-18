using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OCFX.DataModels.SocialModels
{
	public class FeedElement
	{
		[Key]
		[Display(Name = "ID")]
		public int Id { get; set; }
        [Display(Name = "Post ID")]
        public int EntryId { get; set; }
        [Display(Name = "Post Content")]
		public string Text { get; set; }
		[Display(Name = "Post Date")]
		public DateTime DatePosted { get; set; }

        [Display(Name = "Profile")]
		public int ProfileId { get; set; }
		public Profile Profile { get; set; }
	}

	public class Post : FeedElement
	{
		public List<Comment> Comments { get; set; }
	}

	public class Comment : FeedElement
	{
        [Display(Name = "Post ID")]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public List<Reply> Replies { get; set; }
    }

	public class Reply : FeedElement
	{
        [Display(Name = "Comment ID")]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
    }
}
