using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Software_Square.Data;
using Software_Square.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Software_Square.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<bookDonate>().Ignore(p => p.book);
		}

		public DbSet<bookDonate>? BookDonated { get; set; }

		public DbSet<Member> Member { get; set; }
		public DbSet<Event> Event { get; set; }
		public DbSet<SubEvent> SubEvent { get; set; }
		public DbSet<EventSponsor> EventSponsor { get; set; }


	}
}