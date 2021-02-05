using System;

namespace FitLibrary.Exercise
{
    /// <summary>
    /// The main muscle group class.
    /// Goes along with exercise class.
    /// </summary>
    public class MuscleGroup
    {
        /// <summary>
        /// Creates a muscle group with a standard name
        /// </summary>
        /// <param name="name">Muscle group name</param>
        public MuscleGroup(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
