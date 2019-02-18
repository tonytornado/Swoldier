﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.Data.Methods;
using OCFX.DataModels;

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
        public string StatusMessage { get; private set; }

        public async Task OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            Quest = await _context.Quests.SingleOrDefaultAsync(q => q.Id == id);
            QuestBlocker = await _context.QuestLogs.SingleOrDefaultAsync(q => q.QuestId == id && q.Completed == false) ?? new QuestLog { };
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
            return RedirectToPage("Detail", "OnGetAsync", new { id });
        }
    }
}