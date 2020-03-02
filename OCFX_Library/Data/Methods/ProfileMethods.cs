using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Data.Methods
{
    public static class ProfileMethods
    {
        /// <summary>
        /// Gets a user profile with the provided user id
        /// </summary>
        /// <param name="context">Associated DBContext</param>
        /// <param name="id">The User Id</param>
        /// <returns></returns>
        public static async Task<ProfileSheet> GetProfileAsync(OCFXContext context, int? id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ProfileSheet profiler = await context.Profiles
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Comments)
                        .ThenInclude(p => p.Replies)
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Entry)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .Include(p => p.ClubMemberShip)
                    .ThenInclude(p => p.Club)
                .Include(p => p.Photos)
                .Include(p => p.FitStyle)
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
                .Include(p => p.Characters)
                    .ThenInclude(p => p.Quests)
                .Include(p => p.Weights)
                .Include(p => p.WorkoutHistory)
                .SingleOrDefaultAsync(m => m.Id == id);

            return profiler;
        }

        /// <summary>
        /// Gets a list of characters from the provided profile ID
        /// </summary>
        /// <param name="context">DB Context</param>
        /// <param name="id">Profile Id</param>
        /// <returns></returns>
        public static async Task<List<CharacterModel>> GetCharacterDataAsync(OCFXContext context, int? id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var characters = await context.Characters
               .Include(c => c.Avatars)
               .Include(c => c.Campaign)
               .Include(c => c.Quests)
               .Include(c => c.CharacterProfile)
               .Where(i => i.CharacterProfile.Id == id)
               .ToListAsync();

            return characters;
        }

        public static Photo GetProfilePhoto(OCFXContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var photo = context.Photos
                .OrderByDescending(d => d.DateAdded)
                .FirstOrDefault(c => c.Type == Photo.PhotoType.Profile
                           && c.ProfileId == id);

            return photo;
        }

        /// <summary>
        /// The synchronus version of GetProfileAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProfileSheet GetProfile(OCFXContext context, int? id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ProfileSheet profiler = context.Profiles
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Comments)
                        .ThenInclude(p => p.Replies)
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Entry)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .Include(p => p.ClubMemberShip)
                    .ThenInclude(p => p.Club)
                .Include(p => p.Photos)
                .Include(p => p.FitStyle)
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
                .Include(p => p.Characters)
                    .ThenInclude(p => p.Quests)
                .Include(p => p.Weights)
                .Include(p => p.WorkoutHistory)
                .SingleOrDefault(m => m.Id == id);

            return profiler;
        }

        public static async Task<ProfileSheet> GetProfileUser(OCFXContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ProfileSheet prof = await context.Profiles.SingleOrDefaultAsync(i => i.Id == id);

            return prof;
        }

        public static async Task<CharacterModel> GetCharacterProfile(OCFXContext context, int id)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var chara = await context.Characters.SingleOrDefaultAsync(i => i.Id == id);

            return chara;
        }

        /// <summary>
        /// Calculate the body fat percentage of the profile
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="height">Height in cm</param>
        /// <param name="weight">Weight in lbs.</param>
        /// <param name="neck">Neck Length in inches</param>
        /// <param name="waist">Waist Circumference in inches</param>
        /// <param name="hip">Hip Circumference in inches</param>
        /// <returns>double</returns>
        public static double BodyFat(ProfileSheet profile, int height, int weight, int? neck, int? waist, int? hip)
        {
            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            ProfileSheet profiler = profile;

            double percentage = 0.0;

            if (neck == null || waist == null || hip == null)
            {
                return 0.0;
            }

            if (neck == 0 || waist == 0 || hip == 0)
            {
                return 0.0;
            }

            // Check bone structures
            if (profiler.Gender == ProfileSheet.GenderSpectrum.CisMale ||
                profiler.Gender == ProfileSheet.GenderSpectrum.TransFemale ||
                profiler.Gender == ProfileSheet.GenderSpectrum.NotDisclosed)
            //{
            //    double f1 = (weight * 1.082) + 94.42;
            //    double? f2 = waist * 4.15;
            //    double? lbm = f1 - f2;
            //    double? bfw = weight - lbm;
            //    percentage = Convert.ToDouble((bfw / weight) * 100);
            //}
            {
                const double f1 = 495.0;
                double f2 = 1.0324 - (0.19077 * Math.Log10(Convert.ToDouble(waist - neck))) + (0.15456 * Math.Log10(height));
                const double f3 = 450.0;
                percentage = (f1 / f2) - f3;
            }

            if (profiler.Gender != ProfileSheet.GenderSpectrum.CisFemale &&
                profiler.Gender != ProfileSheet.GenderSpectrum.TransMale) return percentage;
            {
                double f1 = (weight * 0.732) + 8.987;
                const double f2 = 6 / 3.140;
                double? f3 = waist * 0.157;
                double? f4 = hip * 0.249;
                const double f5 = 9 * 0.434;
                double? lbm = f1 + f2 - f3 - f4 + f5;
                double? bfw = weight - lbm;
                percentage = Convert.ToDouble((bfw / weight) * 100);
            }

            return percentage;
        }

        public static Dictionary<int, string> Consultation(double bodyFat, double weight, double height)
        {
            string advice = "";
            string weightClass = "";
            double bmi = Math.Round((weight * 703) / Math.Pow(height, 2), 2);
            double bodyFatLeft = Math.Round(bodyFat);

            if (bmi > 30)
            {
                weightClass = "Obese";
                advice = "This is generally considered obese. " +
                    "Variations of this depends on whether or not your body fat percentage is above or below 20% which is the general average for most males and females. " +
                    "Best course of action if body fat percentage is above 20% is to decrease calorie intake and increase exercise/movement.";
            }
            if (29.9 > bmi && bmi > 25.0)
            {
                weightClass = "Overweight";
                advice = "This is generally considered overweight. " +
                    "Generally the advice here is decreased calorie intake and increased physical activity.";
            }
            if (24.9 > bmi && bmi > 18.5)
            {
                weightClass = "Normal";
                advice = "This is generally considered good. " +
                    "Generally the advice here is decreased calorie intake and increased physical activity.";
            }
            if (bmi < 18.4)
            {
                weightClass = "Underweight";
                advice = "This is generally considered underweight. " +
                    "Generally the advice here is increased calorie intake. " +
                    "With increased physical activity, a caloric surplus is necessary for muscle growth.";
            }

            var thing = new Dictionary<int, string>
            {
                { 1, weightClass },
                { 2, advice },
                { 3, bmi.ToString(CultureInfo.CurrentCulture) },
                { 4, bodyFatLeft.ToString(CultureInfo.CurrentCulture) }
            };
            return thing;
        }
    }
}
