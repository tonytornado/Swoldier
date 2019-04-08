using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Dashboard.Messaging
{
    [Authorize]
    public class MessengerModel : PageModel
    {
        private readonly UserManager<OCFXUser> _userManager;
        private readonly OCFXContext _context;

        public MessengerModel(UserManager<OCFXUser> userManager, OCFXContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public OCFXUser MailboxOwner { get; private set; }
        public List<Shout> Chain { get; private set; }
        public List<Shout> MailReceived { get; private set; }
        public List<Shout> ArchivedMailReceived { get; private set; }
        public List<Shout> MailSent { get; private set; }
        public List<Shout> UnreadMail { get; private set; }

        public async Task OnGetAsync()
        {
            // Get the user
            MailboxOwner = await _userManager.GetUserAsync(User);

            // Get latest received messages
            MailReceived = await _context.Messages.Include(p => p.Sender)
                .Where(u => u.ReceiverId == MailboxOwner.ProfileId)
                .Where(u => u.Status != Shout.MessageStatus.Archived)
                .OrderByDescending(d => d.DateSent)
                .ToListAsync();

            ArchivedMailReceived = await _context.Messages.Include(p => p.Sender)
                .Where(u => u.ReceiverId == MailboxOwner.ProfileId)
                .Where(u => u.Status == Shout.MessageStatus.Archived)
                .OrderByDescending(d => d.DateSent)
                .ToListAsync();

            // Get latest sent messages
            MailSent = await _context.Messages
                .Include(p => p.Receiver)
                .Where(u => u.SenderId == MailboxOwner.ProfileId)
                .OrderByDescending(d => d.DateSent)
                .ToListAsync();

            // Get unread mail
            UnreadMail = await _context.Messages
                .Where(u => u.Status == Shout.MessageStatus.Unread)
                .ToListAsync();
        }
    }
}