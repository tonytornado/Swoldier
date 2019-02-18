using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OCFX.Data.DataRepo;
using OCFX.Data.DataModels.SiteModels;
using OCFX.Areas.Identity.Data;

namespace OCFX.Pages.FAQ
{
	public class IndexModel : PageModel
    {
		private readonly OCFXContext context;

		public IndexModel(OCFXContext _context)
		{
			context = _context;
		}

		public string Message { get; set; }
		public HashSet<Facts> Faq;
		public List<IGrouping<SectionName, Facts>> FaqCategory { get; private set; }

		public void OnGet()
        {
			Message = "The most frequently asked/annoying questions asked.";

			Faq = context.FAQs.ToHashSet();
			FaqCategory = context.FAQs.GroupBy(x => x.Section).ToList();
        }
    }
}