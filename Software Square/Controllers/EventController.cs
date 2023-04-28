using CRUD_Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Software_Square.Data;
using Software_Square.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Software_Square.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db;
        public static int MainEveId = 0;
        public EventController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Event> events = new List<Event>();
            try
            {

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Event WHERE Id NOT IN (SELECT SubEventId FROM SubEvent)", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Event eve = new Event
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        RegisterationLink = reader["RegisterationLink"].ToString(),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        EventBanner = (byte[])reader["EventBanner"]
                    };
                    events.Add(eve);
                }
                reader.Close();
            }
            catch
            {

            }
            return View(events);
        }
        public IActionResult Create(int id=0)
        {
            MainEveId = id;
            Event eve = new Event();
            eve.eventSponsors.Add(new EventSponsor() { EventId = 0 });
            return View(eve);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event e)
        {

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    e.posterImage.CopyToAsync(memoryStream);
                    e.EventBanner = memoryStream.ToArray();
                }
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO Event (Title,Description,RegisterationLink,StartDate,EndDate,EventBanner) VALUES (@T,@D,@R,@S,@E,@I); SELECT Id FROM Event WHERE Title=@T AND RegisterationLink=@R AND StartDate=@S AND EndDate=@E;", con);
                cmd.Parameters.AddWithValue("@T", e.Title);
                cmd.Parameters.AddWithValue("@D", e.Description);
                cmd.Parameters.AddWithValue("@R", e.RegisterationLink);
                cmd.Parameters.AddWithValue("@S", e.StartDate);
                cmd.Parameters.AddWithValue("@E", e.EndDate);
                cmd.Parameters.AddWithValue("@I", e.EventBanner);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    e.Id = Convert.ToInt32(reader["Id"]);
                }
                reader.Close();
                foreach (var s in e.eventSponsors)
                {
                    s.EventId = e.Id;
                    cmd = new SqlCommand("INSERT INTO EventSponsor (EventId,SponsorName) VALUES (@Id,@SN)", con);
                    cmd.Parameters.AddWithValue("@Id", s.EventId);
                    cmd.Parameters.AddWithValue("@SN", s.SponsorName);
                    cmd.ExecuteNonQuery();
                }
                if(MainEveId!=0)
                {
                    SubEvent sub = new SubEvent();
                    sub.MainEventId = MainEveId;
                    sub.SubEventId = e.Id;
                    _db.SubEvent.Add(sub);
                    await _db.SaveChangesAsync();
                }
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            Event eve = new Event();
         
            try
			{

				var con = Configuration.getInstance().getConnection();
				SqlCommand cmd = new SqlCommand("SELECT * FROM Event WHERE Id = "+id, con);
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.Read())
                {
                    eve.Id = Convert.ToInt32(reader["Id"]);
                    eve.Title = reader["Title"].ToString();
                    eve.Description = reader["Description"].ToString();
                    eve.RegisterationLink = reader["RegisterationLink"].ToString();
                    eve.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    eve.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    eve.EventBanner = (byte[])reader["EventBanner"];
				}
				reader.Close();
                
                cmd = new SqlCommand("SELECT * FROM Event AS E  JOIN SubEvent AS SE ON E.Id = SE.SubEventId WHERE SE.MainEventId = " + id, con);
				 reader = cmd.ExecuteReader();
				while(reader.Read())
				{
					Event subEve = new Event
					{
						Id = Convert.ToInt32(reader["Id"]),
						Title = reader["Title"].ToString(),
						Description = reader["Description"].ToString(),
						RegisterationLink = reader["RegisterationLink"].ToString(),
						StartDate = Convert.ToDateTime(reader["StartDate"]),
						EndDate = Convert.ToDateTime(reader["EndDate"]),
						EventBanner = (byte[])reader["EventBanner"]
					};
                   eve.SubEvents.Add(subEve);
				}
                reader.Close();

                cmd = new SqlCommand("SELECT * FROM EventSponsor WHERE EventId = " + id, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EventSponsor eveSp = new EventSponsor
                    {
                        EventId = Convert.ToInt32(reader["EventId"]),
                        SponsorName = reader["SponsorName"].ToString()
                        
                      
                    };
                    eve.eventSponsors.Add(eveSp);
                }
                reader.Close();
            }
			catch
			{

			}
            string m = eve.StartDate.ToString("MMM");
			return View(eve);
        }

    }
}
