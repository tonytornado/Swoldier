using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// A message board object
    /// </summary>
    public class MessageBoard
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Board Post Content")]
        public string Text { get; set; }
        [Display(Name = "Board Post Date")]
        public DateTime DatePosted { get; set; }

        [Display(Name = "Profile")]
        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public ProfileSheet Profile { get; set; }

        [Display(Name = "Club Message Board")]
        public int BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Gym Board { get; set; }

    }

    /// <summary>
    /// A message board post for a gym/club
    /// </summary>
    public class MessageBoardPost : MessageBoard
    {
        [Display(Name = "Board Post Title")]
        public string Title { get; set; }

        public List<MessageBoardComment> MessageBoardComments { get; set; }
    }

    /// <summary>
    /// A message board comment for a post.
    /// </summary>
    public class MessageBoardComment : MessageBoard
    {
        [Display(Name = "Club Message Board Post ID")]
        public int BoardPostId { get; set; }
        [ForeignKey("BoardPostId")]
        public MessageBoardPost BoardPost { get; set; }
    }
}
