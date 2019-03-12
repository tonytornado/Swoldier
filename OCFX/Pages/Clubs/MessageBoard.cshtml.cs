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

namespace OCFX.Pages.Clubs
{
    public class MessageBoardModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public MessageBoardModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public OCFXUser BoardViewingMember { get; private set; }
        public List<MessageBoardPost> BoardPosts { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BoardViewingMember = await _userManager.GetUserAsync(User);

            BoardPosts = await _context.MessageBoardPosts.Include(c => c.MessageBoardComments).Where(c => c.Board.Id == id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCommentAsync(int id)
        {
            BoardViewingMember = await _userManager.GetUserAsync(User);

            return RedirectToPage("MessageBoard", new { id });
        }
    }
}