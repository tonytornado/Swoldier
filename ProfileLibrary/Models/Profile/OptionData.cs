using SocialLibrary.DataModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialLibrary.Profile
{
    public class OptionData
    {
        public OptionData()
        {

        }

        /// <summary>
        /// Creates base data for a new user.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="user">User Data</param>
        public OptionData(string userId, AppUser user)
        {
            ScreenSize = 1;
            DarkMode = false;
            Reminder = 0;
            UserId = userId;
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        [Key]
        public int OptionId { get; set; }
        public int ScreenSize { get; set; }
        public bool DarkMode { get; set; }
        public int Reminder { get; set; }

        public string UserId { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}