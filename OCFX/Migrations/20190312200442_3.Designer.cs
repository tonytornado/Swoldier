﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OCFX.Areas.Identity.Data;

namespace OCFX.Migrations
{
    [DbContext(typeof(OCFXContext))]
    [Migration("20190312200442_3")]
    partial class _3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OCFX.Areas.Identity.Data.OCFXRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SiteModels.Facts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<string>("Question");

                    b.Property<int>("Section");

                    b.HasKey("Id");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.Friend", b =>
                {
                    b.Property<int>("ProfileId");

                    b.Property<int>("FriendId");

                    b.Property<int>("ActionUserId");

                    b.Property<int>("FriendshipConfirmer");

                    b.Property<DateTime?>("FriendshipStart");

                    b.HasKey("ProfileId", "FriendId");

                    b.HasAlternateKey("FriendId", "ProfileId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.MessageBoardComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoardId");

                    b.Property<int>("BoardPostId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("BoardPostId");

                    b.HasIndex("ProfileId");

                    b.ToTable("MessageBoardComments");
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.MessageBoardPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoardId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ProfileId");

                    b.ToTable("MessageBoardPosts");
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.Shout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChainIdentifier");

                    b.Property<DateTime?>("DateOpened");

                    b.Property<DateTime>("DateSent");

                    b.Property<Guid>("Identifier");

                    b.Property<string>("MessageText");

                    b.Property<int>("ReceiverId");

                    b.Property<int>("SenderId");

                    b.Property<int>("Status");

                    b.Property<string>("SubjectText");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("OCFX.DataModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressTypeName");

                    b.Property<string>("CityName");

                    b.Property<int?>("ProfileId");

                    b.Property<string>("StateName");

                    b.Property<string>("StreetName");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OCFX.DataModels.Archetype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConcentrationMod");

                    b.Property<int>("ConstitutionMod");

                    b.Property<int>("DexterityMod");

                    b.Property<int>("FitType");

                    b.Property<int>("MotivationMod");

                    b.Property<int>("SkillMod");

                    b.Property<int>("SpeedMod");

                    b.Property<int>("StrengthMod");

                    b.HasKey("Id");

                    b.ToTable("Archetypes");
                });

            modelBuilder.Entity("OCFX.DataModels.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CampaignDetails");

                    b.Property<string>("CampaignLore");

                    b.Property<string>("CampaignName");

                    b.Property<int>("CampaignRisk");

                    b.Property<int>("DietId");

                    b.HasKey("Id");

                    b.HasIndex("DietId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("OCFX.DataModels.Diet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Carbohydrates");

                    b.Property<string>("DietName");

                    b.Property<int>("DietTypeName");

                    b.Property<int>("Fats");

                    b.Property<int>("Protein");

                    b.HasKey("Id");

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("OCFX.DataModels.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EquipDescription");

                    b.Property<string>("EquipName");

                    b.HasKey("Id");

                    b.ToTable("GymAmenities");
                });

            modelBuilder.Entity("OCFX.DataModels.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("ExerType");

                    b.Property<string>("Name");

                    b.Property<int>("TargetedMuscles");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("OCFX.DataModels.Gym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("LeaderId");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("OCFX.DataModels.GymRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EquipmentId");

                    b.Property<int>("GymId");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("GymId");

                    b.ToTable("RelativeGyms");
                });

            modelBuilder.Entity("OCFX.DataModels.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClubId");

                    b.Property<DateTime>("JoinDate");

                    b.Property<int?>("MemberId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("MemberId")
                        .IsUnique()
                        .HasFilter("[MemberId] IS NOT NULL");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("OCFX.DataModels.OCFXUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime>("NameChangedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("ProfileId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OCFX.DataModels.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaCode");

                    b.Property<int>("PhoneNumber");

                    b.Property<int>("PhoneTypeName");

                    b.Property<int?>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("OCFX.DataModels.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption");

                    b.Property<DateTime>("DateAdded");

                    b.Property<int>("ProfileId");

                    b.Property<int>("Type");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("OCFX.DataModels.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("BackStory");

                    b.Property<int?>("CampaignId");

                    b.Property<int?>("ClassId");

                    b.Property<int>("ConcentrationStat");

                    b.Property<int>("ConstitutionStat");

                    b.Property<int>("DexterityStat");

                    b.Property<string>("DriveStory");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("Goals");

                    b.Property<int>("Height");

                    b.Property<int?>("HipMeasurement");

                    b.Property<string>("LastName");

                    b.Property<int>("MotivationStat");

                    b.Property<int?>("NeckMeasurement");

                    b.Property<int?>("QuestId");

                    b.Property<int>("SpeedStat");

                    b.Property<int>("StrengthStat");

                    b.Property<int?>("WaistMeasurement");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("ClassId");

                    b.HasIndex("QuestId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("OCFX.DataModels.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampaignId");

                    b.Property<string>("QuestName");

                    b.Property<string>("QuestStory");

                    b.Property<int>("QuestStyle");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("OCFX.DataModels.QuestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampaignId");

                    b.Property<bool>("Completed");

                    b.Property<int>("ProfileId");

                    b.Property<int>("QuestId");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("QuestId");

                    b.ToTable("QuestLogs");
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("EntryId");

                    b.Property<int>("PostId");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("EntryId");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Reply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<int>("EntryId");

                    b.Property<int>("ProfileId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("EntryId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("OCFX.DataModels.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<int>("Duration");

                    b.Property<int>("TargetedMuscles");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("OCFX.DataModels.WorkoutProgram", b =>
                {
                    b.Property<int>("WorkoutProgramId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampaignId");

                    b.Property<int>("ExerciseId");

                    b.Property<int>("Order");

                    b.Property<int>("Repetitions");

                    b.Property<int>("Sets");

                    b.Property<int>("WorkoutId");

                    b.HasKey("WorkoutProgramId");

                    b.HasIndex("CampaignId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutPrograms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("OCFX.Areas.Identity.Data.OCFXRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("OCFX.DataModels.OCFXUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("OCFX.DataModels.OCFXUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("OCFX.Areas.Identity.Data.OCFXRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.OCFXUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("OCFX.DataModels.OCFXUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.Friend", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Following")
                        .WithMany("Following")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.MessageBoardComment", b =>
                {
                    b.HasOne("OCFX.DataModels.Gym", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.Data.DataModels.SocialModels.MessageBoardPost", "BoardPost")
                        .WithMany("MessageBoardComments")
                        .HasForeignKey("BoardPostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.MessageBoardPost", b =>
                {
                    b.HasOne("OCFX.DataModels.Gym", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.Data.DataModels.SocialModels.Shout", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Address", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany("Addresses")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Campaign", b =>
                {
                    b.HasOne("OCFX.DataModels.Diet", "CampaignDiet")
                        .WithMany()
                        .HasForeignKey("DietId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Gym", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.GymRelation", b =>
                {
                    b.HasOne("OCFX.DataModels.Equipment", "Equipment")
                        .WithMany("Gyms")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Gym", "Gym")
                        .WithMany("Amenities")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Membership", b =>
                {
                    b.HasOne("OCFX.DataModels.Gym", "Club")
                        .WithMany("Members")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Member")
                        .WithOne("Gym")
                        .HasForeignKey("OCFX.DataModels.Membership", "MemberId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.OCFXUser", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithOne("FitUser")
                        .HasForeignKey("OCFX.DataModels.OCFXUser", "ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Phone", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany("Phones")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Photo", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Photos")
                        .WithMany("Photos")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Profile", b =>
                {
                    b.HasOne("OCFX.DataModels.Campaign", "Campaign")
                        .WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Archetype", "FitStyle")
                        .WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Quest", "Quest")
                        .WithMany("CurrentPlayers")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.Quest", b =>
                {
                    b.HasOne("OCFX.DataModels.Campaign", "Campaign")
                        .WithMany("CampaignQuest")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.QuestLog", b =>
                {
                    b.HasOne("OCFX.DataModels.Campaign", "Campaign")
                        .WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Quest", "Quest")
                        .WithMany()
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Comment", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.SocialModels.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Post", b =>
                {
                    b.HasOne("OCFX.DataModels.Profile", "Entry")
                        .WithMany("Entries")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany("Posts")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.SocialModels.Reply", b =>
                {
                    b.HasOne("OCFX.DataModels.SocialModels.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OCFX.DataModels.WorkoutProgram", b =>
                {
                    b.HasOne("OCFX.DataModels.Campaign", "Campaign")
                        .WithMany("CampaignPrograms")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OCFX.DataModels.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
