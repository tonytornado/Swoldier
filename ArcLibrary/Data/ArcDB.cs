using Microsoft.EntityFrameworkCore;
using ArcLibrary.DataModels.CharacterModels;

namespace ArcLibrary.Data
{
    public class ArcDB : DbContext
    {
        public ArcDB (DbContextOptions<ArcDB> options)
            : base(options)
        {
        }

        public DbSet<Sheet> Sheet { get; set; }
        public DbSet<Mod> Mod { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Trait> Trait { get; set; }
    }
}
