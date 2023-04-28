using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Software_Square.Models
{
	public class Event
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(50)]
		public string Title { get; set; }
		[Required, MinLength(50)]
		public string Description { get; set; }
		public string RegisterationLink { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		[Required, NotMapped]
		public IFormFile posterImage { get; set; }
		[Required]
		public byte[] EventBanner { get; set; }
		public List<Event> SubEvents { get; set; } = new List<Event>();
		public List<EventSponsor> eventSponsors { get; set; } = new List<EventSponsor>();
	}
}
