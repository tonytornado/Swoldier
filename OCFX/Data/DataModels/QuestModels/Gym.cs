using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OCFX.DataModels
{
	public partial class Gym
    {
        [Key]
		[Display(Name = "Gym")]
        public int Id { get; set; }
        [Display(Name = "Gym Title")]
        public string Title { get; set; }
        [Display(Name = "Gym Traits")]
        public string Leader { get; set; }
        [Display(Name = "Gym Details")]
        public string Description { get; set; }

		// List of the club's amenities and equipment
        public ICollection<Equipment> Amenities { get; set; }
		public ICollection<Membership> Members { get; set; }
    }

    public class Membership
    { 
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Member Status")]
        public MembershipType Status { get; set; }
        [Display(Name = "Member Profile")]
        public Profile Member { get; set; }
        [Display(Name = "Club Membership")]
        public Gym Club { get; set; }

        public enum MembershipType
        {
            Member = 0,
            Mentor = 1,
            Pending = 2,
            Banned = 3
        }
    }
}