using Microsoft.AspNetCore.Identity;
using SocialLibrary.Profile;
using System;

namespace SocialLibrary.DataModels
{
    public class AppUser : IdentityUser
    {
        public ProfileData Profile { get; set; }
        public OptionData OptionSettings { get; set; }
    }
}
