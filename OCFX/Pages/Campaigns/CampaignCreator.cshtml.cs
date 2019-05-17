using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Campaigns
{
    public class InputModel
    {
        public string Story { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public BossEncounter BigBad { get; internal set; }
    }

    public class CampaignCreatorModel : PageModel
    {
        private readonly OCFXContext context;
        private List<BossEncounter> bossList;

        public CampaignCreatorModel(OCFXContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public InputModel Creator { get; set; }

        

        public void OnGet()
        {
            bossList = context.Bosses.ToList();
        }

        public void OnPost()
        {
            Campaign customCampaign = new Campaign()
            {
                CampaignLore = Creator.Story,
                CampaignName = Creator.Name,
                CampaignDetails = Creator.Details,
                Boss = Creator.BigBad
            };
            
            context.Add(customCampaign);
            context.SaveChanges();
        }
    }

    
}