using KafeRest.Data;
using KafeRest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KafeRest.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var menu = _db.Yemekler.Where(i=>i.Specialy).ToList();
            return View(menu);
        }

        public IActionResult Menu()
        {
            var menu = _db.Yemekler.ToList();

            return View(menu);
        }

        public IActionResult Reservation()
        {
            return View();
        }

        public IActionResult Gallery()
        {

            return View();
        }
        public IActionResult About()
        {

            return View();
        }

        public IActionResult Blog()
        {

            return View();
        }
		public IActionResult CategoryDetails(int? id)
		{
            var menu = _db.Yemekler.Where(i=>i.CategoryId== id).ToList();
			ViewBag.CategoryId = id;
			return View(menu);
		}
		public IActionResult Contact()
        {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
