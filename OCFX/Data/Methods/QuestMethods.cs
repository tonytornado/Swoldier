using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX.Data.Methods
{
	public class QuestMethods
	{
        /// <summary>
        /// Checks the completed quest log for all the quests a user has completed.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<int> CheckCompletedQuests(OCFXContext context, int UserId)
        {
            int QuestGoer = UserId;
            IQueryable<int> ops = from q in context.Quests
                                  join ql in context.QuestLogs on q.Id equals ql.QuestId
                                  where ql.ProfileId == UserId
                                  where ql.Completed == true
                                  select ql.QuestId;

            return ops.ToList();
        }

        /// <summary>
        /// Enables the start of a quest. 
        /// A quest must be a part of a campaign; but can also be a part of a sidequest.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="QuestId">Quest's Id</param>
        /// <param name="UserId">User's Id</param>
        public static void JoinQuest(OCFXContext context, int QuestId, int UserId)
		{
			int QuestGoer = UserId;
			Quest quest = context.Quests.SingleOrDefault(q => q.Id == QuestId);

			// Create the new quest log
			QuestLog challenger = new QuestLog()
			{
				ProfileId = UserId,
				QuestId = QuestId,
				Completed = false,
                CampaignId = quest.CampaignId
			};
			context.QuestLogs.Add(challenger);
			context.SaveChanges();

            Profile profile = context.Profiles.SingleOrDefault(p => p.Id == UserId);
            profile.Quest = quest;
            context.SaveChanges();
		}

		/// <summary>
		/// Completes a quest provided they meet certain conditions.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="QuestId">Quest's Id</param>
		/// <param name="UserId">User's Id</param>
		public static void CompleteQuest(OCFXContext context, int QuestId, int UserId)
		{
			int QuestGoer = UserId;
			QuestLog quest = context.QuestLogs.SingleOrDefault(q => q.QuestId == QuestId && q.ProfileId == QuestGoer);

			bool QuestCheck = CheckForCampaign(context, QuestGoer, quest.CampaignId);
			if (QuestCheck != true)
			{
				throw new Exception("Yeah, they didn't complete the quest, fall back.");
			}

			// Set the quest as complete.
			quest.Completed = true;
			
			context.SaveChanges();
		}

		/// <summary>
		/// Makes a quick check for the right campaign to prevent cheating. This is an ANTI-CHEAT measure.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>Boolean</returns>
		private static bool CheckForCampaign(OCFXContext context, int x, int y)
		{
			Profile player = context.Profiles.SingleOrDefault(p => p.Id == x);
			Quest campaignQuests = context.Quests.Include(q => q.Campaign).SingleOrDefault(p => p.CampaignId == y);

			if(campaignQuests == null)
			{
				throw new Exception("This is bogus. They didn't complete anything.");
			}

			return true;
		}
	}
}
