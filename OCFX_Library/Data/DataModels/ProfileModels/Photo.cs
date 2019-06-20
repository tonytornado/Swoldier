using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Photo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Caption { get; set; }
        public PhotoType Type { get; set; }
        public DateTime DateAdded { get; set; }

        public int ProfileId { get; set; }

        public enum PhotoType
        {
            Profile = 1,
            Progress = 2,
            Action = 3,
            Other = 4
        }
    }

    public class PhotoUpload
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Photo Upload")]
        public IFormFile PhotoFile { get; set; }
    }
}