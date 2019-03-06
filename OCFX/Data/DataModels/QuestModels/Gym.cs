using OCFX.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OCFX.DataModels
{
    public class Gym
    {
        [Key]
        [Display(Name = "Gym")]
        public int Id { get; set; }
        [Display(Name = "Gym Title")]
        public string Title { get; set; }
        [Display(Name = "Gym Leader")]
        public Profile Leader { get; set; }
        [Display(Name = "Gym Details")]
        public string Description { get; set; }
        public ApprovalStatus Status { get; set; }

        // List of the club's amenities and equipment
        public ICollection<Equipment> Amenities { get; set; }

        // List of the club's members
		public ICollection<Membership> Members { get; set; }
        
    }
}