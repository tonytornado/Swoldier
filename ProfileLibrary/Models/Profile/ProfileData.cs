using SocialLibrary.DataModels;
using SocialLibrary.DataModels.Mail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialLibrary.Profile
{
    public class ProfileData
    {
        /// <summary>
        /// Base class
        /// </summary>
        public ProfileData()
        {

        }
        /// <summary>
        /// Creation class
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="dob">Date of Birth</param>
        /// <param name="city">The City</param>
        /// <param name="state">The State</param>
        public ProfileData(string firstName, string lastName, DateTime dob, string city = null, string state = null)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Dob = dob;
            Deleted = false;
            City = city;
            State = state;
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Deleted { get; private set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }

        public Guid MailboxId { get; set; }
        [JsonIgnore]
        [ForeignKey("MailboxId")]
        public Mailbox Mail { get; set; }

        /// <summary>
        /// Deletes the profile while keeping data up.
        /// </summary>
        /// <param name="profile">A profile attached to a user.</param>
        public static void DeleteProfile(ProfileData profile) =>
            // Set the profile to deleted
            profile.Deleted = true;

        /// <summary>
        /// Returns the location in a profile
        /// </summary>
        /// <param name="City">The City</param>
        /// <param name="State">The State</param>
        /// <returns></returns>
        public static string GetLocation(string City, string State)
        {
            if (City == null || State == null)
            {
                if (State != null)
                {
                    return State;
                }
                return "Unknown";
            }
            return $"{City}, {State}";
        }

        public string FullName => $"{FirstName} {LastName}";
        public double Age => GetAge();

        private double GetAge()
        {
            var time = (DateTime.Now - Dob).TotalDays
                       / 365;
            return Math.Round(time, 0);
        }
    }
}