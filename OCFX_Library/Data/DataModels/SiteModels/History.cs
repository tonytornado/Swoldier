using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.Data.HistoryModels
{
    public class History
    {
        public History()
        {

        }

        /// <summary>
        /// Standard Implementation with single change
        /// </summary>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">A new change</param>
        /// <param name="type"></param>
        public History(string oldValue, string newValue, Type type)
        {
            Date = DateTime.Now;
            OldValue = oldValue;
            NewValue = newValue;
            Deleted = 'N';
            ThingType = nameof(type);
        }

        /// <summary>
        /// Implementation with multiple changes in an array for each change
        /// </summary>
        /// <param name="oldValue">An array of the old values</param>
        /// <param name="newValue">An array of the changes</param>
        /// <param name="type"></param>
        public History(Array oldValue, Array newValue, Type type)
        {
            Date = DateTime.Now;
            OldValue = String.Join(", ", oldValue);
            NewValue = String.Join(", ", newValue); 
            Deleted = 'N';
            ThingType = nameof(type);
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Created/Modified Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Old")]
        public string OldValue { get; set; }
        [Display(Name = "New")]
        public string NewValue { get; set; }
        [Display(Name = "Deleted")]
        public char Deleted { get; set; }
        [Display(Name = "Type")]
        public string ThingType { get; set; }
    }
}