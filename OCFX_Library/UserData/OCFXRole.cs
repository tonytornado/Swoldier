using Microsoft.AspNetCore.Identity;
using System;

namespace OCFX.Areas.Identity.Data
{
    public class OCFXRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
