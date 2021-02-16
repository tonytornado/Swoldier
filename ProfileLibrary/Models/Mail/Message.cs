using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SocialLibrary.Profile;
// using SwoldierCore.Data.Profile;

namespace SocialLibrary.DataModels.Mail
{
    public class Message
    {
        public Message()
        {
        }

        /// <summary>
        /// New messages only
        /// </summary>
        /// <param name="messageText">Message Text (in HTML/Markdown)</param>
        /// <param name="sender">Sender's Profile</param>
        /// <param name="receiver">Receiver's Profile</param>
        public Message(string messageText = null, ProfileData sender = null, ProfileData receiver = null)
        {
            ConversationId = new Guid();
            MessageId = new Guid();
            Unread = true;
            MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
            MessageDate = new DateTime();
        }

        [Key]
        public int Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid MessageId { get; set; }
        public bool Unread { get; set; }
        public string MessageText { get; set; }
        [ForeignKey("SenderId")]
        public ProfileData Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public ProfileData Receiver { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageExcerpt => GetMessageExcerpt(MessageText);
        // public IFormFile Attachment { get; set; }

        /// <summary>
        /// Returns a small excerpt of the first ten words
        /// </summary>
        /// <param name="text">The message text</param>
        private static string GetMessageExcerpt(string text)
        {
            return text ?? "";
        }
    }
}
