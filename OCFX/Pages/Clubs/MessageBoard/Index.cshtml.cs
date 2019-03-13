using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public InputModel Input { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BoardViewingMember = await _userManager.GetUserAsync(User);

            BoardPosts = await _context.MessageBoardPosts.Include(c => c.MessageBoardComments).Where(c => c.Board.Id == id).ToListAsync();

            return Page();
        }

        /// <summary>
        /// Adds a post onto the message board
        /// </summary>
        /// <param name="id">Gym Id</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostTopicAsync(int id)
        {
            BoardViewingMember = await _userManager.GetUserAsync(User);

            MessageBoardPost BoardPost = new MessageBoardPost
            {
                BoardId = id,
                ProfileId = BoardViewingMember.ProfileId,
                Title = Input.Title,
                Text = Input.Text,
                DatePosted = DateTime.Now
                
            };

            _context.MessageBoardPosts.Add(BoardPost);

            return RedirectToPage("MessageBoard", new { id });
        }

        /// <summary>
        /// The Input Model associated with posting topics and such.
        /// </summary>
        public class InputModel
        {
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Display(Name = "Text")]
            public string Text { get; set; }
        }
    }
}