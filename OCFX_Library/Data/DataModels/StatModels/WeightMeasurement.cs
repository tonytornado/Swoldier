using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// An log entry for a user's weight along with a progress photo (optional)
    /// </summary>
    public class WeightMeasurement
    {

        /// <summary>
        /// Standard Weight Measurement object
        /// </summary>
        public WeightMeasurement()
        {
        }

        /// <summary>
        /// Standard implementation of a Weight Measurement
        /// </summary>
        /// <param name="date"></param>
        /// <param name="weight"></param>
        /// <param name="progressPhoto"></param>
        /// <param name="profile"></param>
        public WeightMeasurement(DateTime date, double weight, Photo progressPhoto, ProfileSheet profile)
        {
            Date = date;
            Weight = weight;
            ProgressPhoto = progressPhoto ?? throw new ArgumentNullException(nameof(progressPhoto));
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public int Id { get; set; }
        /// <summary>
        /// An optional progress @Photo object
        /// </summary>
        [Display(Name = "Progress Photo")]
        public Photo ProgressPhoto { get; set; }
        /// <summary>
        /// The date that the photo was taken, added automatically in most cases.
        /// </summary>
        [Display(Name = "Photo Date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Current weight
        /// </summary>
        [Display(Name = "Weight")]
        [StringLength(4)]
        public double Weight { get; set; }
        /// <summary>
        /// The profile ID of the user making the change.
        /// </summary>
        [ForeignKey("ProfileId")]
        public ProfileSheet Profile { get; set; }
    }
}