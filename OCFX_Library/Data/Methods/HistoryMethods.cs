using OCFX.Data.Context;
using OCFX.Data.HistoryModels;
using OCFX.DataModels;
using System;

namespace OCFX.Data.Methods
{
    public static class HistoryMethods
    {
        /// <summary>
        /// Method for changes of several types
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="oldValue">Old value</param>
        /// <param name="newValue">New value</param>
        /// <param name="type">Type of change</param>
        public static void GenericHistory(HistoryContext context, string oldValue, string newValue, Type type)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var HistoryItem = new History { 
                Date = DateTime.Now,
                OldValue = oldValue,
                NewValue = newValue,
                ThingType = nameof(type),
                Deleted = 'N'
            };
            context.Histories.Add(HistoryItem);
            context.SaveChanges();
        }


        // Method for deleting accounts
        public static void DeletedProfileHistory(HistoryContext context, string oldValue)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var HistoryItem = new History
            {
                Date = DateTime.Now,
                OldValue = oldValue,
                NewValue = "N/A",
                ThingType = nameof(ProfileSheet),
                Deleted = 'Y'
            };
            context.Histories.Add(HistoryItem);
            context.SaveChanges();
        }
        // Method for posting on other accounts

        // Method for changing campaigns
        // Method for adding to campaigns
        // Method for when someone joins your campaign instance

        // Method for changing name information
        // Method for changing profile information
        // Method for changing drive information
        // Method for changing classes

        // Method for completing workouts
        // Method for completing quests
        // Method for completing campaigns
    }
}
