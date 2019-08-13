using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Photo
    {
        public int Id { get; set; }
        [Display(Name = "URL")]
        public string URL { get; set; }
        [Display(Name = "Caption")]
        public string Caption { get; set; }
        [Display(Name = "Type")]
        public PhotoType Type { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateAdded { get; set; }

        public int ProfileId { get; set; }

        public enum PhotoType
        {
            Profile,
            Progress,
            Action,
            Other
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