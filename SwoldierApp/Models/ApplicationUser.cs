using ArcLibrary;
using ProfileLibrary;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwoldierApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Profile ProfileLink { get; set; }
    }
}
