using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assesment03.Data;
using Assesment03.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assesment03.Controllers
{
    [Authorize]
    public class RsourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rsources
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rsource.ToListAsync());
        }

        // GET: Rsources/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rsource == null)
            {
                return NotFound();
            }

            var rsource = await _context.Rsource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsource == null)
            {
                return NotFound();
            }

            return View(rsource);
        }

        // GET: Rsources/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rsources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Resource_Id,Resource_Name,Resource_Description,Resource_Specification,Resource_Availability")] Rsource rsource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rsource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(rsource);
        }

        // GET: Rsources/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rsource == null)
            {
                return NotFound();
            }

            var rsource = await _context.Rsource.FindAsync(id);
            if (rsource == null)
            {
                return NotFound();
            }
            return View(rsource);
        }

        // POST: Rsources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Resource_Id,Resource_Name,Resource_Description,Resource_Specification,Resource_Availability")] Rsource rsource)
        {
            if (id != rsource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rsource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsourceExists(rsource.Id))
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
            return View(rsource);
        }

        // GET: Rsources/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rsource == null)
            {
                return NotFound();
            }

            var rsource = await _context.Rsource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsource == null)
            {
                return NotFound();
            }

            return View(rsource);
        }

        // POST: Rsources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rsource == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rsource'  is null.");
            }
            var rsource = await _context.Rsource.FindAsync(id);
            if (rsource != null)
            {
                _context.Rsource.Remove(rsource);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsourceExists(int id)
        {
          return _context.Rsource.Any(e => e.Id == id);
        }
    }
}
