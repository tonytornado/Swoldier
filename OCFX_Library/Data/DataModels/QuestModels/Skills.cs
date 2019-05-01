using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class Skills
    {
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
    }

    /// <summary>
    /// Type of skill
    /// </summary>
    public enum StyleType
    {
        Physical = 0,
        Mental = 1,
        Spiritual = 2
    }

    /// <summary>
    /// Target types
    /// </summary>
    public enum TargetType
    {
        [Display(Name = "Self")]
        Self = 0,
        [Display(Name = "Front Target")]
        SingleFront = 1,
        [Display(Name = "Rear Target")]
        SingleRear = 2,
        [Display(Name = "Side Target")]
        SingleSide = 3,
        [Display(Name = "Area of Effect")]
        AreaOfEffect = 4,
    }

    /// <summary>
    /// Effects from moves used.
    /// </summary>
    public enum EffectType
    {
        Nothing = 0,
        // Negatives
        Aggro = 1,
        DOMS = 2,
        Strain = 3,
        MuscleTear = 4,
        // Positives
        Pump = 5,
        Buff = 6,
        Hype = 7,
        // Incapacitation
        Concussion = 8,
        BrokenLimb = 9,
        DoctorsOrder =10
    }
}