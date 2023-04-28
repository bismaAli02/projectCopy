using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Software_Square.Data;
using Software_Square.Models;
using Software_Square.Services;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Hosting.Builder;
using CRUD_Operations;
using System.Data.SqlClient;

namespace Software_Square.Controllers
{
	public class BookDonateController : Controller
	{
		private readonly IUserService _userService;
		private readonly IServiceProvider _service;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		public BookDonateController(IUserService userService, IServiceProvider service, ApplicationDbContext db)
		{
			_db = db;
			_userService = userService;
			_userManager = service.GetService<UserManager<ApplicationUser>>();
		}
		[HttpGet]
		public IActionResult Donate()
		{
			bookDonate bookDonate = new bookDonate();
			return View(bookDonate);
		}

		public int GetStatus(string value)
		{
			int id = 0;
			var con = Configuration.getInstance().getConnection();
			SqlCommand cmd = new SqlCommand("SELECT Id FROM Lookup WHERE value = '" + value + "'", con);
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.Read())
			{
				id = Convert.ToInt32(reader["id"]);
			}
			reader.Close();
			return id;
		}

		[HttpPost]
		public async Task<IActionResult> Donate(bookDonate bookDonated)
		{
			bookDonated.UserId = _userManager.GetUserId(HttpContext.User);
			string fileName = bookDonated.book.FileName;
			fileName = Path.GetFileName(fileName);
			fileName = fileName.Replace(" ","");
			string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Books", fileName);
			FileStream file = new FileStream(uploadFilePath, FileMode.Create);
			bookDonated.book.CopyToAsync(file);
			bookDonated.path = "Books/"+fileName;
			bookDonated.Status = GetStatus("Pending");
			_db.BookDonated.Add(bookDonated);
			await _db.SaveChangesAsync();


			return RedirectToAction("Books");
		}
		[HttpGet]
		public async Task<IActionResult> Books()
		{
			int approvedId = GetStatus("Approved");
			int rejectId = GetStatus("Rejected");
			int PendingId = GetStatus("Pending");
			List<bookDonate> books = _db.BookDonated.ToList();
			ViewData["ApproveId"] = approvedId;
			ViewData["RejectId"] = rejectId;
			ViewData["PendingId"] = PendingId;
			return View(books);
		}

		public async Task<IActionResult> StatusChange(int id, int status)
		{
			bookDonate book = _db.BookDonated.ToList().Where(x => x.Id == id).First();
			book.Status = status;
			_db.BookDonated.Update(book);
			await _db.SaveChangesAsync();
			return RedirectToAction("Books");

		}

	}
}