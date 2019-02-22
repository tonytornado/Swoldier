using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
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
                .Include(p => p.Posts).ThenInclude(p => p.Comments).ThenInclude(p => p.Replies)
				.Include(p => p.Followers)
				.Include(p => p.Following)
                .Include(p => p.Gym)
				.Include(p => p.Photos)
				.Include(p => p.FitStyle)
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
				.Include(p => p.Quest)
				.Include(p => p.Campaign)
					.ThenInclude(p => p.CampaignDiet)
				.Include(p => p.Campaign)
					.ThenInclude(p => p.CampaignQuest)
				.SingleOrDefaultAsync(m => m.Id == id);

			return Profiler;
		}

		/// <summary>
		/// Snags the profile photo.
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="context"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public static async Task<Photo> GetProfilePhoto(OCFXContext context, int id)
		{
			var ProfilePhoto = await context.Photos
					.OrderByDescending(d => d.DateAdded)
					.FirstOrDefaultAsync(i => i.ProfileId == id);

			return ProfilePhoto;
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
			var Profiler = profile;

			var percentage = 0.0;

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
			{
				double f1 = (weight * 1.082) + 94.42;
				double? f2 = waist * 4.15;
				double? lbm = f1 - f2;
				double? bfw = weight - lbm;
				percentage = Convert.ToDouble((bfw / weight) * 100);
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
	}
}
