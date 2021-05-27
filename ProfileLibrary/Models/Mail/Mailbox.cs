using System;
using System.Collections.Generic;
// using SwoldierCore.Data.Profile;

namespace SocialLibrary.DataModels.Mail
{
    public class Mailbox
    {
        public Mailbox()
        {
        }

        public Mailbox(List<Message> messages, List<Message> unreadMessages)
        {
            MailboxId = new Guid();
            Messages = messages ?? new List<Message>();
            UnreadMessages = unreadMessages ?? new List<Message>();
            Deleted = 0;
        }

        public Guid MailboxId { get; set; }
        public List<Message> Messages { get; set; }
        public List<Message> UnreadMessages { get; set; }
        public int Deleted { get; set; }
    }
}
