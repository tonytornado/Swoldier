using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
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
        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; }

        public enum MembershipType
        {
            Member = 0,
            Mentor = 1,
            Pending = 2,
            Banned = 3
        }
    }
}