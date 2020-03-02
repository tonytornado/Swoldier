using System;
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
        public Campaign()
        {
        }

        public Campaign(string name, string details, string lore, RiskLevel risk, Diet nutrition)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Details = details ?? throw new ArgumentNullException(nameof(details));
            Lore = lore ?? throw new ArgumentNullException(nameof(lore));
            Risk = risk;
            Nutrition = nutrition ?? throw new ArgumentNullException(nameof(nutrition));
        }

        [Display(Name = "Campaign")]
        public int Id { get; set; }
        /// <summary>
        /// All campaigns need names.
        /// </summary>
        [Display(Name = "Campaign Name")]
        public string Name { get; set; }
        /// <summary>
        /// Give it something witty.
        /// </summary>
        [Display(Name = "Campaign Tagline")]
        public string Details { get; set; }
        /// <summary>
        /// Lore for the campaign
        /// </summary>
        [Display(Name = "Campaign Story")]
        public string Lore { get; set; }
        /// <summary>
        /// I mean, yeah, you have to have risks in this campaign you're making, right?
        /// </summary>
        [Display(Name = "Campaign Risks")]
        public RiskLevel Risk { get; set; }

        [Display(Name = "Associated Diet")]
        [ForeignKey("DietId")]
        public Diet Nutrition { get; set; }

        // The list of all workouts associated with the campaign
        /// <summary>
        /// The list of associated Campaign Programs
        /// </summary>
        public List<WorkoutProgram> CampaignPrograms { get; set; } = new List<WorkoutProgram>();

        // Get the list of appropriate quests for each campaign
        /// <summary>
        /// A list of quests in the Campaign
        /// </summary>
        public List<Quest> Quests { get; set; } = new List<Quest>();

        // Wait, we're adding bosses now!?
        // That Boss is gonna need some MINIONS
        public BossEncounter Antagonist { get; set; }
        public List<PersonalEncounter> Minions { get; set; } = new List<PersonalEncounter>();
    }
}
