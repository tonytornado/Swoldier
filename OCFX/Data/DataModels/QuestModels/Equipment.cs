using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Equipment
    {
        [Key]
        [Display(Name = "Equipment")]
        public int Id { get; set; }
        [Display(Name = "Equipment Title")]
        public string EquipName { get; set; }
        [Display(Name = "Equipment Description")]
        public string EquipDescription { get; set; }
        [Display(Name = "Equipment Tips")]
        public string EquipSkill { get; set; }
    }
}
