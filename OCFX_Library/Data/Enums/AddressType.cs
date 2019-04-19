using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public enum AddressType
    {
		[Display(Name = "Home")]
        Home = 1,
		[Display(Name = "Work")]
		Work = 2,
		[Display(Name = "Other")]
		Other = 3
    }
}