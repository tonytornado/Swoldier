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

namespace OCFX.Pages.Clubs.MessageBoard
{
    public class BoardMessageModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public BoardMessageModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public OCFXUser BoardViewingMember { get; private set; }
        public MessageBoardPost InitialBoardPost { get; private set; }
        public List<MessageBoardComment> InitialBoardComments { get; private set; }

        [BindProperty]
        public string Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int BoardId, int MessageId)
        {
            BoardViewingMember = await _userManager.GetUserAsync(User);

            InitialBoardPost = await _context.MessageBoardPosts
                .Include(b => b.Board)
                .Include(p => p.Profile)
                    .ThenInclude(c => c.Photos)
                .Include(c => c.MessageBoardComments)
                    .ThenInclude(c => c.Profile)
                        .ThenInclude(c => c.Photos)
                .SingleOrDefaultAsync(c =>
            c.Board.Id == BoardId
            && c.Id == MessageId);

            InitialBoardComments = InitialBoardPost.MessageBoardComments.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostCommentAsync(int BoardId, int MessageId)
        {
            if (ModelState.IsValid)
            {

                BoardViewingMember = await _userManager.GetUserAsync(User);

                MessageBoardComment BoardPost = new MessageBoardComment
                {
                    BoardId = BoardId,
                    BoardPostId = MessageId,
                    ProfileId = BoardViewingMember.ProfileId,
                    Text = Input,
                    DatePosted = DateTime.Now

                };

                _context.MessageBoardComments.Add(BoardPost);
                _context.SaveChanges();
                StatusMessage = "Reply posted.";
            }
            else
            {
                StatusMessage = "ERROR: Reply not posted. Something has goofed up.";
            }

            return RedirectToPage(new { BoardId, MessageId });
        }
    }
}