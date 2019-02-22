using Microsoft.AspNetCore.Identity;
using OCFX.Data.DataModels.SocialModels;
using OCFX.DataModels.SocialModels;
using System.Collections.Generic;
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
		[Display(Name = "Age")]
		public int Age { get; set; }
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
		[Display(Name = "Waist")]
		[PersonalData]
		public int? WaistMeasurement { get; set; }
		[Display(Name = "Hips")]
		[PersonalData]
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
		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public Archetype FitStyle { get; set; }

		// Navigation properties
		public ICollection<Address> Addresses { get; set; }
		public ICollection<Phone> Phones { get; set; }
		public ICollection<Photo> Photos { get; set; }

		[InverseProperty("Following")]
		public ICollection<Friend> Following { get; set; }

		[InverseProperty("Follower")]
		public ICollection<Friend> Followers { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Shout> ReceivedMessages { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Shout> SentMessages { get; set; }

        [InverseProperty("Profile")]
        public ICollection<Post> Posts { get; set; }

        [InverseProperty("Entry")]
        public ICollection<Post> Entries { get; set; }

        // Tie to user login, quest, campaign?
        public OCFXUser FitUser { get; set; }
		public Quest Quest { get; set; }
		public Campaign Campaign { get; set; }
		public Gym Gym { get; set; }

		// The Gender Attribute Enum
		public enum GenderSpectrum
		{
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
	}
}
