using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OCFX.DataModels
{
    /// <summary>
    /// The model for a person's character they have created
    /// </summary>
    public class CharacterModel
    {

        public CharacterModel()
        {
        }

        public CharacterModel(string firstName, string lastName, int STR, int SPD, int VIT, int DEX, int CON, int MVN,
            string backStory, string driveStory, string goals, Archetype fitStyle, ICollection<Skill> skillList,
            ProfileSheet characterProfile, ICollection<Photo> avatars, ICollection<Campaign> campaign, ICollection<Quest> quests,
            bool mainCharacter = true)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            StrengthStat = STR;
            SpeedStat = SPD;
            ConstitutionStat = VIT;
            DexterityStat = DEX;
            ConcentrationStat = CON;
            MotivationStat = MVN;
            BackStory = backStory ?? throw new ArgumentNullException(nameof(backStory));
            DriveStory = driveStory ?? throw new ArgumentNullException(nameof(driveStory));
            Goals = goals ?? throw new ArgumentNullException(nameof(goals));
            FitStyle = fitStyle ?? throw new ArgumentNullException(nameof(fitStyle));
            SkillList = skillList ?? throw new ArgumentNullException(nameof(skillList));
            CharacterProfile = characterProfile ?? throw new ArgumentNullException(nameof(characterProfile));
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
            Quests = quests ?? throw new ArgumentNullException(nameof(quests));
            MainCharacter = mainCharacter;
            Avatars = avatars ?? throw new ArgumentNullException(nameof(avatars));
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        [Display(Name = "Primary")]
        public bool MainCharacter { get; set; }

        /// <summary>
        /// The character's class
        /// </summary>
        [ForeignKey("ClassId")]
        public Archetype FitStyle { get; set; }

        // Link three skills
        public ICollection<Skill> SkillList { get; set; } = new List<Skill>();

        // Tie quests to that character
        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
        public ICollection<Campaign> Campaign { get; set; } = new List<Campaign>();

        // Tie photos to the character
        public ICollection<Photo> Avatars { get; set; } = new List<Photo>();

        // Tie to profile
        [ForeignKey("ProfileId")]
        public ProfileSheet CharacterProfile { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}, the {FitStyle.FitType}";
        [NotMapped]
        public Photo AvatarPhoto => GetAvatarPhoto(Id);


        /// <summary>
        /// Retrieves a Profile Photo from the Photos Nav Property
        /// </summary>
        /// <param name="Id">Profile ID</param>
        /// <returns></returns>
        private Photo GetAvatarPhoto(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return null;
            }
            Photo p = Avatars
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefault(c => c.Type == Photo.PhotoType.Avatar
                                     && c.ProfileId == Id);
            //string j = p.URL;
            return p;
        }
    }
}