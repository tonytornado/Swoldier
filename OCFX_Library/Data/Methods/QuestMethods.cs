using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;

namespace OCFX.Data.Methods
{
    public static class QuestMethods
    {
        /// <summary>
        /// Checks the completed quest log for all the quests a user has completed.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<int> CheckCompletedQuests(OCFXContext context, int userId)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var ops = from q in context.Quests
                join ql in context.QuestLogs on q.Id equals ql.Quest.Id
                where ql.Character.Id == userId
                where ql.Completed == true
                select ql.Quest.Id;

            return ops.ToList();
        }

        /// <summary>
        /// Enables the start of a quest. 
        /// A quest must be a part of a campaign; but can also be a part of a sidequest.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="questId">Quest's Id</param>
        /// <param name="userId">User's Id</param>
        public static void JoinQuest(OCFXContext context, int questId, int userId)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Quest quest = context.Quests.SingleOrDefault(q => q.Id == questId);

            // Create the new quest log
            if (quest != null)
            {
                QuestLog challenger = new QuestLog()
                {
                    Character = context.Characters.SingleOrDefault(c => c.Id == userId),
                    Quest = quest,
                    Completed = false,
                    Campaign = quest.Campaign
                };
                context.QuestLogs.Add(challenger);
            }

            context.SaveChanges();

            var chara = context.Characters.Include(q => q.Quests).SingleOrDefault(p => p.Id == userId);
            context.SaveChanges();
        }

        /// <summary>
        /// Completes a quest provided they meet certain conditions.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="questId">Quest's Id</param>
        /// <param name="userId">User's Id</param>
        public static void CompleteQuest(OCFXContext context, int questId, int userId)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            int questGoer = userId;
            QuestLog completedQuest =
                context.QuestLogs.SingleOrDefault(q => q.Quest.Id == questId && q.Character.Id == questGoer);

            // Check if the quest is actually part of the campaign
            // bool questCheck = completedQuest != null &&
                              // CheckForCampaign(context, questGoer, completedQuest.Campaign.Id);
            // if (questCheck != true)
            // {
                // throw new Exception("Yeah, they didn't complete the quest, fall back.");
            // }

            // Set the quest as complete.
            if (completedQuest != null) completedQuest.Completed = true;
            context.SaveChanges();

            CharacterModel completionist =
                context.Characters.Include(q => q.Quests).SingleOrDefault(q => q.Id == userId);
            if (completionist != null) completionist.Quests = null;
            context.SaveChanges();
        }

        // /// <summary>
        // /// Makes a quick check for the right campaign to prevent cheating. This is an ANTI-CHEAT measure.
        // /// </summary>
        // /// <param name="context"></param>
        // /// <param name="x"></param>
        // /// <param name="y"></param>
        // /// <returns>Boolean</returns>
        // private static bool CheckForCampaign(OCFXContext context, int x, int y)
        // {
        //     if (context is null)
        //     {
        //         throw new ArgumentNullException(nameof(context));
        //     }
        //
        //     CharacterModel player = context.Characters.SingleOrDefault(p => p.Id == x);
        //     Quest quest = context.Quests.SingleOrDefault(p => p.Id == y);
        //     Campaign campaign = context.Campaigns.FirstOrDefault(p => p.Quests.Contains(quest));
        //
        //     
        //
        //     return true;
        // }
    }
}