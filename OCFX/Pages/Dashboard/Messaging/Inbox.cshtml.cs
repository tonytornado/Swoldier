using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SocialModels;
using OCFX.DataModels;

namespace OCFX.Pages.Dashboard.Messaging
{
    public class InboxModel : PageModel
    {
        private readonly UserManager<OCFXUser> _userManager;
        private readonly OCFXContext _context;

        public InboxModel(UserManager<OCFXUser> userManager, OCFXContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public OCFXUser MailboxOwner { get; private set; }
        public Profile MailboxProfile { get; private set; }
        public int MessageIdentifier { get; private set; }
        public Shout IMessage { get; private set; }
        public List<Shout> MailReceived { get; private set; }

        // Reply Strings
        public string ReplySubject { get; private set; }
        public string ReplyMessage { get; private set; }
        public int ReplyReceiver { get; private set; }

        public async Task OnGetAsync(int MessageId, int ChainId)
        {
            // User's mailbox
            MailboxOwner = await _userManager.GetUserAsync(User);
            MailboxProfile = MailboxOwner.Profile;
            IMessage = _context.Messages.SingleOrDefault(m => m.Id == MessageId);

            // User's mail chain
            MailReceived = _context.Messages.OrderByDescending(d => d.DateSent).Where(u => u.ReceiverId == MailboxOwner.ProfileId || u.ChainIdentifier == ChainId).ToList();

            Shout Messenger = MailReceived.FirstOrDefault();
            ReplyReceiver = Messenger.SenderId;
        }



        public IActionResult OnPostReply(int MessageId, int ChainId)
        {
            Shout Reply = new Shout()
            {
                DateOpened = DateTime.Now,
                DateSent = DateTime.Now,
                ChainIdentifier = ChainId,
                Identifier = Guid.NewGuid(),
                SenderId = MailboxOwner.ProfileId,
                ReceiverId = ReplyReceiver,
                SubjectText = ">>: " + ReplySubject,
                MessageText = ReplyMessage,
                Status = Shout.MessageStatus.Unread
            };

            _context.Messages.Add(Reply);
            _context.SaveChanges();

            return RedirectToPage("/Messaging/Inbox", "OnGetAsync", new { MessageId, ChainId });
        }
    }
}