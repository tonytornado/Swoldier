using SocialLibrary.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace FitLibrary.Models.Community
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ForumId { get; set; }
        [JsonIgnore]
        [ForeignKey("ForumId")]
        public ClubBoard Forum { get; set; }


        public int? CaptainId { get; set; }
        [JsonIgnore]
        public ProfileData Captain { get; set; }
        
        public List<ProfileData> Members { get; set; }
    }
}
