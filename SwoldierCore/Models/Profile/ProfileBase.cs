using System;

namespace SwoldierCore.Data.Profile
{
    public class ProfileBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
    }
}