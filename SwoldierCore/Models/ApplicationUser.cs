using Microsoft.AspNetCore.Identity;
using SwoldierCore.Data.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwoldierCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ProfileBase Profile { get; set; }
    }
}
