using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
	public enum RiskLevel
	{
		[Display(Name = "Low")]
		Low = 1,
		[Display(Name = "Mid")]
		Mid = 2,
		[Display(Name = "High")]
		High = 3,
		[Display(Name = "BENJAMIN CARSON DOUBLE OMEGA")]
		EX = 4
	}
}