using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX.Pages.Campaigns.Quests
{
    public class IndexModel : PageModel
    {
        private readonly OCFXContext _context;

        public IndexModel(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Quest> QuestList { get; private set; }

        public void OnGet()
        {
            QuestList = _context.Quests.ToList();
        }
    }
}