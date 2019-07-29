using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// Records all quests completed
    /// </summary>
    public class QuestLog
    {
        public QuestLog()
        {
        }

        public QuestLog(int questId, bool completed, Campaign campaign, Profile profile, Quest quest)
        {
            Completed = completed;
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
            Quest = quest ?? throw new ArgumentNullException(nameof(quest));
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "Completed?")]
        public bool Completed { get; set; }
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("QuestId")]
        public Quest Quest { get; set; }
    }
}
