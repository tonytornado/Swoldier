﻿using System;
using OCFX.DataModels;
using System.Linq;
using OCFX.Areas.Identity.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OCFX.Data.DataModels.SiteModels;

namespace OCFX.Data.DataRepo
{
	public static class InitialLoad
	{
		public static void Initialize(OCFXContext context)
		{
			//var context = serviceProvider.GetRequiredService<OCFXContext>();
			//context.Database.EnsureCreated();

			// Exercises
			if (context.Exercises.Any())
			{
				return;
			}
			var exercises = new Exercise[]
			{
				new Exercise { ExName = "Squats", ExGroup = Workout.WorkoutType.Legs, ExType = Exercise.ExerciseType.Strength},
				new Exercise { ExName = "Straight-Leg Deadlift", ExGroup = Workout.WorkoutType.Legs, ExType = Exercise.ExerciseType.Strength},
				new Exercise { ExName = "Bench Press", ExGroup = Workout.WorkoutType.Chest, ExType = Exercise.ExerciseType.Strength},
				new Exercise { ExName = "Pull-Up", ExGroup = Workout.WorkoutType.Back, ExType = Exercise.ExerciseType.Strength},
				new Exercise { ExName = "Overhead Press", ExGroup = Workout.WorkoutType.Shoulders, ExType = Exercise.ExerciseType.Strength}

			};
			foreach (var exercise in exercises)
			{
				context.Exercises.Add(exercise);
			}

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
					DateAdded = DateTime.Now
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
				new WorkoutProgram{ WorkoutId = 1, ExerciseId = 1},
				new WorkoutProgram{ WorkoutId = 1, ExerciseId = 2},
				new WorkoutProgram{ WorkoutId = 1, ExerciseId = 3},
				new WorkoutProgram{ WorkoutId = 1, ExerciseId = 4},
				new WorkoutProgram{ WorkoutId = 1, ExerciseId = 5},
				new WorkoutProgram{ WorkoutId = 2, ExerciseId = 1},
				new WorkoutProgram{ WorkoutId = 2, ExerciseId = 2},
				new WorkoutProgram{ WorkoutId = 2, ExerciseId = 3},
				new WorkoutProgram{ WorkoutId = 2, ExerciseId = 4}
			};
			foreach (var program in programs)
			{
				context.WorkoutPrograms.Add(program);
			}
			context.SaveChanges();

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

			// Gyms
			if (context.Gyms.Any())
			{
				return;
			}
			var clubs = new Gym[]
			{
				new Gym{ Title = "Bar-Barian Lounge", Description = "The Bar-Barian race has been long seen as the strongest \n and will never meet an equal.", Leader = "Alfred Calcutta"
				}
			};
			foreach (var character in clubs)
			{
				context.Gyms.Add(character);
			}
			context.SaveChanges();

			// Campaigns
			if (context.Campaigns.Any())
			{
				return;
			}
			var campaigns = new Campaign[]
			{
				new Campaign { CampaignName = "Tutorial", CampaignDetails = "This is just a tutorial level created to show you the ropes.", CampaignLore = "This is how we get started; and everyone has to start from somewhere.", CampaignRisk = RiskLevel.Low, DietId = 1 },
				new Campaign { CampaignName = "The Great Sheng Long", CampaignDetails = "Face death.", CampaignLore = "To move onward into the next tier, you must defeat thee great Sheng Long. He is a magnificent and deadly fighter capable of crushing boulders and breaking islands! A puny mortal such as yourself cannot stand in front of him at this level where you are; but with just enough training, you might have a chance - a rather slim chance, mind you - to survive.", CampaignRisk = RiskLevel.EX, DietId = 3 }
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

			// FAQs
			if (context.FAQs.Any())
			{
				return;
			}
			var faqs = new Facts[]
			{
				new Facts{ Question = "What exactly is this place?", Answer = "This is a D&D site masquerading as a coaching area for fitness but it isn't all that completed quite yet?", Section = SectionName.Main },
				new Facts{ Question = "What do you do here?", Answer = "We hook you up with others like you looking to get into your new fitness habit by making it easier to jump in with guided quests and DM's who know a thing or two about throwing weights around.", Section = SectionName.Main },
				new Facts{ Question = "What's an Alpha?", Answer = "Obviously, it's before a beta. We still have some kinks to fix but you're invited to come in and wreck some shit up.", Section = SectionName.Site }
			};
			foreach (var faq in faqs)
			{
				context.FAQs.Add(faq);
			}
			context.SaveChanges();
		}
	}
}
