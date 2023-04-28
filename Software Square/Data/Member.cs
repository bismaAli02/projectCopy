using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Software_Square.Data
{
    [Table("Member")]
    public class Member
    {

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        
        public string RegistrationNumber { get; set; }
        public string Department { get; set; }
        public string Session { get; set; }
        [Required]
        public int  Team { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
    }
}
