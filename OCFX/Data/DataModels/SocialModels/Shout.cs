﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Data.DataModels.SocialModels
{
    public class Shout
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string ChainIdentifier { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateOpened { get; set; }
        public MessageStatus Status { get; set; }
        public string SubjectText { get; set; }

        public enum MessageStatus
        {
            Unread = 1,
            Opened = 2,
            Archived = 3
        }

        private string IdentityMaker() => Guid.NewGuid() + "-" + Id;
    }
}
