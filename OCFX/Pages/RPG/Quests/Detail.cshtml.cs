using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Pages.Campaigns.Quests
{
    public class DetailModel : PageModel
    {
        private readonly OCFXContext _context;
        private readonly UserManager<OCFXUser> _userManager;

        public DetailModel(OCFXContext context, UserManager<OCFXUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Quest Quest { get; private set; }
        public QuestLog QuestBlocker { get; private set; }
        public List<int> Completed { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            Quest = _context.Quests.SingleOrDefault(q => q.Id == id);
            QuestBlocker = _context.QuestLogs.SingleOrDefault(q => q.Quest.Id == id && q.Completed == false);
            Completed = QuestMethods.CheckCompletedQuests(_context, user.ProfileId);
        }

        /// <summary>
        /// Accepts a quest.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAcceptQuestAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            QuestMethods.JoinQuest(_context, id, user.ProfileId);
            StatusMessage = "Quest Accepted!";
            return RedirectToPage(new { id });
        }

        /// <summary>
        /// Completes a quest.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostCompleteQuestAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            try
            {
                QuestMethods.CompleteQuest(_context, id, user.ProfileId);
            }
            catch (Exception e)
            {
                StatusMessage = e.Message;
                return RedirectToPage(new { id });
            }

            StatusMessage = "Quest Completed!";
            return RedirectToPage(new { id });
        }
    }
}