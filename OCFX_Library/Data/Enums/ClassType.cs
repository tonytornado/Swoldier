using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    /// <summary>
    /// The different class types, each with their own set of stat boosts.
    /// </summary>
    public enum ClassType
    {
        [Display(Name = "Hobbyist")]
        Hobbyist,
        [Display(Name = "Runner")]
        Runner,
        [Display(Name = "Powerlifter")]
        Powerlifter,
        [Display(Name = "Bodybuilder")]
        Bodybuilder,
        [Display(Name = "Crossfitter")]
        Crossfit,
        [Display(Name = "Olympian")]
        Olympian,
        [Display(Name = "Fighter")]
        Fighter,
        [Display(Name = "Dancer")]
        Dancer,
        [Display(Name = "Yogi")]
        Yoga
    }
}