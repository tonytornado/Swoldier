using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Phone
    {
        [Display(Name = "Phone")]
        public int Id { get; set; }
        [Display(Name = "Phone Type")]
        public PhoneType PhoneTypeName { get; set; }
        [Display(Name = "Area Code")]
        public int AreaCode { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        public Profile Profile { get; set; }

        public string FullNumber => $"({AreaCode}) {PhoneNumber}";
    }

    public enum PhoneType
    {
        [Display(Name = "Home")]
        Home = 1,
        [Display(Name = "Work")]
        Work = 2,
        [Display(Name = "Mobile")]
        Mobile = 3
    }
}