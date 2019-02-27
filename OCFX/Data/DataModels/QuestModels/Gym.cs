using OCFX.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.DataModels
{
    public class Gym
    {
        private OCFXContext _context { get; set; }

        public Gym()
        {

        }

        private Gym(OCFXContext context)
        {
            _context = context;
        }

        [Key]
        [Display(Name = "Gym")]
        public int Id { get; set; }
        [Display(Name = "Gym Title")]
        public string Title { get; set; }
        [Display(Name = "Gym Leader")]
        public Profile Leader  => _context.Memberships.FirstOrDefault(i => i.Status == Membership.MembershipType.Leader).Member;
        [Display(Name = "Gym Details")]
        public string Description { get; set; }

		// List of the club's amenities and equipment
        public ICollection<Equipment> Amenities { get; set; }
		public ICollection<Membership> Members { get; set; }
    }
}