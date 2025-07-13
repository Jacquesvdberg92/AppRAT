using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Authorization;
using AppRat.ViewModels;
using AppRat.Services;

namespace AppRat.Controllers
{
    [Authorize]
    public class ARL_DealershipController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly IDropdownService _dropdownService;

        public ARL_DealershipController(AppRatDbContext context, IDropdownService dropdownService)
        {
            _context = context;
            _dropdownService = dropdownService;
        }

        // GET: ARL_Dealership
        public async Task<IActionResult> Index()
        {
              return _context.ARL_Dealerships != null ? 
                          View(await _context.ARL_Dealerships.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_Dealerships'  is null.");
        }

        // GET: ARL_Dealership/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Dealerships == null)
            {
                return NotFound();
            }

            var aRL_Dealership = await _context.ARL_Dealerships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Dealership == null)
            {
                return NotFound();
            }

            return View(aRL_Dealership);
        }

        // GET: ARL_Dealership/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Dealership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Dealership aRL_Dealership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Dealership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Dealership);
        }

        // GET: ARL_Dealership/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Dealerships == null)
            {
                return NotFound();
            }

            var aRL_Dealership = await _context.ARL_Dealerships.FindAsync(id);
            if (aRL_Dealership == null)
            {
                return NotFound();
            }
            return View(aRL_Dealership);
        }

        // POST: ARL_Dealership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Dealership aRL_Dealership)
        {
            if (id != aRL_Dealership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Dealership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_DealershipExists(aRL_Dealership.Id))
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
            return View(aRL_Dealership);
        }

        // GET: ARL_Dealership/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Dealerships == null)
            {
                return NotFound();
            }

            var aRL_Dealership = await _context.ARL_Dealerships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Dealership == null)
            {
                return NotFound();
            }

            return View(aRL_Dealership);
        }

        // POST: ARL_Dealership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Dealerships == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Dealerships'  is null.");
            }
            var aRL_Dealership = await _context.ARL_Dealerships.FindAsync(id);
            if (aRL_Dealership != null)
            {
                _context.ARL_Dealerships.Remove(aRL_Dealership);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_DealershipExists(int id)
        {
          return (_context.ARL_Dealerships?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ConditionDropdown()
        {
            List<ARL_DealershipViewModel.ARL_Dealership> dealerships = _dropdownService.GetDealershipsFromDatabase();
            return PartialView("_DealershipDropdown", dealerships);
        }
    }
}
