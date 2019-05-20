using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// The main Campaign class. Used to designate a campaign.
    /// </summary>
    public class Campaign
    {
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

        // The list of all workouts associated with the campaign
        public List<WorkoutProgram> CampaignPrograms { get; set; }

        // Get the list of appropriate quests for each campaign
        public List<Quest> CampaignQuest { get; set; }

        // Wait, we're adding bosses now!?
        // That Boss is gonna need some MINIONS
        //public BossEncounter Boss { get; set; }
        //public PersonalEncounter[] Minions { get; set; }
    }
}
