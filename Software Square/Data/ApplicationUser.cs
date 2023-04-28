using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Software_Square.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string contact  { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
