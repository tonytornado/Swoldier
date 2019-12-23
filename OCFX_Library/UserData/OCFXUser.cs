using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    // Add profile data for application users by adding properties to the OCFXUser class

    /// <summary>
    /// Added properties for the standard user class
    /// </summary>
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

        /// <summary>
        /// A datetime value of the last name change
        /// Used in preventing frequent name changes
        /// </summary>
        public DateTime NameChangedDate { get; set; }

        public int ProfileId { get; set; }
        public ProfileSheet Profile { get; set; }
    }
}