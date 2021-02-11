using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using SwoldierCore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SwoldierCore.Data.Profile
{
    public class ProfileBase
    {
        public ProfileBase(){
            
        }
        public ProfileBase(string firstName, string lastName, DateTime dob, string city = null, string state = null)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Dob = dob;
            Deleted = false;
            City = city;
            State = state;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public bool Deleted { get; private set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Deletes the profile while keeping data up.
        /// </summary>
        /// <param name="profile">A profile attached to a user.</param>
        public void DeleteProfile(ProfileBase profile) =>
            // Set the profile to deleted
            profile.Deleted = true;

        public string Location => $"{City}, {State}";
        public string FullName => $"{FirstName} {LastName}";
        public double Age => GetAge();

        private double GetAge()
        {
            var time = (DateTime.Now - Dob).TotalDays
                       / 365;
            return Math.Round(time,0);
        }
    }
}