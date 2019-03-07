using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// A gym or club that members can join. 
    /// Needs to have some sort of approval beforehand.
    /// </summary>
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
        public ICollection<GymRelation> Amenities { get; set; }

        // List of the club's members
		public ICollection<Membership> Members { get; set; }
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

        public ICollection<GymRelation> Gyms { get; set; }
    }

    /// <summary>
    /// Gym Equipment and Amenities relation table
    /// </summary>
    public class GymRelation
    {
        [Key]
        public int GymRelationId { get; set; }

        [Display(Name = "Equipment ID")]
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }

        [Display(Name = "Gym ID")]
        public int GymId { get; set; }
        [ForeignKey("GymId")]
        public Gym Gym { get; set; }
    }
}