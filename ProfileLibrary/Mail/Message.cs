using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
// using SwoldierCore.Data.Profile;

namespace ProfileLibrary.DataModels.Mail
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
        // public Message(string messageText = null,ProfileBase sender = null, ProfileBase receiver = null)
        // {
        //     ConversationId = new Guid();
        //     MessageId = new Guid();
        //     Unread = true;
        //     MessageText = messageText ?? throw new ArgumentNullException(nameof(messageText));
        //     Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        //     Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        //     MessageDate = new DateTime();
        // }

        public int Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid MessageId { get; set; }
        public bool Unread { get; set; }
        public string MessageText { get; set; }
        // public ProfileBase Sender { get; set; }
        // public ProfileBase Receiver { get; set; }
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
