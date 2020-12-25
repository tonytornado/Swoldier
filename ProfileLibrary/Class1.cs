using System;

namespace ProfileLibrary
{
    public class Profile
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }


        public ApplicationUser User { get; set; }
        public OCFXProfile ProfileSetup { get; set; }
    }
}
