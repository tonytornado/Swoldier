using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Address
    {
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

		//public int ProfileId { get; set; }
		public Profile Profile { get; set; }
    }
}