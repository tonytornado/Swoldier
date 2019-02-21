using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OCFX.DataModels
{
    // Add profile data for application users by adding properties to the OCFXUser class
    public class OCFXUser : IdentityUser<Guid>
    {
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [PersonalData]
        [Display(Name = "Date of Birth")]
		[DataType(DataType.Date)]
        public DateTime DOB { get; set; }

		// To prevent frequent name changes!
		public DateTime NameChangedDate { get; set; }

		public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}