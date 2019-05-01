using System.ComponentModel.DataAnnotations;
using static OCFX.DataModels.Workout;

namespace OCFX.DataModels
{
    // An exercise can be a part of several workouts?
    public class Exercise
    {
        [Key]
        [Display(Name = "Exercise")]
        public int Id { get; set; }

        [Display(Name = "Exercise Name")]
        public string Name { get; set; }

        [Display(Name = "Exercise Type")]
        public ExerciseType ExerType { get; set; }

        [Display(Name = "Targeted Muscle Groups")]
        public WorkoutType TargetedMuscles { get; set; }

        [Display(Name = "Exercise Description")]
        public string Description { get; set; }

        [Display(Name = "Video Link")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Url]
        public string Url { get; set; }

        public enum ExerciseType
        {
            [Display(Name = "Cardio")]
            Cardio = 1,
            [Display(Name = "Strength")]
            Strength = 2,
            [Display(Name = "Balance")]
            Balance = 3,
            [Display(Name = "Flexibility")]
            Flexibility = 4,
            [Display(Name = "Tabata")]
            Tabata = 5
        }
    }
}