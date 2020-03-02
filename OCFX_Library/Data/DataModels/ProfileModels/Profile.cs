using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace OCFX.DataModels
{
    public class ProfileSheet
    {
        [Key]
        [Display(Name = "User Profile")]
        public int Id { get; set; }

        // Imports from user table [Personal Data]
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [PersonalData]
        [Display(Name = "Date of Birth")]
        public DateTime Dob { get; set; }
        [PersonalData]
        [Display(Name = "Age")]
        [NotMapped]
        public int Age => GetAge(Dob);
        [PersonalData]
        [Display(Name = "Gender")]
        public GenderSpectrum Gender { get; set; }

        // Body Measurements [PERSONAL DATA]
        [PersonalData]
        [Display(Name = "Height (in inches)")]
        [Range(30, 96, ErrorMessage = "Let's go with something believable here.")]
        public int Height { get; set; }
        [PersonalData]
        [Display(Name = "Weight (in lbs.")]
        [Range(50, 600, ErrorMessage = "Again, something believable. ")]
        public int Weight { get; set; }
        [PersonalData]
        [Display(Name = "Neck")]
        public int? NeckMeasurement { get; set; }
        [PersonalData]
        [Display(Name = "Waist")]
        public int? WaistMeasurement { get; set; }
        [PersonalData]
        [Display(Name = "Hips")]
        public int? HipMeasurement { get; set; }

        /// <summary>
        /// The profile's class
        /// </summary>
        [ForeignKey("ClassId")]
        public Archetype FitStyle { get; set; }

        // Navigation properties
        //public Collection<Address> Addresses { get; set; }
        //public Collection<Phone> Phones { get; set; }
        public Collection<Photo> Photos { get; set; } = new Collection<Photo>();
        public Collection<WeightMeasurement> Weights { get; set; } = new Collection<WeightMeasurement>();
        public Collection<WorkoutSetLog> WorkoutHistory { get; set; } = new Collection<WorkoutSetLog>();

        [InverseProperty("Following")]
        public Collection<FriendSheet> Following { get; set; } = new Collection<FriendSheet>();

        [InverseProperty("Follower")]
        public Collection<FriendSheet> Followers { get; set; } = new Collection<FriendSheet>();
        
        /// <summary>
        /// The profile's received messages
        /// </summary>
        [InverseProperty("Receiver")]
        public Collection<Shout> ReceivedMessages { get; set; } = new Collection<Shout>();

        /// <summary>
        /// A profile's sent messages
        /// </summary>
        [InverseProperty("Sender")]
        public Collection<Shout> SentMessages { get; set; } = new Collection<Shout>();

        /// <summary>
        /// The profile's entries on other walls
        /// </summary>
        [InverseProperty("Profile")]
        public Collection<Post> Posts { get; set; } = new Collection<Post>();

        /// <summary>
        /// The profile's wall entries on their own wall
        /// </summary>
        [InverseProperty("Entry")]
        public Collection<Post> Entries { get; set; } = new Collection<Post>();
        
        /// <summary>
        /// Characters of a person's profile
        /// </summary>
        [InverseProperty("CharacterProfile")]
        public Collection<CharacterModel> Characters { get; set; } = new Collection<CharacterModel>();

        [InverseProperty("Member")]
        public Membership ClubMemberShip { get; set; } 

        // Tie to user login
        public OCFXUser FitUser { get; set; }

        /// <summary>
        /// The Gender Attribute <see cref="Enum"/>
        /// </summary>
        public enum GenderSpectrum
        {
            [Display(Name = "Non-Binary")]
            NonBinary,
            [Display(Name = "Cis Male")]
            CisMale,
            [Display(Name = "Cis Female")]
            CisFemale,
            [Display(Name = "Trans Male")]
            TransMale,
            [Display(Name = "Trans Female")]
            TransFemale,
            [Display(Name = "Prefer not to disclose")]
            NotDisclosed
        }

        // Convenience Properties
        /// <summary>
        /// Shows the full name
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Shows the subtitle of a person
        /// </summary>
        [NotMapped]
        public string SubTitle => $"{Age} year-old {Gender}";

        /// <summary>
        /// Profile Photo
        /// </summary>
        [NotMapped]
        public Photo ProfilePhoto => GetProfilePhoto();

        /// <summary>
        /// Calculates BFP
        /// </summary>
        [NotMapped]
        [Display(Name = "Body Fat Percentage")]
        public double BodyFat => GetBodyFat(Height, Weight, NeckMeasurement, WaistMeasurement, HipMeasurement);

        /// <summary>
        /// Retrieves a Profile Photo from the Photos Nav Property
        /// </summary>
        /// <param name="i">Profile ID</param>
        /// <returns></returns>
        private Photo GetProfilePhoto()
        {
            if (this.Id == 0) return null;
            Photo p = Photos
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefault(c => c.Type == Photo.PhotoType.Profile
                                     && c.ProfileId == this.Id);
            return p;
        }

        /// <summary>
        /// Calculate age from a given date of birth
        /// </summary>
        /// <param name="dob"></param>
        /// <returns>int AGE</returns>
        private static int GetAge(DateTime dob)
        {
            int age = Convert.ToInt32((DateTime.Now - dob).TotalDays / 365);
            return age;
        }

        /// <summary>
        /// Calculate the body fat percentage of the profile
        /// </summary>
        /// <param name="height">Height in cm</param>
        /// <param name="weight">Weight in lbs.</param>
        /// <param name="neck">Neck Length in inches</param>
        /// <param name="waist">Waist Circumference in inches</param>
        /// <param name="hip">Hip Circumference in inches</param>
        /// <returns>double</returns>
        private double GetBodyFat(int height, int weight, int? neck, int? waist, int? hip)
        {
            double percentage = 0.0;
            double weightConversion = weight / 2.2;
            double heightConversion = height / 2.54;

            if (neck == null || waist == null || hip == null)
            {
                return 0.0;
            }

            if (neck == 0 || waist == 0 || hip == 0)
            {
                return 0.0;
            }

            // Check bone structures
            if (Gender == GenderSpectrum.CisMale ||
                Gender == GenderSpectrum.TransFemale ||
                Gender == GenderSpectrum.NotDisclosed)
                //{
                //    double f1 = (weight * 1.082) + 94.42;
                //    double? f2 = waist * 4.15;
                //    double? lbm = f1 - f2;
                //    double? bfw = weight - lbm;
                //    percentage = Convert.ToDouble((bfw / weight) * 100);
                //}
            {
                double f1 = 495.0;
                double f2 = 1.0324 - (0.19077 * Math.Log10(Convert.ToDouble(waist - neck))) + (0.15456 * Math.Log10(heightConversion));
                double f3 = 450.0;
                percentage = (f1 / f2) - f3;
            }

            if (Gender == GenderSpectrum.CisFemale ||
                Gender == GenderSpectrum.TransMale)
            {
                double f1 = (weightConversion * 0.732) + 8.987;
                double f2 = 6 / 3.140;
                double? f3 = waist * 0.157;
                double? f4 = hip * 0.249;
                double f5 = 9 * 0.434;
                double? lbm = f1 + f2 - f3 - f4 + f5;
                double? bfw = weightConversion - lbm;
                percentage = Convert.ToDouble((bfw / weight) * 100);
            }

            return percentage;
        }
    }
}