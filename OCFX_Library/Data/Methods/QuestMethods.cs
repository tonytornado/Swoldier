using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX.Data.Methods
{
    public static class QuestMethods
    {
        /// <summary>
        /// Checks the completed quest log for all the quests a user has completed.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<int> CheckCompletedQuests(OCFXContext context, int UserId)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            int QuestGoer = UserId;
            IQueryable<int> ops = from q in context.Quests
                                  join ql in context.QuestLogs on q.Id equals ql.Quest.Id
                                  where ql.Character.Id == UserId
                                  where ql.Completed == true
                                  select ql.Quest.Id;

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
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            int QuestGoer = UserId;
            Quest quest = context.Quests.SingleOrDefault(q => q.Id == QuestId);

            // Create the new quest log
            QuestLog challenger = new QuestLog()
            {
                Character = context.Characters.SingleOrDefault(c => c.Id == UserId),
                Quest = quest,
                Completed = false,
                Campaign = quest.Campaign
            };
            context.QuestLogs.Add(challenger);
            context.SaveChanges();

            var chara = context.Characters.Include(q => q.Quests).SingleOrDefault(p => p.Id == UserId);
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
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            int QuestGoer = UserId;
            QuestLog CompletedQuest = context.QuestLogs.SingleOrDefault(q => q.Quest.Id == QuestId && q.Character.Id == QuestGoer);

            // Check if the quest is actually part of the campaign
            bool QuestCheck = CheckForCampaign(context, QuestGoer, CompletedQuest.Campaign.Id);
            if (QuestCheck != true)
            {
                throw new Exception("Yeah, they didn't complete the quest, fall back.");
            }

            // Set the quest as complete.
            CompletedQuest.Completed = true;
            context.SaveChanges();

            CharacterModel completionist = context.Characters.Include(q => q.Quests).SingleOrDefault(q => q.Id == UserId);
            completionist.Quests = null;
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
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            CharacterModel player = context.Characters.SingleOrDefault(p => p.Id == x);
            Quest quest = context.Quests.SingleOrDefault(p => p.Id == y);
            Campaign campaign = context.Campaigns.FirstOrDefault(p => p.Quests.Contains(quest));

            if (campaign != player.Campaign)
            {
                throw new Exception("This is bogus. You didn't complete anything.");
            }

            return true;
        }
    }
}
