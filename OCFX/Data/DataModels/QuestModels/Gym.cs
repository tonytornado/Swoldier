using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        public string Leader { get; set; }
        [Display(Name = "Gym Details")]
        public string Description { get; set; }

		// List of the club's amenities and equipment
        public ICollection<Equipment> Amenities { get; set; }
		public ICollection<Membership> Members { get; set; }
    }
}