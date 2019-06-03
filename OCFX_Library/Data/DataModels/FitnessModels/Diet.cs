using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Diet
    {
        [Display(Name = "Diet")]
        public int Id { get; set; }
        [Display(Name = "Diet Name")]
        public string DietName { get; set; }
        [Display(Name = "Diet Type")]
        public DietType DietTypeName { get; set; }
        [Display(Name = "Carbohydrates")]
        [Range(1, 100)]
        public int Carbohydrates { get; set; }
        [Display(Name = "Protein")]
        [Range(1, 100)]
        public int Protein { get; set; }
        [Display(Name = "Fat")]
        [Range(1, 100)]
        public int Fats { get; set; }

        public enum DietType
        {
            [Display(Name = "Fat Loss")]
            FatLoss = 1,
            [Display(Name = "Muscle Mass Gain")]
            MassGain = 2,
            [Display(Name = "Maintenance")]
            Maintenance = 3,
            [Display(Name = "Cutting")]
            MassCut = 4
        }
    }
}