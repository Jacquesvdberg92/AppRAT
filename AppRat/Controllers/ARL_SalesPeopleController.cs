using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;

namespace AppRat.Controllers
{
    public class ARL_SalesPeopleController : Controller
    {
        private readonly AppRatDbContext _context;

        public ARL_SalesPeopleController(AppRatDbContext context)
        {
            _context = context;
        }

        // GET: ARL_SalesPeople
        public async Task<IActionResult> Index()
        {
              return _context.ARL_SalesPeople != null ? 
                          View(await _context.ARL_SalesPeople.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_SalesPeople'  is null.");
        }

        // GET: ARL_SalesPeople/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_SalesPeople == null)
            {
                return NotFound();
            }

            var aRL_SalesPeople = await _context.ARL_SalesPeople
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_SalesPeople == null)
            {
                return NotFound();
            }

            return View(aRL_SalesPeople);
        }

        // GET: ARL_SalesPeople/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_SalesPeople/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DealerId")] ARL_SalesPeople aRL_SalesPeople)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_SalesPeople);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_SalesPeople);
        }

        // GET: ARL_SalesPeople/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_SalesPeople == null)
            {
                return NotFound();
            }

            var aRL_SalesPeople = await _context.ARL_SalesPeople.FindAsync(id);
            if (aRL_SalesPeople == null)
            {
                return NotFound();
            }
            return View(aRL_SalesPeople);
        }

        // POST: ARL_SalesPeople/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DealerId")] ARL_SalesPeople aRL_SalesPeople)
        {
            if (id != aRL_SalesPeople.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_SalesPeople);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_SalesPeopleExists(aRL_SalesPeople.Id))
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
            return View(aRL_SalesPeople);
        }

        // GET: ARL_SalesPeople/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_SalesPeople == null)
            {
                return NotFound();
            }

            var aRL_SalesPeople = await _context.ARL_SalesPeople
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_SalesPeople == null)
            {
                return NotFound();
            }

            return View(aRL_SalesPeople);
        }

        // POST: ARL_SalesPeople/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_SalesPeople == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_SalesPeople'  is null.");
            }
            var aRL_SalesPeople = await _context.ARL_SalesPeople.FindAsync(id);
            if (aRL_SalesPeople != null)
            {
                _context.ARL_SalesPeople.Remove(aRL_SalesPeople);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_SalesPeopleExists(int id)
        {
          return (_context.ARL_SalesPeople?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
