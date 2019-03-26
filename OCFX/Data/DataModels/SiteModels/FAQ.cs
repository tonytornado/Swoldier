using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
	public class Facts
	{
		[Display(Name = "FAQ #")]
		public int Id { get; set; }
		[Display(Name = "Question")]
		public string Question { get; set; }
		[Display(Name = "Answer")]
		public string Answer { get; set; }
		[Display(Name = "Section")]
		public SectionName Section { get; set; }
	}

	public enum SectionName
	{
		Main = 1,
		Site = 2,
		Lore = 3,
		Community = 4
	}
}
