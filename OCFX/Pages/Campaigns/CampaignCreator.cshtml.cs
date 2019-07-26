using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX.Pages.Campaigns
{
    public class InputModel
    {
        public string Story { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public BossEncounter BigBad { get; set; }
        public List<Quest> Quests { get; set; }
        public int TypeChecker { get; set; }
    }

    public class CampaignCreatorModel : PageModel
    {
        private readonly OCFXContext context;

        public CampaignCreatorModel(OCFXContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public InputModel Creator { get; set; }
        public SelectList BossSelection { get; private set; }
        public List<PersonalEncounter> MinionSelection { get; private set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
            List<BossEncounter> BossList = context.Bosses.ToList();
            List<PersonalEncounter> MinionList = context.Minions.ToList();
            BossSelection = new SelectList(BossList, "Id", "Name");
            MinionSelection = MinionList;
        }

        public void OnPost()
        {
            if (ModelState.IsValid == false)
            {
                StatusMessage = "Something's wrong. Fix it!";
                RedirectToAction("Get");
            }

            var customCampaign = new Campaign()
            {
                Lore = Creator.Story,
                Name = Creator.Name,
                Details = Creator.Details,
                Quests = Creator.Quests,
                //Boss = Creator.BigBad
            };

            context.Add(customCampaign);
            context.SaveChanges();

            RedirectToPage("Campaign", new { customCampaign.Id });
        }
    }
}