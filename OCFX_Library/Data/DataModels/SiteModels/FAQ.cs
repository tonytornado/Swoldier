using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Facts
    {
        public Facts()
        {
        }

        public Facts(string question, string answer, SectionName section)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            Answer = answer ?? throw new ArgumentNullException(nameof(answer));
            Section = section;
        }

        [Display(Name = "FAQ #")]
        public int Id { get; set; }
        /// <summary>
        /// Question portion of the FAQ
        /// </summary>
        [Display(Name = "Question")]
        public string Question { get; set; }
        /// <summary>
        /// Answer portion of the FAQ
        /// </summary>
        [Display(Name = "Answer")]
        public string Answer { get; set; }
        /// <summary>
        /// Section for the FAQ, a category even.
        /// </summary>
        [Display(Name = "Section")]
        public SectionName Section { get; set; }
        /// <summary>
        /// DateTime for added date
        /// </summary>
        [Display(Name = "Date Added"), DataType(DataType.Date)]
        public DateTime AddedDate => DateTime.Now;
    }

    public enum SectionName
    {
        Main = 1,
        Site = 2,
        Lore = 3,
        Community = 4,
        Fitness = 5,
        Integration = 6
    }
}
