using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class HistorySection
    {
        public HistorySection(string historyAction, string historyDescription, Profile profile)
        {
            HistoryAction = historyAction ?? throw new ArgumentNullException(nameof(historyAction), "What happened and why can't I see it??");
            HistoryDescription = historyDescription ?? throw new ArgumentNullException(nameof(historyDescription), "Description Needed");
            Date = DateTime.Now;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile), "Profile not found");
        }

        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "History Text")]
        public string HistoryAction { get; set; }
        [Display(Name = "History description")]
        public string HistoryDescription { get; set; }
        [Display(Name = "Post Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Profile")]
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }


    }
}
