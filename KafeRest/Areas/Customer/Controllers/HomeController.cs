using KafeRest.Data;
using KafeRest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Diagnostics;

namespace KafeRest.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _he;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db,IToastNotification toast, IWebHostEnvironment he)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _he = he;
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


        public IActionResult Gallery()
        {
            var gallery = _db.Galery.ToList();
            return View(gallery);
        }
        public IActionResult Reservation()
        {
            return View();
        }

        // POST: Admin/Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation([Bind("Id,Name,Email,PhoneNumber,Count,Clock,History")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _db.Add(reservation);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Thanks you to Appolina Restaurant Customer!" +
                                             " We are expecting you... ");
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public IActionResult About()
        {
            var about=_db.Abouts.ToList();
            return View(about);
        }

        public IActionResult Blog()
        {
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.History=DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (blog.Image is not null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, blog.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName+ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    blog.Image=@"\Site\menu\"+fileName+ext;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage($"Thanks you for comment,dear {blog.Name} :)");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
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
