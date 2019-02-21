using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SocialModels;
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


        private OCFXUser MailboxOwner { get; set; }
        public IOrderedQueryable<Shout> MailReceived { get; private set; }
        public IOrderedQueryable<Shout> MailSent { get; private set; }
        public IQueryable<Shout> UnreadMail { get; private set; }

        public async void OnGetAsync()
        {
            // Get the user
            MailboxOwner = await _userManager.GetUserAsync(User);

            // Get latest received messages
            MailReceived = _context.Messages.Where(u => u.ReceiverId == MailboxOwner.ProfileId).OrderByDescending(d => d.DateSent);

            // Get latest sent messages
            MailSent = _context.Messages.Where(u => u.SenderId == MailboxOwner.ProfileId).OrderByDescending(d => d.DateSent);

            // Get unread mail
            UnreadMail = _context.Messages.Where(u => u.Status == Shout.MessageStatus.Unread);
        }
    }
}