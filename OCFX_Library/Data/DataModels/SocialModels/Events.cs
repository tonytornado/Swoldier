using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Type of Event")]
        public EventType Type { get; set; }
        [Display(Name = "Name of Event")]
        public string Name { get; set; }
        [Display(Name = "Event description")]
        public string Description { get; set; }
        //[Display(Name = "Start Date")]
        //[DataType(DataType.Date)]
        //public DateTime StartDate { get; set; }
        [Display(Name = "Start")]
        //[DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        //[Display(Name = "End Date")]
        //[DataType(DataType.Date)]
        //public DateTime EndDate { get; set; }
        [Display(Name = "End")]
        //[DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        [Display(Name = "Frequency")]
        public MeetingInterval Interval { get; set; }

        [NotMapped]
        public double TimeLength => Duration(StartTime, EndTime);

        /// <summary>
        /// Provides the length of time in a readable format
        /// </summary>
        /// <param name="Time1">The Start Time of the event</param>
        /// <param name="Time2">The End Time of the event</param>
        /// <returns></returns>
        private double Duration(DateTime Time1, DateTime Time2)
        {
            Time1 = StartTime;
            Time2 = EndTime;

            TimeSpan lengthOfTime = (Time2 - Time1);
            double time = lengthOfTime.Hours > 24 ? lengthOfTime.TotalDays : lengthOfTime.Hours;
            return time;
        }

        public enum DayInterval
        {
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }

        public enum MeetingInterval
        {
            Once = 1,
            Weekly = 2,
            Monthly = 3,
            Annually = 4,
        }

        public enum EventType
        {
            Sitewide = 1,
            Gym = 2,
            Personal = 3,
        }
    }
}
