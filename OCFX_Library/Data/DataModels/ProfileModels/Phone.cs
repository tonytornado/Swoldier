using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Phone
    {
        public Phone()
        {
        }

        /// <summary>
        /// Standard phone number implementation
        /// </summary>
        /// <param name="phoneTypeName"></param>
        /// <param name="areaCode"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="profile"></param>
        public Phone(PhoneType phoneTypeName,
                     int areaCode,
                     int phoneNumber,
                     ProfileSheet profile)
        {
            AreaCode = areaCode;
            PhoneNumber = phoneNumber;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile), "An associated profile could not be found");
        }

        [Display(Name = "Phone")]
        public int Id { get; set; }

        [Display(Name = "Area Code")] 
        private int AreaCode { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        private int PhoneNumber { get; set; }

        private ProfileSheet Profile { get; set; }

        public string FullNumber => $"({AreaCode}) {PhoneNumber}";
    }

    public enum PhoneType
    {
        [Display(Name = "Home")]
        Home,
        [Display(Name = "Work")]
        Work,
        [Display(Name = "Mobile")]
        Mobile
    }
}