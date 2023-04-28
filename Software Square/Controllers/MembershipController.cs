using Microsoft.AspNetCore.Mvc;
using Software_Square.Data;
namespace Software_Square.Controllers
{
    public class MembershipController : Controller
    {
        private readonly ApplicationDbContext Dbcontext;

        public MembershipController(ApplicationDbContext context)
        {
            this.Dbcontext = context;
        }
        public IActionResult MembersList()
        {
            //var members = Dbcontext.Membership.ToList();
            //return View(members);
            return View();
        }

        public IActionResult Membership()
        {
            return View();
        }
    }
}
