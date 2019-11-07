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

        public Address(AddressType addressTypeName, string streetName, string cityName, string stateName, int zipCode, ProfileSheet profile)
        {
            AddressTypeName = addressTypeName;
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName), "Street is invalid");
            CityName = cityName ?? throw new ArgumentNullException(nameof(cityName),"No city found");
            StateName = stateName ?? throw new ArgumentNullException(nameof(stateName),"No state found");
            ZipCode = zipCode;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile),"A profile could not be found for this address");
        }

        [Key]
        [Display(Name = "Address")]
        public int Id { get; set; }
        [Display(Name = "Address Type")]
        public AddressType AddressTypeName { get; set; }
        [Display(Name = "Street")]
        public string StreetName { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "Postal Code")]
        public int ZipCode { get; set; }

        [NotMapped]
        public string AddressLine1 => $"{StreetName}";
        [NotMapped]
        public string AddressLine2 => $"{CityName}, {StateName} {ZipCode}";

        public ProfileSheet Profile { get; set; }
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