using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class HistorySection
    {
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
