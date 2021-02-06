namespace FitLibrary.Exercise
{

    /// <summary>
    /// The main exercise class
    /// </summary>
    public class Exercise
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
}
