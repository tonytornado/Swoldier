using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public ProfileSheet MailboxProfile { get; private set; }
        public Shout IMessage { get; private set; }
        public List<Shout> MailReceived { get; private set; }

        [BindProperty]
        public string ReplyMessage { get; set; }
        public List<Shout> IMessageChain { get; private set; }

        public async Task OnGetAsync(int MessageId, string ChainId)
        {
            // User's mailbox
            MailboxOwner = await _userManager.GetUserAsync(User);
            MailboxProfile = MailboxOwner.Profile;
            IMessage = _context.Messages
                .SingleOrDefault(m => m.Id == MessageId);

            if (IMessage.Status == Shout.MessageStatus.Unread)
            {
                IMessage.Status = Shout.MessageStatus.Opened;
                IMessage.DateOpened = DateTime.Now;
                _context.SaveChanges();
            }

            // User's mail chain
            MailReceived = _context.Messages
                .Include(p => p.Sender)
                .OrderByDescending(d => d.DateSent)
                .Where(u => u.ChainIdentifier == ChainId)
                .ToList();
        }

        /// <summary>
        /// Send a reply
        /// </summary>
        /// <param name="MessageId"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostReplyAsync(int MessageId)
        {
            MailboxOwner = await _userManager.GetUserAsync(User);
            IMessage = _context.Messages
                .SingleOrDefault(m => m.Id == MessageId);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Shout reply = new Shout()
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

        public async Task OnPostDeleteMessageAsync(int MessageId)
        {
            // Get the message
            IMessage = await _context.Messages
                .SingleOrDefaultAsync(u => u.Id == MessageId);

            IMessage.Status = Shout.MessageStatus.Archived;
            _context.SaveChanges();

            RedirectToPage("./Index");
        }

        /// <summary>
        /// Removes the full mail chain for a user
        /// </summary>
        /// <param name="ChainId"></param>
        /// <returns></returns>
        public async Task OnPostArchiveAsync(string ChainId)
        {
            // Get the whole conversation chain
            IMessageChain = await _context.Messages
                .Where(u => u.ChainIdentifier == ChainId)
                .ToListAsync();

            foreach (Shout item in IMessageChain)
            {
                item.Status = Shout.MessageStatus.Archived;
            }
            _context.SaveChanges();

            RedirectToPage("./Index");
        }
    }
}