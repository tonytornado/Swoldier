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

        /// <summary>
        /// Standard implementation of a Skill object
        /// </summary>
        /// <param name="name">Name of the skill</param>
        /// <param name="airCost">O2 cost</param>
        /// <param name="style"></param>
        /// <param name="cooldown"></param>
        /// <param name="target"></param>
        /// <param name="effect"></param>
        /// <param name="description"></param>
        /// <param name="warning"></param>
        public Skill(string name, int airCost, StyleType style, int cooldown, TargetType target, EffectType effect, string description, string warning)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AirCost = airCost;
            Style = style;
            Cooldown = cooldown;
            Target = target;
            Effect = effect;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Warning = warning ?? throw new ArgumentNullException(nameof(warning));
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "O2 Cost")]
        [Required]
        public int AirCost { get; set; }
        [Display(Name = "Type")]
        public StyleType Style { get; set; }
        [Display(Name = "Cooldown")]
        public int Cooldown { get; set; }
        [Display(Name = "Target")]
        public TargetType Target { get; set; }
        [Display(Name = "Effect")]
        public EffectType Effect { get; set; }
        [Display(Name = "Description")]
        [Required]
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