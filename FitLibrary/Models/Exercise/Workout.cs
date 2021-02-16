using System.Collections.Generic;

namespace FitLibrary.Models.Fit
{
    /// <summary>
    /// The main workout class
    /// Can contain a list of exercises.
    /// </summary>
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public MuscleGroup GreatFor { get; set; }
        public List<Exercise> WorkoutSet { get; set; }

        /// <summary>
        /// Returns the exercise count
        /// </summary>
        public int ExerciseCount => WorkoutSet.Count;

        /// <summary>
        /// Returns the workout name and muscle group
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public override string ToString() => $"{Name} - {GreatFor}";

    }
}
