using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    /// <summary>
    /// Skills for the DnD side of Swoldier Application
    /// </summary>
    public class Skill
    {
        public Skill()
        {
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "O2 Cost")]
        public int AirCost { get; set; }
        [Display(Name = "Type")]
        public StyleType Style { get; set; }
        [Display(Name = "Cooldown")]
        public TimeSpan Cooldown { get; set; }
        [Display(Name = "Target")]
        public TargetType Target { get; set; }
        [Display(Name = "Effect")]
        public EffectType Effect { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Dangers")]
        public string Warning { get; set; }

        public string SkillPower => $"{Name} [{Style}][{Target}][{Effect}]";
    }

    /// <summary>
    /// Type of skill
    /// </summary>
    public enum StyleType
    {
        Physical,
        Mental,
        Spiritual
    }

    /// <summary>
    /// Target types
    /// </summary>
    public enum TargetType
    {
        [Display(Name = "Self")]
        Self,
        [Display(Name = "Front Target")]
        SingleFront,
        [Display(Name = "Rear Target")]
        SingleRear,
        [Display(Name = "Side Target")]
        SingleSide,
        [Display(Name = "Area of Effect")]
        AreaOfEffect,
    }

    /// <summary>
    /// Effects from moves used.
    /// </summary>
    public enum EffectType
    {
        None,
        // Negatives
        Aggro,
        DOMS,
        Strain,
        MuscleTear,
        // Positives
        Pump,
        Buff,
        Hype,
        // Incapacitation
        Concussion,
        BrokenLimb,
        DoctorsOrder
    }
}