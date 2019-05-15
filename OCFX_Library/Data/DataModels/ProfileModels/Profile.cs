using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Profile
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
        [NotMapped]
        public DateTime DOB { get; set; }
        [PersonalData]
        [Display(Name = "Age")]
        public int Age => GetAge(DOB);
        [PersonalData]
        [Display(Name = "Gender")]
        public GenderSpectrum Gender { get; set; }

        // Body Measurements [PERSONAL DATA]
        [PersonalData]
        [Display(Name = "Height")]
        public int Height { get; set; }
        [PersonalData]
        [Display(Name = "Weight")]
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

        // Starting stats from typical character sheet
        [Display(Name = "Strength")]
        [Range(1, 10)]
        public int StrengthStat { get; set; }
        [Display(Name = "Speed")]
        [Range(1, 10)]
        public int SpeedStat { get; set; }
        [Display(Name = "Constitution")]
        [Range(1, 10)]
        public int ConstitutionStat { get; set; }
        [Display(Name = "Dexterity")]
        [Range(1, 10)]
        public int DexterityStat { get; set; }
        [Display(Name = "Concentration")]
        [Range(1, 10)]
        public int ConcentrationStat { get; set; }
        [Display(Name = "Motivation")]
        [Range(1, 10)]
        public int MotivationStat { get; set; }

        // Character Background
        [Display(Name = "Background")]
        public string BackStory { get; set; }
        [Display(Name = "Drive/Determination")]
        public string DriveStory { get; set; }
        [Display(Name = "Goals")]
        public string Goals { get; set; }

        // Imports (Create forms for each)
        [ForeignKey("ClassId")]
        public Archetype FitStyle { get; set; }

        // Navigation properties
        public Collection<Address> Addresses { get; set; }
        public Collection<Phone> Phones { get; set; }
        public Collection<Photo> Photos { get; set; }

        [InverseProperty("Following")]
        public Collection<Friend> Following { get; set; }

        [InverseProperty("Follower")]
        public Collection<Friend> Followers { get; set; }

        [InverseProperty("Receiver")]
        public Collection<Shout> ReceivedMessages { get; set; }

        [InverseProperty("Sender")]
        public Collection<Shout> SentMessages { get; set; }

        [InverseProperty("Profile")]
        public Collection<Post> Posts { get; set; }

        [InverseProperty("Entry")]
        public Collection<Post> Entries { get; set; }

        [InverseProperty("Member")]
        public Membership Gym { get; set; }

        // Tie to user login, quest, campaign?
        public OCFXUser FitUser { get; set; }
        public Quest Quest { get; set; }
        public Campaign Campaign { get; set; }

        // The Gender Attribute Enum
        public enum GenderSpectrum
        {
            [Display(Name = "Non-Binary")]
            NonBinary = 0,
            [Display(Name = "Cis Male")]
            CisMale = 1,
            [Display(Name = "Cis Female")]
            CisFemale = 2,
            [Display(Name = "Trans Male")]
            TransMale = 3,
            [Display(Name = "Trans Female")]
            TransFemale = 4,
            [Display(Name = "Prefer not to disclose")]
            NotDisclosed = 5
        }

        // Anything else?
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        private int GetAge(DateTime DOB)
        {
            int age = Convert.ToInt32((DateTime.Today - DOB).TotalDays / 365);
            return age;
        }
    }
}
