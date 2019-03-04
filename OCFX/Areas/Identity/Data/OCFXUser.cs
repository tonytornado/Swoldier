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
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [PersonalData]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [PersonalData]
        [Required]
        [Display(Name = "Date of Birth")]
		[DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1920", "4/20/2001", 
            ErrorMessage = "You must be 18 or older to even use this site... also, not dead or dying.")]
        public DateTime DOB { get; set; }

		// To prevent frequent name changes!
		public DateTime NameChangedDate { get; set; }

		public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}