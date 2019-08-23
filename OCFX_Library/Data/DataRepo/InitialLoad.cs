using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Linq;

namespace OCFX.Data.DataRepo
{
    public static class InitialLoad
    {
        public static void Initialize(OCFXContext context)
        {
            AddNerdery(context);
            AddFitness(context);
            AddFaq(context);
            AddSkills(context);
        }

        private static void AddSkills(OCFXContext context)
        {
            var SkillSet = new Skill[]
                        {
                new Skill("Hype Up",10,StyleType.Mental,5,TargetType.Self,EffectType.Hype, "Increases critical hit chance", "Makes you a loud asshole."),
                new Skill("Pump", 10, StyleType.Physical, 10,TargetType.Self, EffectType.Pump, "It pumps... you up.", "You're good for a while. Don't let it come down."),
                new Skill("High Volume Pump", 10, StyleType.Physical, 10,TargetType.Self, EffectType.Pump, "It pumps... you up.", "This is a status at the end of a workout. You'll be up for more than a minute; but the crash will be hard along with the DOMS."),
                        };
            foreach (var skill in SkillSet)
            {
                context.Skills.Add(skill);
            }
            context.SaveChanges();
        }

        private static void AddNerdery(OCFXContext context)
        {
            // Archetype/Classes
            if (!context.Archetypes.Any())
            {
                Archetype[] classes = new Archetype[]
                {
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Hobbyist, StrengthMod = 0, DexterityMod = 0, ConcentrationMod = 0, MotivationMod = 0, ConstitutionMod = 0, SpeedMod = 0,
                    Background = "They have no idea what they're doing and that's okay.",
                    Weakness = "None",
                    Strengths = "None"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Runner, StrengthMod = 1, DexterityMod = 1, ConcentrationMod = 1, MotivationMod = 1, ConstitutionMod = 1, SpeedMod = 3,
                    Background = "Gotta go fast",
                    Weakness = "Strength",
                    Strengths = "Speed"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Powerlifter, StrengthMod = 3, DexterityMod = 1, MotivationMod = 1, ConcentrationMod = 1, ConstitutionMod = 2, SpeedMod = 1,
                    Background = "It's not about the size; but rather how friggin' heavy it is.",
                    Weakness = "Strength and Constitution",
                    Strengths = "Speed"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Bodybuilder, StrengthMod = 3, DexterityMod = 1, MotivationMod = 2, ConcentrationMod = 1, ConstitutionMod = 2, SpeedMod = 1,
                    Background = "We're going to PUMP you UP.",
                    Weakness = "Speed",
                    Strengths = "Stregnth and Constitution"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Crossfit, StrengthMod = 2, DexterityMod = 1, MotivationMod = 3, ConcentrationMod = 1, ConstitutionMod = 1, SpeedMod = 1,
                    Background = "Did this person ever tell you about Crossfit? Well...",
                    Weakness = "Concentration",
                    Strengths = "Strength and Motivation"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Olympian, StrengthMod = 2, DexterityMod = 2, MotivationMod = 2, ConcentrationMod = 2, ConstitutionMod = 2, SpeedMod = 2,
                    Background = "Peak performance, gold standard.",
                    Weakness = "Constitution",
                    Strengths = "Dexterity and Concentration"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Fighter, StrengthMod = 2, DexterityMod = 2, MotivationMod = 1, ConcentrationMod = 2, ConstitutionMod = 1, SpeedMod = 2,
                    Background = "Trained from the early days to do one thing and one thing only: Kick ass.",
                    Weakness = "Speed",
                    Strengths = "Dexterity and Constitution" },
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Dancer, StrengthMod = 1, DexterityMod = 3, MotivationMod = 1, ConcentrationMod = 1, ConstitutionMod = 1, SpeedMod = 2,
                    Background = "Dance Dance Revolution",
                    Weakness = "Strength",
                    Strengths = "Dexterity and Speed"},
                new Archetype {SkillMod = SkillType.Basic, FitType = ClassType.Yoga, StrengthMod = 1, DexterityMod = 3, MotivationMod = 1, ConcentrationMod = 2, ConstitutionMod = 1, SpeedMod = 1,
                    Background = "You can't spit fire but you can still stretch harder than anyone else.",
                    Weakness = "Strength",
                    Strengths = "Dexterity and Concentration"}
                };
                for (int i = 0; i < classes.Length; i++)
                {
                    Archetype classy = classes[i];
                    context.Archetypes.Add(classy);
                }
                context.SaveChanges();

                //Diets
                if (!context.Diets.Any())
                {
                    Diet[] diets = new Diet[]
                    {
                        new Diet { DietName = "Mediterrean", Carbohydrates = 40, Protein = 40,  Fats = 20, DietTypeName = Diet.DietType.Maintenance},
                        new Diet { DietName = "Ketogenic", Carbohydrates = 5, Protein = 15,  Fats = 80, DietTypeName = Diet.DietType.FatLoss },
                        new Diet { DietName = "High Protein", Carbohydrates = 35, Protein = 45,  Fats = 20, DietTypeName = Diet.DietType.MassGain},
                    };
                    for (int i = 0; i < diets.Length; i++)
                    {
                        Diet diet = diets[i];
                        context.Diets.Add(diet);
                    }
                    context.SaveChanges();

                    // Campaigns
                    if (!context.Campaigns.Any())
                    {
                        Campaign[] campaigns = new Campaign[]
                        {
                            new Campaign("Tutorial",
                                         "This is just a tutorial level created to show you the ropes.",
                                         "This is how we get started; and everyone has to start from somewhere.",
                                         RiskLevel.Low,
                                         diets[0]),
                            new Campaign {
                                Name = "The Great Sheng Long",
                                Details = "Face death.",
                                Lore = "To move onward into the next tier, you must defeat thee great Sheng Long. He is a magnificent and deadly fighter capable of crushing boulders and breaking islands! A puny mortal such as yourself cannot stand in front of him at this level where you are; but with just enough training, you might have a chance - a rather slim chance, mind you - to survive.",
                                Risk = RiskLevel.EX,
                                Nutrition = diets[2]
                            }
                        };
                        for (int i = 0; i < campaigns.Length; i++)
                        {
                            Campaign quest = campaigns[i];
                            context.Campaigns.Add(quest);
                        }
                        context.SaveChanges();

                        // Exercises
                        if (!context.Exercises.Any())
                        {
                            Exercise[] exercises = new Exercise[]
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
                            for (int i = 0; i < exercises.Length; i++)
                            {
                                Exercise exercise = exercises[i];
                                context.Exercises.Add(exercise);
                            }

                            // Workouts
                            if (!context.Workouts.Any())
                            {
                                Workout[] lessons = new Workout[]
                                {
                                new Workout(
                                    "The Basics",
                                    "The first of many workouts to get someone started on their fitness journey.",
                                    45,
                                    Workout.WorkoutType.TotalBody),
                                new Workout(
                                    "The Ropes",
                                    "Something a little quicker, but harder",
                                    25,
                                    Workout.WorkoutType.TotalBody)
                                };
                                foreach (Workout lesson in lessons)
                                {
                                    context.Workouts.Add(lesson);
                                }
                                context.SaveChanges();

                                // Quests (Campaigns first)
                                CombineQuests(context);

                                // Workout Programs (Diets, Campaigns, Quests, Exercises, Workouts first)
                                CombineNerdery(context);
                            }
                        }
                    }
                }
            }
        }

        private static void CombineQuests(OCFXContext context)
        {
            if (!context.Quests.Any())
            {
                Quest[] quests = new Quest[]
                {
                new Quest{ QuestName = "Run the Grid", QuestStyle = QuestType.Speed, QuestStory = "There's someone in this area creating a ruckus. Run them down.", Campaign = context.Campaigns.Find(1)},
                new Quest{ QuestName = "Stomp the Grid", QuestStyle = QuestType.Power, QuestStory = "Oh great, you caught them! Now let's show them what you've got.", Campaign = context.Campaigns.Find(1)}
                };
                foreach (Quest quest in quests)
                {
                    context.Quests.Add(quest);
                }
                context.SaveChanges();
            }
        }

        private static void CombineNerdery(OCFXContext context)
        {
            if (!context.WorkoutPrograms.Any())
            {
                WorkoutProgram[] programs = new WorkoutProgram[]
                {
                    new WorkoutProgram{ WorkoutId = 1, ExerciseId = 1, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 1, ExerciseId = 2, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 1, ExerciseId = 3, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 1, ExerciseId = 4, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 1, ExerciseId = 5, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 2, ExerciseId = 1, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 2, ExerciseId = 2, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 2, ExerciseId = 3, Sets = 3, Repetitions=10 },
                    new WorkoutProgram{ WorkoutId = 2, ExerciseId = 4, Sets = 3, Repetitions=10 },
                };
                for (int i = 0; i < programs.Length; i++)
                {
                    WorkoutProgram program = programs[i];
                    context.WorkoutPrograms.Add(program);
                }
                context.SaveChanges();
            }
        }

        private static void AddFitness(OCFXContext context)
        {
            // Gyms
            if (!context.Gyms.Any())
            {
                Gym[] clubs = new Gym[]
                {
                    new Gym
                    {
                        Title = "Bar-Barian Lounge",
                        Description = "The Bar-Barian race has been long seen as the strongest \n and will never meet an equal.",
                        Status = ApprovalStatus.Approved,
                        MeetingDate = DayOfWeek.Saturday,
                        MeetingTime = DateTime.Now,
                        MeetingFrequency = Session.MeetingInterval.Weekly
                    },
                    new Gym
                    {
                        Title = "Pilate House",
                        Description = "It's a house where people do Pilates. What were you expecting?",
                        Status = ApprovalStatus.Approved,
                        MeetingDate = DayOfWeek.Saturday,
                        MeetingTime = DateTime.Now,
                        MeetingFrequency = Session.MeetingInterval.Weekly
                    }
                };
                foreach (Gym character in clubs)
                {
                    context.Gyms.Add(character);
                }
                context.SaveChanges();

                // Amenities
                if (context.GymAmenities.Any())
                {
                    return;
                }
                Equipment[] equipment = new Equipment[]
                {
                new Equipment { EquipName = "Pool", EquipDescription = "One of those things you swim in." },
                new Equipment { EquipName = "Sauna", EquipDescription = "To heat up and cool off at the same damn time." },
                new Equipment { EquipName = "Crossfit", EquipDescription = "I mean, someone's gotta talk about it." },
                };
                foreach (Equipment item in equipment)
                {
                    context.GymAmenities.Add(item);
                }
                context.SaveChanges();

                // Equipment Setup (Gym + Amenities first)
                if (!context.RelativeGyms.Any())
                {
                    GymRelation[] EquipmentRelation = new GymRelation[]
                    {
                        new GymRelation { EquipmentId = 1, GymId = 1 },
                        new GymRelation { EquipmentId = 2, GymId = 1 },
                        new GymRelation { EquipmentId = 3, GymId = 1 },
                        new GymRelation { EquipmentId = 1, GymId = 2 },
                        new GymRelation { EquipmentId = 2, GymId = 2 },
                        new GymRelation { EquipmentId = 3, GymId = 2 },
                    };
                    foreach (GymRelation item in EquipmentRelation)
                    {
                        context.RelativeGyms.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        private static void AddFaq(OCFXContext context)
        {
            // FAQs
            if (!context.FAQs.Any())
            {
                Facts[] faqs = new Facts[]
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
                foreach (Facts faq in faqs)
                {
                    context.FAQs.Add(faq);
                }
                context.SaveChanges();
            }
        }

    }
}
