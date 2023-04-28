using System.ComponentModel.DataAnnotations;

namespace Software_Square.Models
{
	public class SubEvent
	{
		[Key]
		public int MainEventId { get; set; }
		[Required]
		public int SubEventId { get; set; }
	}
}
