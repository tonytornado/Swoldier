using SwoldierCore.Models;

namespace SwoldierCore.Data.Profile
{
    public class OptionData
    {
        public int Id { get; set; }
        public int ScreenSize { get; set; }
        public bool DarkMode { get; set; }
        public int Reminder { get; set; }

        //public int UserId { get; set; }
        //public ApplicationUser User { get; set; }
    }
}