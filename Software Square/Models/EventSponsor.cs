using System.ComponentModel.DataAnnotations;

namespace Software_Square.Models
{
	public class EventSponsor
	{
		[Key]
		public int EventId { get; set; }
		[Required, MaxLength(50)]
		public string SponsorName { get; set; }
	}
}
