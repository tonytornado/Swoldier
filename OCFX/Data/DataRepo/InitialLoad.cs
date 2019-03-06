using System;
using OCFX.DataModels;
using System.Linq;
using OCFX.Areas.Identity.Data;
using OCFX.Data.DataModels.SiteModels;
using System.Collections.Generic;

namespace OCFX.Data.DataRepo
{
    public static class InitialLoad
    {
        public static void Initialize(OCFXContext context)
        {
            //var context = serviceProvider.GetRequiredService<OCFXContext>();
            //context.Database.EnsureCreated();

            

            // Archetype/Classes
            if (context.Archetypes.Any())
            {
                return;
            }
            var classes = new Archetype[]
            {
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Hobbyist, StrengthMod = 0, DexterityMod = 0, ConcentrationMod = 0, MotivationMod = 0, ConstitutionMod = 0, SpeedMod = 0 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Runner, StrengthMod = 1, DexterityMod = 1, ConcentrationMod = 1, MotivationMod = 1, ConstitutionMod = 1, SpeedMod = 3 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Powerlifter, StrengthMod = 3, DexterityMod = 1, MotivationMod = 1, ConcentrationMod = 1, ConstitutionMod = 2, SpeedMod = 1 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Bodybuilder, StrengthMod = 3, DexterityMod = 1, MotivationMod = 2, ConcentrationMod = 1, ConstitutionMod = 2, SpeedMod = 1 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Crossfit, StrengthMod = 2, DexterityMod = 1, MotivationMod = 3, ConcentrationMod = 1, ConstitutionMod = 1, SpeedMod = 1 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Olympian, StrengthMod = 2, DexterityMod = 2, MotivationMod = 2, ConcentrationMod = 2, ConstitutionMod = 2, SpeedMod = 2 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Fighter, StrengthMod = 2, DexterityMod = 2, MotivationMod = 1, ConcentrationMod = 2, ConstitutionMod = 1, SpeedMod = 2 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Dancer, StrengthMod = 1, DexterityMod = 3, MotivationMod = 1, ConcentrationMod = 1, ConstitutionMod = 1, SpeedMod = 2 },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Yoga, StrengthMod = 1, DexterityMod = 3, MotivationMod = 1, ConcentrationMod = 2, ConstitutionMod = 1, SpeedMod = 1 }
            };
            foreach (var classy in classes)
            {
                context.Archetypes.Add(classy);
            }
            context.SaveChanges();

            //Diets
            if (context.Diets.Any())
            {
                return;
            }
            var diets = new Diet[]
            {
                new Diet { DietName = "Mediterrean", Carbohydrates = 40, Protein = 40,  Fats = 20, DietTypeName = Diet.DietType.Maintenance},
                new Diet { DietName = "Ketogenic", Carbohydrates = 5, Protein = 15,  Fats = 80, DietTypeName = Diet.DietType.FatLoss },
                new Diet { DietName = "High Protein", Carbohydrates = 35, Protein = 45,  Fats = 20, DietTypeName = Diet.DietType.MassGain},
            };
            foreach (var diet in diets)
            {
                context.Diets.Add(diet);
            }
            context.SaveChanges();

            // Campaigns
            if (context.Campaigns.Any())
            {
                return;
            }
            var campaigns = new Campaign[]
            {
                new Campaign
                {
                    CampaignName = "Tutorial",
                    CampaignDetails = "This is just a tutorial level created to show you the ropes.",
                    CampaignLore = "This is how we get started; and everyone has to start from somewhere.",
                    CampaignRisk = RiskLevel.Low, DietId = 1,
                },
                new Campaign {
                    CampaignName = "The Great Sheng Long",
                    CampaignDetails = "Face death.",
                    CampaignLore = "To move onward into the next tier, you must defeat thee great Sheng Long. He is a magnificent and deadly fighter capable of crushing boulders and breaking islands! A puny mortal such as yourself cannot stand in front of him at this level where you are; but with just enough training, you might have a chance - a rather slim chance, mind you - to survive.",
                    CampaignRisk = RiskLevel.EX,
                    DietId = 3
                }
            };
            foreach (var quest in campaigns)
            {
                context.Campaigns.Add(quest);
            }
            context.SaveChanges();

            // Quests
            if (context.Quests.Any())
            {
                return;
            }
            var quests = new Quest[]
            {
                new Quest{ QuestName = "Run the Grid", QuestStyle = QuestType.Speed, QuestStory = "There's someone in this area creating a ruckus. Run them down.", CampaignId = 1},
                new Quest{ QuestName = "Stomp the Grid", QuestStyle = QuestType.Power, QuestStory = "Oh great, you caught them! Now let's show them what you've got.", CampaignId = 1}
            };
            foreach (var quest in quests)
            {
                context.Quests.Add(quest);
            }
            context.SaveChanges();

            // Exercises
            if (context.Exercises.Any())
            {
                return;
            }
            var exercises = new Exercise[]
            {
                new Exercise
                {
                    Name = "Squats",
                    TargetedMuscles = Workout.WorkoutType.Legs,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "A basic lower body squat using your legs and hip drive."
                },
                new Exercise
                {
                    Name = "Straight-Leg Deadlift",
                    TargetedMuscles = Workout.WorkoutType.Legs,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "Pull weight up, put weight down. Don't move your hips or legs, and straighten out your back!"
                },
                new Exercise
                {
                    Name = "Bench Press",
                    TargetedMuscles = Workout.WorkoutType.Chest,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "Sit on the bench, lie flat on the bench on your back, push the weight up, let it back down, push it back up, repeat."
                },
                new Exercise
                {
                    Name = "Pull-Up",
                    TargetedMuscles = Workout.WorkoutType.Back,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "Pull your body up to on bar."
                },
                new Exercise
                {
                    Name = "Overhead Press",
                    TargetedMuscles = Workout.WorkoutType.Shoulders,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "Push some weigh over your shoulder."
                },
                new Exercise
                {
                    Name = "Face-pull",
                    TargetedMuscles = Workout.WorkoutType.Back,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "Take a bola string and pull it to your face from a high cable pull. Yeah, now do that again."
                },
                new Exercise
                {
                    Name = "Incline Bench Press",
                    TargetedMuscles = Workout.WorkoutType.Chest,
                    ExerType = Exercise.ExerciseType.Strength,
                    Description = "It's a bench press only slanted upwards, you know, to make it tougher."
                },


            };
            foreach (var exercise in exercises)
            {
                context.Exercises.Add(exercise);
            }

            //Workouts
            if (context.Workouts.Any())
            {
                return;
            }
            var lessons = new Workout[]
            {
                new Workout {
                    Title = "The Basics",
                    Description = "The first of many workouts to get someone started on their fitness journey.",
                    Duration = 45,
                    TargetedMuscles = Workout.WorkoutType.TotalBody,
                    DateAdded = DateTime.Now,
                },
                new Workout {
                    Title = "The Ropes",
                    Description = "Something a little quicker, but harder",
                    Duration = 25,
                    TargetedMuscles = Workout.WorkoutType.TotalBody,
                    DateAdded = DateTime.Now
                }
            };
            foreach (var lesson in lessons)
            {
                context.Workouts.Add(lesson);
            }
            context.SaveChanges();

            // Workout Programs
            if (context.WorkoutPrograms.Any())
            {
                return;
            }
            var programs = new WorkoutProgram[]
            {
                new WorkoutProgram{ WorkoutId = 1, ExerciseId = 1, CampaignId = 1, Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 1, ExerciseId = 2, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 1, ExerciseId = 3, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 1, ExerciseId = 4, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 1, ExerciseId = 5, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 2, ExerciseId = 1, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 2, ExerciseId = 2, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 2, ExerciseId = 3, CampaignId = 1,  Sets = 3, Repetitions=10 },
                new WorkoutProgram{ WorkoutId = 2, ExerciseId = 4, CampaignId = 1,  Sets = 3, Repetitions=10 },
            };
            foreach (var program in programs)
            {
                context.WorkoutPrograms.Add(program);
            }
            context.SaveChanges();

            // Gyms
            if (context.Gyms.Any())
            {
                return;
            }
            var clubs = new Gym[]
            {
                new Gym
                {
                    Title = "Bar-Barian Lounge",
                    Description = "The Bar-Barian race has been long seen as the strongest \n and will never meet an equal.",
                },
                new Gym
                {
                    Title = "Pilate House",
                    Description = "It's a house where people do Pilates. What were you expecting?"
                }
            };
            foreach (var character in clubs)
            {
                context.Gyms.Add(character);
            }
            context.SaveChanges();

            // FAQs
            if (context.FAQs.Any())
            {
                return;
            }
            var faqs = new Facts[]
            {
                new Facts
                {
                    Question = "What exactly is this place?",
                    Answer = "This is a D&D site masquerading as a coaching area for fitness but it isn't all that completed quite yet?",
                    Section = SectionName.Main
                },
                new Facts
                {
                    Question = "What do you do here?",
                    Answer = "We hook you up with others like you looking to get into your new fitness habit by making it easier to jump in with guided quests and DM's who know a thing or two about throwing weights around.",
                    Section = SectionName.Main
                },
                new Facts
                {
                    Question = "What's an Alpha?",
                    Answer = "Obviously, it's before a beta. We still have some kinks to fix but you're invited to come in and wreck some shit up.",
                    Section = SectionName.Site
                },
                new Facts
                {
                    Question = "How do you keep the peace?",
                    Answer = "We, uh, have a guy. He beats the asses of those that need to get beat.",
                    Section = SectionName.Community
                }
            };
            foreach (var faq in faqs)
            {
                context.FAQs.Add(faq);
            }
            context.SaveChanges();
        }
    }
}
