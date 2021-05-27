using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SocialLibrary.Profile;

namespace SocialLibrary.Feed
{
    public class Wall
    {
        [Key]
        public int WallId { get; set; }
        public List<Post> Posts { get; set; }

        public int ProfileId { get; set; }
        [JsonIgnore]
        // [ForeignKey("ProfileId")]
        public ProfileData Profile { get; set; }
    }
}