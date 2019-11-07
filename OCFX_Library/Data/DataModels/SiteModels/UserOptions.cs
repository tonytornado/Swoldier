using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels.SiteModels
{
    /// <summary>
    /// Sitewide options for the user to change
    /// </summary>
    public class UserOptions
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Option for newsletter subscription
        /// </summary>
        [Display(Name = "Newsletter Option")]
        public int NewsletterOption { get; set; }
        /// <summary>
        /// Option for push/email notifications
        /// </summary>
        [Display(Name = "Notifications")]
        public int NotificationOption { get; set; }
        /// <summary>
        /// Option for conversion units
        /// </summary>
        [Display(Name = "Measurement Units")]
        public Conversion MeasurementUnits { get; set; }

        public ProfileSheet UserProfile { get; set; }

        /// <summary>
        /// Represents a measurement system conversion. 
        /// This allows the user to see Metric or Imperial Units
        /// </summary>
        public enum Conversion
        {
            Metric,
            Imperial
        }
    }
}
