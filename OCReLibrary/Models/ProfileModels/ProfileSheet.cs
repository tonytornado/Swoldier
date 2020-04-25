using OCRX.Models;
using System;
using System.Collections.Generic;

namespace OCReLibrary
{
    public class ProfileSheet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }


        /* Nav properties */
        public List<CharacterSheet> Characters { get; set; }
        public List<Conversation> Conversations { get; set; }


        /* Methods */

        public int Age => GetAge();

        public int GetAge()
        {
            return (DateTime.Now - DOB).Days / 365;
        }

        public ApplicationUser AppUser { get; set; }
    }
}