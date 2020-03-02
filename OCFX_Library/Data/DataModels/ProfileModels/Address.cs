using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string streetName, string cityName, string stateName, int zipCode, ProfileSheet profile)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName), "Street is invalid");
            CityName = cityName ?? throw new ArgumentNullException(nameof(cityName),"No city found");
            StateName = stateName ?? throw new ArgumentNullException(nameof(stateName),"No state found");
            ZipCode = zipCode;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile),"A profile could not be found for this address");
        }

        [Key]
        [Display(Name = "Address")]
        public int Id { get; set; }
        [Display(Name = "Street")] private string StreetName { get; }
        [Display(Name = "City")] private string CityName { get; }
        [Display(Name = "State")] private string StateName { get; }
        [Display(Name = "Postal Code")] private int ZipCode { get; }

        [NotMapped]
        public string AddressLine1 => $"{StreetName}";
        [NotMapped]
        public string AddressLine2 => $"{CityName}, {StateName} {ZipCode}";

        private ProfileSheet Profile { get; }
    }

    public enum AddressType
    {
        [Display(Name = "Home")]
        Home,
        [Display(Name = "Work")]
        Work,
        [Display(Name = "Other")]
        Other
    }
}