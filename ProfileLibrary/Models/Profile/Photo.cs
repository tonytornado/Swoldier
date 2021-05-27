using Microsoft.AspNetCore.Http;

using System;
using System.ComponentModel.DataAnnotations;

namespace SocialLibrary.Models.Profile
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public Guid PhotoIdentificationString { get; set; }
        // public IFormFile FormFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Alt { get; set; }
        public int PhotoType { get; set; }
    }
}
