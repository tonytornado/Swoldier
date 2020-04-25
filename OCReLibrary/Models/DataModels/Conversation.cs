using Microsoft.AspNetCore.Http.Internal;
using System;

namespace OCReLibrary
{
    public class Conversation
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public SentMessage SentMessage { get; set; }
        public ReceivedMessage ReceivedMessage { get; set; }
    }

    public class Message
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public FormFile Attachment { get; set; }
        public DateTime Date { get; set; }
    }

    public class SentMessage : Message
    {
        public int SenderId { get; set; }
        public ProfileSheet Sender { get; set; }

    }

    public class ReceivedMessage : Message
    {
        public int ReceiverId { get; set; }
        public ProfileSheet Receiver { get; set; }
    }
}