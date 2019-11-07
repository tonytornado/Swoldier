using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static OCFX.DataModels.Session;

namespace OCFX.DataModels
{
    /// <summary>
    /// A gym or club that members can join. 
    /// Needs to have some sort of approval beforehand.
    /// Oh, and I guess you can arrange meet ups and such
    /// </summary>
    public class Gym
    {
        [Key]
        [Display(Name = "Gym")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Gym Title")]
        public string Title { get; set; }
        [Display(Name = "Gym Leader")]
        public ProfileSheet Leader { get; set; }
        [Required]
        [Display(Name = "Gym Details")]
        public string Description { get; set; }
        [Display(Name = "Gym Status")]
        public ApprovalStatus Status { get; set; }
        [Required]
        [Display(Name = "Frequency")]
        public MeetingInterval MeetingFrequency { get; set; }
        [Required]
        [Display(Name = "Meeting Day")]
        public DayOfWeek MeetingDate { get; set; }
        [Required]
        [Display(Name = "Meeting Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime MeetingTime { get; set; }

        /// <summary>
        /// List of the club's amenities and equipment
        /// </summary>
        public ICollection<GymRelation> Amenities { get; set; }

        /// <summary>
        /// List of the club's members
        /// </summary>
		public ICollection<Membership> Members { get; set; }

        /// <summary>
        /// Events
        /// </summary>
        public ICollection<Session> Meetings { get; set; }
    }

    /// <summary>
    /// Gym Equipment (i.e. Pool, Sauna, TRX)
    /// </summary>
    public class Equipment
    {
        [Key]
        [Display(Name = "Equipment")]
        public int Id { get; set; }
        [Display(Name = "Equipment Title")]
        public string EquipName { get; set; }
        [Display(Name = "Equipment Description")]
        public string EquipDescription { get; set; }

        /// <summary>
        /// Where the equipment is located
        /// </summary>
        public ICollection<GymRelation> Gyms { get; set; }
    }

    /// <summary>
    /// Gym Equipment and Amenities relation table
    /// </summary>
    public class GymRelation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Equipment ID")]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        [Display(Name = "Gym ID")]
        public int GymId { get; set; }
        public Gym Gym { get; set; }
    }
}