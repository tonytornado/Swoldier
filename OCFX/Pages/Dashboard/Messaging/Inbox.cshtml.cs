using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public Shout IMessage { get; private set; }
        public List<Shout> MailReceived { get; private set; }

        [BindProperty]
        public string ReplyMessage { get; set; }

        public async Task OnGetAsync(int MessageId, string ChainId)
        {
            // User's mailbox
            MailboxOwner = await _userManager.GetUserAsync(User);
            MailboxProfile = MailboxOwner.Profile;
            IMessage = _context.Messages.SingleOrDefault(m => m.Id == MessageId);

            if(IMessage.Status == Shout.MessageStatus.Unread)
            {
                IMessage.Status = Shout.MessageStatus.Opened;
                IMessage.DateOpened = DateTime.Now;
                _context.SaveChanges();
            }

            // User's mail chain
            MailReceived = _context.Messages.Include(p => p.Sender).OrderByDescending(d => d.DateSent).Where(u => u.ReceiverId == MailboxOwner.ProfileId || u.ChainIdentifier == ChainId).ToList();
        }

        public async Task<IActionResult> OnPostReplyAsync(int MessageId)
        {
            MailboxOwner = await _userManager.GetUserAsync(User);
            IMessage = _context.Messages.SingleOrDefault(m => m.Id == MessageId);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var reply = new Shout()
            {
                Identifier = Guid.NewGuid(),
                ChainIdentifier = IMessage.ChainIdentifier,
                SenderId = MailboxOwner.ProfileId,
                ReceiverId = IMessage.SenderId,
                SubjectText = ">>: " + IMessage.SubjectText,
                MessageText = ReplyMessage,
                DateOpened = null,
                DateSent = DateTime.Now,
                Status = Shout.MessageStatus.Unread
            };

            _context.Messages.Add(reply);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}