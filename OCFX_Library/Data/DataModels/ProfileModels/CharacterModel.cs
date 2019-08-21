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
        public ICollection<Skill> SkillList { get; set; }
        
        // Tie quests to that character
        public ICollection<Quest> Quests { get; set; }
        public ICollection<Campaign> Campaign { get; set; }

        // Tie photos to the character
        public ICollection<Photo> Avatars { get; set; }

        // Tie to profile
        [ForeignKey("ProfileId")]
        public Profile CharacterProfile { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}, the {FitStyle.FitType}";


        /// <summary>
        /// Retrieves a Profile Photo from the Photos Nav Property
        /// </summary>
        /// <param name="Id">Profile ID</param>
        /// <returns></returns>
        private Photo GetAvatarPhoto(int? Id)
        {
            if (Id != null && Id != 0)
            {
                Photo p = Avatars
                    .OrderByDescending(d => d.DateAdded)
                    .FirstOrDefault(c =>
                    {
                        return c.Type == Photo.PhotoType.Avatar
                               && c.ProfileId == Id;
                    });
                //string j = p.URL;
                return p;
            }
            return null;
        }
    }
}