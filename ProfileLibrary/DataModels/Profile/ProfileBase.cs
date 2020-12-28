using System;

namespace ProfileLibrary.DataModels.Profile
{
    public class ProfileBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public Guid UserId { get; set; }
        // public ApplicationUser User { get; set; }
    }
}