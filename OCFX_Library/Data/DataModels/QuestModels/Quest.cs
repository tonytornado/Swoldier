﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    public class Quest
    {
        public Quest()
        {
        }

        public Quest(string questName, QuestType questStyle, string questStory, Campaign campaign)
        {
            QuestName = questName ?? throw new ArgumentNullException(nameof(questName));
            QuestStyle = questStyle;
            QuestStory = questStory ?? throw new ArgumentNullException(nameof(questStory));
            Campaign = campaign ?? throw new ArgumentNullException(nameof(campaign));
        }

        [Key]
        [Display(Name = "Quest")]
        public int Id { get; set; }
        [Display(Name = "Quest Name")]
        public string QuestName { get; set; }
        [Display(Name = "Quest Type")]
        public QuestType QuestStyle { get; set; }
        [Display(Name = "Quest Description")]
        public string QuestStory { get; set; }

        // Folks that are on this quest
        public List<Profile> CurrentPlayers { get; set; }

        // Encounters on this quest
        public List<Encounter> Encounters { get; set; }

        // It's part of the campaign, yeah?
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        public string QuestTitle => $"{QuestName} [{QuestStyle}] - {Campaign.Name}";
    }

    public enum QuestType
    {
        [Display(Name = "Power")]
        Power = 1,
        [Display(Name = "Endurance")]
        Endurance = 2,
        [Display(Name = "Speed")]
        Speed = 3,
        [Display(Name = "Consistency")]
        Consistency = 4
    }

    public enum RiskLevel
    {
        [Display(Name = "Low")]
        Low = 1,
        [Display(Name = "Mid")]
        Mid = 2,
        [Display(Name = "High")]
        High = 3,
        [Display(Name = "JOJO'S BIZARRE SMASH ULTIMATE ELIMINATION CHAMBER IN THE BANK IN A CELL")]
        EX = 4
    }
}
