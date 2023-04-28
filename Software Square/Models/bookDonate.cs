using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Software_Square.Models
{
	public class bookDonate
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		[Required]
		public string UserId { get; set; }
		public int Status { get; set; }
		public IFormFile book { get; set; }
		public string path { get; set; }
	}
}
