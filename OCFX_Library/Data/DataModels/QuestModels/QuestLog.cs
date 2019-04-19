using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class QuestLog
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Quest ID")]
		public int QuestId { get; set; }
		[Display(Name = "Profile ID")]
		public int ProfileId { get; set; }
        [Display(Name = "Campaign ID")]
        public int CampaignId { get; set; }
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
