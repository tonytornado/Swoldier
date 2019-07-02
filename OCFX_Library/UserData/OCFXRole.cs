using Microsoft.AspNetCore.Identity;
using System;

namespace OCFX.Areas.Identity.Data
{
    public class OCFXRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Role Description
        /// </summary>
        public string Description { get; set; }
    }
}
