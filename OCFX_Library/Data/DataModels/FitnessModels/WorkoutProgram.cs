using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// A relation table for the many to many relationship of the workouts to exercises.
    /// </summary>
    public class WorkoutProgram
    {
        [Key]
        [Display(Name = "Workout / Exercise Relation")]
        public int WorkoutProgramId { get; set; }

        [Display(Name = "Exercise ID's")]
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        [Display(Name = "Workout Name")]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }

        [Display(Name = "Sets")]
        public int Sets { get; set; }

        [Display(Name = "Reps")]
        [Range(1, 20, ErrorMessage = "Uhhh....")]
        public int Repetitions { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    //struct WorkoutProgramName
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    [ForeignKey("WorkoutProgramId")]
    //    public WorkoutProgram Worx { get; set; }
    //}
}