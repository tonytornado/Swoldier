using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// A general feed element that can be used for posts and such
    /// </summary>
    public class PostElement
	{
		[Key]
		[Display(Name = "ID")]
		public int Id { get; set; }
        [Display(Name = "Post Content")]
		public string Text { get; set; }
		[Display(Name = "Post Date")]
		public DateTime DatePosted { get; set; }

        [Display(Name = "Profile")]
		public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
		public Profile Profile { get; set; }
    }
    /// <summary>
    /// A profile post 
    /// </summary>
	public class Post : PostElement
	{
        [Display(Name = "Entry")]
        public int EntryId { get; set; }
        [ForeignKey("EntryId")]
        public Profile Entry { get; set; }

        public List<Comment> Comments { get; set; }
	}

    /// <summary>
    /// A comment on a profile's post.
    /// </summary>
	public class Comment : PostElement
	{
        [Display(Name = "Entry")]
        public int EntryId { get; set; }
        [ForeignKey("EntryId")]
        public Profile Entry { get; set; }

        [Display(Name = "Post ID")]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public List<Reply> Replies { get; set; }
    }

    /// <summary>
    /// A reply to a profile post's comment
    /// </summary>
	public class Reply : PostElement
	{
        [Display(Name = "Entry")]
        public int EntryId { get; set; }
        [ForeignKey("EntryId")]
        public Profile Entry { get; set; }

        [Display(Name = "Comment ID")]
        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
    }
}
