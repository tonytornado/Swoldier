using Microsoft.EntityFrameworkCore;
using FitLibrary.Models.Fit;
using FitLibrary.Models.Community;

namespace FitLibrary.Data
{
    public class FitDB : DbContext
    {
        public FitDB (DbContextOptions<FitDB> options)
            : base(options)
        {
        }

        public DbSet<ClubBoard> ForumBoards { get; set; }
        public DbSet<ClubPosts> ForumPosts { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Workout> Workout { get; set; }
    }
}
