using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KafeRest.Data;
using KafeRest.Models;
using Microsoft.AspNetCore.Authorization;

namespace KafeRest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GaleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;

        public GaleryController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he=he;
        }

        // GET: Admin/Galery
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galery.ToListAsync());
        }

        // GET: Admin/Galery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _context.Galery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galery == null)
            {
                return NotFound();
            }

            return View(galery);
        }

        // GET: Admin/Galery/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Galery galery)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (galery.Image is not null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, galery.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName+ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    galery.Image=@"\Site\menu\"+fileName+ext;
                }
                _context.Add(galery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galery);
        }

        // GET: Admin/Galery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _context.Galery.FindAsync(id);
            if (galery == null)
            {
                return NotFound();
            }
            return View(galery);
        }

        // POST: Admin/Galery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] Galery galery)
        {
            if (id != galery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleryExists(galery.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(galery);
        }

        // GET: Admin/Galery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _context.Galery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galery == null)
            {
                return NotFound();
            }

            return View(galery);
        }

        // POST: Admin/Galery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galery = await _context.Galery.FindAsync(id);
            if (galery != null)
            {
                _context.Galery.Remove(galery);
            }
            var imagePath = Path.Combine(_he.WebRootPath, galery.Image.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleryExists(int id)
        {
            return _context.Galery.Any(e => e.Id == id);
        }
    }
}
