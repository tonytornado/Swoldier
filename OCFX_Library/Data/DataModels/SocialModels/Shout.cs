using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// A message item.
    /// </summary>
    public class Shout
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Unique Message Identifier")]
        public Guid Identifier { get; set; }
        [Display(Name = "Mail Chain Identifier")]
        public string ChainIdentifier { get; set; }
        [Display(Name = "Sender Id")]
        public int SenderId { get; set; }
        [Display(Name = "Receiver Id")]
        public int ReceiverId { get; set; }
        [Display(Name = "Subject")]
        public string SubjectText { get; set; }
        [Display(Name = "Message Text")]
        public string MessageText { get; set; }
        [Display(Name = "Sent")]
        public DateTime DateSent { get; set; }
        [Display(Name = "Opened")]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Message Status")]
        public MessageStatus Status { get; set; }

        [ForeignKey("SenderId")]
        public virtual Profile Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual Profile Receiver { get; set; }

        public enum MessageStatus
        {
            Unread = 1,
            Opened = 2,
            Archived = 3
        }
    }
}
