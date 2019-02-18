using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
	// Campaigns have several players, workouts and quests.
	public class Campaign
    {
        [Key]
		[Display(Name = "Campaign")]
        public int Id { get; set; }
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }
		[Display(Name = "Campaign Tagline")]
		public string CampaignDetails { get; set; }
		[Display(Name = "Campaign Story")]
		public string CampaignLore { get; set; }
		[Display(Name = "Campaign Risks")]
		public RiskLevel CampaignRisk { get; set; }

		[Display(Name = "Associated Diet")]
        public int DietId { get; set; }
		[ForeignKey("DietId")]
        public Diet CampaignDiet { get; set; }

		// Get the list of appropriate quests for each campaign
		public List<Quest> CampaignQuest { get; set; }
	}
}
