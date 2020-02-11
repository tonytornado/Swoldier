using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    /// <summary>
    /// Levels of skill for a certain set. Adds small boosts after some time.
    /// </summary>
    public enum SkillType
    {
        [Display(Name = "Basic")]
        Basic,
        [Display(Name = "Intermediate")]
        Intermediate,
        [Display(Name = "Advanced")]
        Advanced,
        [Display(Name = "Elite")]
        Elite,
        [Display(Name = "Legendary")]
        Legendary
    }
}