using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Diet
    {
        /// <summary>
        /// Simple implementation of Diet
        /// </summary>
        public Diet()
        {
        }

        /// <summary>
        /// Standard implementation of Diet
        /// </summary>
        /// <param name="dietName"></param>
        /// <param name="dietTypeName"></param>
        /// <param name="carbohydrates"></param>
        /// <param name="protein"></param>
        /// <param name="fats"></param>
        public Diet(string dietName,
                    DietType dietTypeName,
                    int carbohydrates,
                    int protein,
                    int fats)
        {
            if(carbohydrates + protein + fats != 100)
            {
                throw new ArithmeticException("Macronutrients");
            }

            DietName = dietName ?? throw new ArgumentNullException(nameof(dietName));
            DietTypeName = dietTypeName;
            Carbohydrates = carbohydrates;
            Protein = protein;
            Fats = fats;
        }

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