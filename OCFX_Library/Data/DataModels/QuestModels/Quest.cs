using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    // Each quest can have several players running it.
    // May also have a specific type of workouts associated with it.

    public class Quest
	{
		[Key]
		[Display(Name = "Quest")]
		public int Id { get; set; }
		[Display(Name = "Quest Name")]
		public string QuestName { get; set; }
		[Display(Name = "Quest Type")]
		public QuestType QuestStyle { get; set; }
		[Display(Name = "Quest Description")]
		public string QuestStory { get; set; }

		// Folks that are on this quest
		public List<Profile> CurrentPlayers { get; set; }

        // Encounters on this quest
        public List<Encounter> Encounters { get; set; }

		// It's part of the campaign, yeah?
		public int CampaignId { get; set; }
		[ForeignKey("CampaignId")]
		public Campaign Campaign { get; set; }
	}

	public enum QuestType
	{
		[Display(Name = "Power")]
		Power = 1,
		[Display(Name = "Endurance")]
		Endurance = 2,
		[Display(Name = "Speed")]
		Speed = 3,
		[Display(Name = "Consistency")]
		Consistency = 4
	}
}
