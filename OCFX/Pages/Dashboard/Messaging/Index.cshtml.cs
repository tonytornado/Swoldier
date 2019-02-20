using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SocialModels;
using OCFX.DataModels;

namespace OCFX.Pages.Dashboard.Messaging
{
    public class MessengerModel : PageModel
    {
        private readonly UserManager<OCFXUser> _userManager;
        private readonly OCFXContext _context;

        public MessengerModel(UserManager<OCFXUser> userManager, OCFXContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private OCFXUser MailboxOwner { get; set; }
        public List<Shout> MailReceived { get; private set; }
        public List<Shout> MailSent { get; private set; }
        public List<Shout> UnreadMail { get; private set; }

        public async void OnGetAsync()
        {
            // Get the user
            MailboxOwner = await _userManager.GetUserAsync(User);

            // Get latest received messages
            MailReceived = _context.Messages.OrderByDescending(d => d.DateSent).Where(u => u.ReceiverId == MailboxOwner.ProfileId).ToList();

            // Get latest sent messages
            MailSent = _context.Messages.OrderByDescending(d => d.DateSent).Where(u => u.SenderId == MailboxOwner.ProfileId).ToList();

            // Get unread mail
            UnreadMail = _context.Messages.Where(u => u.Status == Shout.MessageStatus.Unread).ToList();
        }
    }
}