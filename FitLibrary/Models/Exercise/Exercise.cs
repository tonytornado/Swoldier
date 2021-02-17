namespace FitLibrary.Models.Fit
{
    /// <summary>
    /// The main exercise base
    /// </summary>
    public class ExerciseBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }

        /// <summary>
        /// Shows the exercise and description
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name} - {MuscleGroup.Name}";
    }

    /// <summary>
    /// Exercise class for entering new data into workouts
    /// </summary>
    public class Exercise : ExerciseBase
    {
        /// <summary>
        /// Creates an exercise set for a workout
        /// </summary>
        /// <param name="order"></param>
        /// <param name="set"></param>
        /// <param name="repCount"></param>
        public Exercise(int order, int set, int repCount)
        {
            Order = order;
            Set = set;
            RepCount = repCount;
        }

        public int Order { get; set; }
        public int Set { get; set; }
        public int RepCount { get; set; }
    }

    public class ExerciseDesc : ExerciseBase
    {
    }
}
