using System;

namespace OCFX.DataModels
{
    public class Skills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AirCost { get; set; }
        public StyleType Style { get; set; }
        public TimeSpan Cooldown { get; set; }
        public TargetType Target { get; set; }
        public EffectType Effect { get; set; }
        public string Description { get; set; }
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
        Self = 0,
        SingleFront = 1,
        SingleRear = 2,
        SingleSide = 3,
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