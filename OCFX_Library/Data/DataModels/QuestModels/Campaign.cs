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
        /// <summary>
        /// All campaigns need names.
        /// </summary>
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }
        /// <summary>
        /// Give it something witty.
        /// </summary>
        [Display(Name = "Campaign Tagline")]
        public string CampaignDetails { get; set; }
        /// <summary>
        /// Lore is needed for this tale to be told proper, so you have to do that world building, right?
        /// </summary>
        [Display(Name = "Campaign Story")]
        public string CampaignLore { get; set; }
        /// <summary>
        /// I mean, yeah, you have to have risks in this campaign you're making, right?
        /// </summary>
        [Display(Name = "Campaign Risks")]
        public RiskLevel CampaignRisk { get; set; }

        
        public int DietId { get; set; }
        [Display(Name = "Associated Diet")]
        [ForeignKey("DietId")]
        public Diet CampaignDiet { get; set; }

        // The list of all workouts associated with the campaign
        public List<WorkoutProgram> CampaignPrograms { get; set; }

        // Get the list of appropriate quests for each campaign
        public List<Quest> CampaignQuest { get; set; }

        // Wait, we're adding bosses now!?
        // That Boss is gonna need some MINIONS
        // AND YOUR CHARACTER NEEDS A DESIGNATED RIVAL PERSON
        public BossEncounter Antagonist { get; set; }
        public PersonalEncounter Rival { get; set; }
        public List<PersonalEncounter> Enemies { get; set; }
    }
}
