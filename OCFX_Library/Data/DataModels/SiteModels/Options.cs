using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OCFX.DataModels.SiteModels
{
    /// <summary>
    /// Sitewide options for the user to change
    /// </summary>
    public class Options
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Option for newsletter subscription
        /// </summary>
        public int NewsletterOption { get; set; }
        /// <summary>
        /// Option for push/email notifications
        /// </summary>
        public int NotificationOption { get; set; }
        /// <summary>
        /// Option for conversion units
        /// </summary>
        public Conversion MeasurementUnits { get; set; }

        public Profile UserProfile { get; set; }

        /// <summary>
        /// Represents a measurement system conversion. 
        /// This allows the user to see Metric or Imperial Units
        /// </summary>
        public enum Conversion
        {
            Metric = 1,
            Imperial = 2
        }
    }
}
