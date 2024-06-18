using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KafeRest.Data;
using KafeRest.Models;

namespace KafeRest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YemekController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;
        


        public YemekController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he=he;
        }

        // GET: Admin/Yemek
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Yemekler.Include(m => m.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Yemek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Yemekler
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Yemek/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Yemek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
           
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count>0)
                {
                    var fileName=Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if(menu.Images is not null)
                    {
                        var imagePath=Path.Combine(_he.WebRootPath,menu.Images.TrimStart('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    
                    }
                    using(var filesStreams =new FileStream(Path.Combine(uploads,fileName+ext),FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    menu.Images=@"\Site\menu\"+fileName+ext;
                }
                _context.Add(menu);
                menu.Category = await _context.Categories.FindAsync(menu.CategoryId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", menu.CategoryId);
            return View(menu);
        }

        // GET: Admin/Yemek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Yemekler.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", menu.CategoryId);
            return View(menu);
        }

        // POST: Admin/Yemek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Menu menu)
        {
            
            if (ModelState.IsValid)
            {
               
                    var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (menu.Images is not null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, menu.Images.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName+ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    menu.Images=@"\Site\menu\"+fileName+ext;
                }
                        _context.Update(menu);
                        await _context.SaveChangesAsync();
                    
                
               
                return RedirectToAction(nameof(Index));
            }
            
            return View(menu);
        }

        // GET: Admin/Yemek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Yemekler
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Yemek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Yemekler.FindAsync(id);
            if (menu != null)
            {
                _context.Yemekler.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Yemekler.Any(e => e.Id == id);
        }
    }
}
