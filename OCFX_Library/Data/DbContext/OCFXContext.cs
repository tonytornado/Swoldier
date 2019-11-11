using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCFX.DataModels;
using System;
using System.Linq;

namespace OCFX.Areas.Identity.Data
{
    public class OCFXContext : IdentityDbContext<OCFXUser, OCFXRole, Guid>
    {
        public OCFXContext(DbContextOptions<OCFXContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            base.OnModelCreating(builder);
            builder.Entity<FriendSheet>().HasKey(c => new { c.ProfileId, c.FriendId });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        // Site DB
        public DbSet<Facts> FAQs { get; set; }
        //public DbSet<Options> Options { get; set; }

        // Social DB 
        public DbSet<FriendSheet> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Shout> Messages { get; set; }
        public DbSet<Session> Events { get; set; }

        // Profile DB
        //public DbSet<Phone> Phones { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<ProfileSheet> Profiles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        // Fitness DB
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Equipment> GymAmenities { get; set; }
        public DbSet<GymRelation> RelativeGyms { get; set; }
        public DbSet<WeightMeasurement> Weights { get; set; }
        public DbSet<WorkoutSetLog> WorkoutSetLogs { get; set; }
        public DbSet<MessageBoardPost> MessageBoardPosts { get; set; }
        public DbSet<MessageBoardComment> MessageBoardComments { get; set; }
        
        //public DbSet<WorkoutProgramLog> WorkoutLogs { get; set; }

        // Quest DB
        public DbSet<Archetype> Archetypes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestLog> QuestLogs { get; set; }
        public DbSet<BossEncounter> Bosses { get; set; }
        public DbSet<PersonalEncounter> Minions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }
    }
}
