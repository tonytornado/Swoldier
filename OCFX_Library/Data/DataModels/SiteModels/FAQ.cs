﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Facts
    {
        public Facts()
        {
        }

        /// <summary>
        /// Standard implementation of a new fact
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <param name="section"></param>
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
        Main,
        Site,
        Lore,
        Community,
        Fitness,
        Integration
    }
}
