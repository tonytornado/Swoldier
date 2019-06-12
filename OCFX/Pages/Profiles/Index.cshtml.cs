using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Profiles
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public IndexModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public OCFXUser Player { get; set; }
        public Profile Profiler { get; set; }
        public List<Friend> Friender { get; set; }
        public List<Friend> Requests { get; set; }
        public List<Profile> RelatedFolkList { get; set; }

        public string userTitle;
        public double bodyFat;

        [BindProperty]
        public Post Entry { get; set; }
        [BindProperty]
        public MessageBoardComment PostNote { get; set; }
        [BindProperty]
        public Reply CommentNote { get; set; }
        [BindProperty]
        public string MessageContext { get; set; }
        [BindProperty]
        public string MessageSubject { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Displays the profile page
        /// </summary>
        /// <param name="id">Profile Id</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Gets the current user
            Player = await _userManager.GetUserAsync(User);

            // Loads the current id's associated profile
            Profiler = await ProfileMethods.GetProfileAsync(_context, id);

            if (Profiler == null)
            {
                return RedirectToPage("./Index");
            }

            // Loads the current id's friends and followers
            Friender = FriendlyMethods.GetFriendList(_context, Profiler.Id);
            Requests = await FriendlyMethods.GetFriendRequestsAsync(_context, Profiler.Id);

            // Loads any related users through their fitness profile and skill mods
            RelatedFolkList = await _context.Profiles.Where(p => p.FitStyle.FitType == Profiler.FitStyle.FitType).ToListAsync();

            // First and Last Name
            userTitle = Profiler.FirstName + " " + Profiler.LastName;

            return Page();
        }

        /// <summary>
        /// Adds a post onto someone's wall.
        /// </summary>
        /// <returns>Redirect</returns>
        public async Task<IActionResult> OnPostEntryFeedAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the user ID of who is posting
            Player = await _userManager.GetUserAsync(User);

            Post post = new Post()
            {
                ProfileId = Entry.ProfileId,
                EntryId = Player.ProfileId,
                Text = Entry.Text,
                DatePosted = DateTime.Now,
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToPage("./Index", new { Entry.ProfileId });
        }

        /// <summary>
        /// Comments on a post.
        /// </summary>
        /// <param name="PostId">The Post's id where the comment will be</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostEntryCommentAsync(int PostId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the user ID of who is posting
            Player = await _userManager.GetUserAsync(User);

            Comment comment = new Comment()
            {
                DatePosted = DateTime.Now,
                EntryId = PostNote.ProfileId,
                ProfileId = Player.ProfileId,
                PostId = PostId,
                Text = PostNote.Text,
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToPage("./Index", new { PostNote.ProfileId });
        }

        /// <summary>
        /// Replies to a comment
        /// </summary>
        /// <param name="CommentId">Comment Id</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostEntryReplyAsync(int CommentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the user ID of who is posting
            Player = await _userManager.GetUserAsync(User);

            var reply = new Reply()
            {
                DatePosted = DateTime.Now,
                EntryId = CommentNote.ProfileId,
                ProfileId = Player.ProfileId,
                CommentId = CommentId,
                Text = CommentNote.Text,
            };

            _context.Replies.Add(reply);
            _context.SaveChanges();

            return RedirectToPage("./Index", new { CommentNote.ProfileId });
        }


        /// <summary>
        /// Adds a new friend.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        public IActionResult OnGetFriending(int user, int friend)
        {
            FriendlyMethods.AddFriend(_context, user, friend);
            StatusMessage = "Friend request sent.";
            return RedirectToPage("/Dashboard/Index", new { friend });
        }

        public async Task<IActionResult> OnPostSendMailAsync(int id)
        {
            Player = await _userManager.GetUserAsync(User);
            Profiler = await ProfileMethods.GetProfileAsync(_context, id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mail = new Shout()
            {
                Identifier = Guid.NewGuid(),
                ChainIdentifier = Guid.NewGuid() + "" + Player.Id,
                DateSent = DateTime.Now,
                DateOpened = null,
                SenderId = Player.ProfileId,
                ReceiverId = Profiler.Id,
                SubjectText = MessageSubject,
                MessageText = MessageContext,
                Status = Shout.MessageStatus.Unread
            };

            _context.Messages.Add(mail);
            await _context.SaveChangesAsync();

            StatusMessage = "Message Sent";

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
