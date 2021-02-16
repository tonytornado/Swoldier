using Microsoft.EntityFrameworkCore;
using SocialLibrary.DataModels.Mail;
using SocialLibrary.Feed;
using SocialLibrary.Profile;

namespace SocialLibrary.Data
{
    public class SocialDB : DbContext
    {
        public SocialDB (DbContextOptions<SocialDB> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Wall> Wall { get; set; }

        public DbSet<ProfileData> ProfileData { get; set; }
        public DbSet<OptionData> OptionsData { get; set; }
        
    }
}
