using System;
using System.ComponentModel.DataAnnotations;
using static OCFX.DataModels.Workout;

namespace OCFX.DataModels
{
    /// <summary>
    /// Exercise that is a series of movements and muscular twitches.
    /// An exercise can be a part of several workouts and programs.
    /// </summary>
    public class Exercise
    {
        /// <summary>
        /// Standard initialization of an exercise
        /// </summary>
        public Exercise()
        {
        }

        public Exercise(string name, ExerciseType exerType, WorkoutType targetedMuscles, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ExerType = exerType;
            TargetedMuscles = targetedMuscles;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// An exercise that doesn't have a URL or Description
        /// </summary>
        /// <param name="name">Exercise Name</param>
        /// <param name="exerType">Exercise Type</param>
        /// <param name="targetedMuscles">Targeted Muscles of the Exercise</param>
        public Exercise(string name, ExerciseType exerType, WorkoutType targetedMuscles)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ExerType = exerType;
            TargetedMuscles = targetedMuscles;
            Description = "N/A";
        }

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

        public string ExerciseTitle => $"{Name} [{ExerType}]";
    }
}