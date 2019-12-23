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

        public QuestLog(bool completed, Campaign campaign, CharacterModel character, Quest quest)
        {
            Completed = completed;
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
            Character = character ?? throw new ArgumentNullException(nameof(character));
            Quest = quest ?? throw new ArgumentNullException(nameof(quest));
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "Completed?")]
        public bool Completed { get; set; }
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }
        [ForeignKey("CharacterId")]
        public CharacterModel Character { get; set; }
        [ForeignKey("QuestId")]
        public Quest Quest { get; set; }
    }
}
