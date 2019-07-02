﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Membership
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Member Status")]
        public MembershipType Status { get; set; }
        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Member Profile")]
        [ForeignKey("MemberId")]
        public Profile Member { get; set; }

        [Display(Name = "Club Membership")]
        [ForeignKey("ClubId")]
        public Gym Club { get; set; }

        public enum MembershipType
        {
            [Display(Name = "Leader")]
            Leader = 0,
            [Display(Name = "Mentor")]
            Mentor = 1,
            [Display(Name = "Member")]
            Member = 2,
            [Display(Name = "Pending")]
            Pending = 3,
            [Display(Name = "Banned")]
            Banned = 4,
        }
    }
}