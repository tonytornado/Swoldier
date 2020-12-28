using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ArcLibrary.DataModels.SheetModels;
using ProfileLibrary.DataModels.Profile;
using SwoldierApp.Data;

namespace SwoldierApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ProfileBase ProfileLink { get; set; }
        public OptionData OptionData { get; set; }
        public List<Sheet> Sheets { get; set; }
    }
}
