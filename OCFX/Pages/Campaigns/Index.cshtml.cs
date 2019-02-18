using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Pages.Campaigns
{
	public class IndexModel : PageModel
    {
		public string Message { get; set; }
		private readonly OCFXContext _context;

		public IndexModel(OCFXContext context)
		{
			_context = context;
		}

		public List<Campaign> Campaigns { get; set; }

		public void OnGet()
        {
			Message = "Get ready for the next challenge!";

			Campaigns = _context.Campaigns
				.Include(c => c.CampaignDiet)
				.Include(c => c.CampaignQuest)
				.ToList();
        }
    }
}