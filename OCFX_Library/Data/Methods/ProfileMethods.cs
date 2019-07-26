using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Data.Methods
{
    public class ProfileMethods
    {
        /// <summary>
        /// Gets a user profile with the provided user id
        /// </summary>
        /// <param name="context">Associated DBContext</param>
        /// <param name="id">The User Id</param>
        /// <returns></returns>
        public static async Task<Profile> GetProfileAsync(OCFXContext context, int? id)
        {
            Profile Profiler = await context.Profiles
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Comments)
                        .ThenInclude(p => p.Replies)
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Entry)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .Include(p => p.Gym)
                    .ThenInclude(p => p.Club)
                .Include(p => p.Photos)
                .Include(p => p.FitStyle)
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
                .Include(p => p.Quest)
                .Include(p => p.Campaign)
                    .ThenInclude(p => p.Nutrition)
                .Include(p => p.Campaign)
                    .ThenInclude(p => p.Quests)
                .Include(p => p.Weights)
                .SingleOrDefaultAsync(m => m.Id == id);

            return Profiler;
        }

        public static Photo GetProfilePhoto(OCFXContext context, int id)
        {
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
        public static Profile GetProfile(OCFXContext context, int? id)
        {
            Profile Profiler = context.Profiles
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Comments)
                        .ThenInclude(p => p.Replies)
                .Include(p => p.Posts)
                    .ThenInclude(p => p.Entry)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .Include(p => p.Gym)
                    .ThenInclude(p => p.Club)
                .Include(p => p.Photos)
                .Include(p => p.FitStyle)
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
                .Include(p => p.Quest)
                .Include(p => p.Campaign)
                    .ThenInclude(p => p.Nutrition)
                .Include(p => p.Campaign)
                    .ThenInclude(p => p.Quests)
                .SingleOrDefault(m => m.Id == id);

            return Profiler;
        }

        public static async Task<Profile> GetProfileUser(OCFXContext context, int id)
        {
            Profile Poster = await context.Profiles.SingleOrDefaultAsync(i => i.Id == id);

            return Poster;
        }

        /// <summary>
        /// Calculate the body fat percentage of the profile
        /// </summary>
        /// <param name="height">Height in cm</param>
        /// <param name="weight">Weight in lbs.</param>
        /// <param name="neck">Neck Length in inches</param>
        /// <param name="waist">Waist Circumference in inches</param>
        /// <param name="hip">Hip Circumference in inches</param>
        /// <returns>double</returns>
        public static double BodyFat(Profile profile, int height, int weight, int? neck, int? waist, int? hip)
        {
            Profile Profiler = profile;

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
            if (Profiler.Gender == Profile.GenderSpectrum.CisMale ||
                Profiler.Gender == Profile.GenderSpectrum.TransFemale ||
                Profiler.Gender == Profile.GenderSpectrum.NotDisclosed)
            //{
            //    double f1 = (weight * 1.082) + 94.42;
            //    double? f2 = waist * 4.15;
            //    double? lbm = f1 - f2;
            //    double? bfw = weight - lbm;
            //    percentage = Convert.ToDouble((bfw / weight) * 100);
            //}
            {
                double f1 = 495.0;
                double f2 = 1.0324 - (0.19077 * Math.Log10(Convert.ToDouble(waist - neck))) + (0.15456 * Math.Log10(height));
                double f3 = 450.0;
                percentage = (f1 / f2) - f3;
            }

            if (Profiler.Gender == Profile.GenderSpectrum.CisFemale ||
                Profiler.Gender == Profile.GenderSpectrum.TransMale)
            {
                double f1 = (weight * 0.732) + 8.987;
                double f2 = 6 / 3.140;
                double? f3 = waist * 0.157;
                double? f4 = hip * 0.249;
                double f5 = 9 * 0.434;
                double? lbm = f1 + f2 - f3 - f4 + f5;
                double? bfw = weight - lbm;
                percentage = Convert.ToDouble((bfw / weight) * 100);
            }

            return percentage;
        }

        public static Dictionary<int, string> Consultation(double bodyFat, double weight, double height)
        {
            string Advice = "";
            string WeightClass = "";
            double BMI = Math.Round((weight * 703) / Math.Pow(height, 2), 2);
            double BodyFatLeft = Math.Round(bodyFat);

            if (BMI > 30)
            {
                WeightClass = "Obese";
                Advice = "This is generally considered obese. " +
                    "Variations of this depends on whether or not your body fat percentage is above or below 20% which is the general average for most males and females. " +
                    "Best course of action if body fat percentage is above 20% is to decrease calorie intake and increase exercise/movement.";
            }
            if (29.9 > BMI && BMI > 25.0)
            {
                WeightClass = "Overweight";
                Advice = "This is generally considered overweight. " +
                    "Generally the advice here is decreased calorie intake and increased physical activity.";
            }
            if (24.9 > BMI && BMI > 18.5)
            {
                WeightClass = "Normal";
                Advice = "This is generally considered good. " +
                    "Generally the advice here is decreased calorie intake and increased physical activity.";
            }
            if (BMI < 18.4)
            {
                WeightClass = "Underweight";
                Advice = "This is generally considered underweight. " +
                    "Generally the advice here is increased calorie intake. " +
                    "With increased physical activity, a caloric surplus is necessary for muscle growth.";
            }

            var thing = new Dictionary<int, string>
            {
                { 1, WeightClass },
                { 2, Advice },
                { 3, BMI.ToString() },
                { 4, BodyFatLeft.ToString() }
            };
            return thing;
        }
    }
}
