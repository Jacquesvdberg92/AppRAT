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
    public class ARL_InsuranceController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly IDropdownService _dropdownService;

        public ARL_InsuranceController(AppRatDbContext context, IDropdownService dropdownService)
        {
            _context = context;
            _dropdownService = dropdownService;
        }

        // GET: ARL_Insurance
        public async Task<IActionResult> Index()
        {
              return _context.ARL_Insurances != null ? 
                          View(await _context.ARL_Insurances.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_Insurances'  is null.");
        }

        // GET: ARL_Insurance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Insurances == null)
            {
                return NotFound();
            }

            var aRL_Insurance = await _context.ARL_Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Insurance == null)
            {
                return NotFound();
            }

            return View(aRL_Insurance);
        }

        // GET: ARL_Insurance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Insurance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Insurance aRL_Insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Insurance);
        }

        // GET: ARL_Insurance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Insurances == null)
            {
                return NotFound();
            }

            var aRL_Insurance = await _context.ARL_Insurances.FindAsync(id);
            if (aRL_Insurance == null)
            {
                return NotFound();
            }
            return View(aRL_Insurance);
        }

        // POST: ARL_Insurance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Insurance aRL_Insurance)
        {
            if (id != aRL_Insurance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_InsuranceExists(aRL_Insurance.Id))
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
            return View(aRL_Insurance);
        }

        // GET: ARL_Insurance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Insurances == null)
            {
                return NotFound();
            }

            var aRL_Insurance = await _context.ARL_Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Insurance == null)
            {
                return NotFound();
            }

            return View(aRL_Insurance);
        }

        // POST: ARL_Insurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Insurances == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Insurances'  is null.");
            }
            var aRL_Insurance = await _context.ARL_Insurances.FindAsync(id);
            if (aRL_Insurance != null)
            {
                _context.ARL_Insurances.Remove(aRL_Insurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_InsuranceExists(int id)
        {
          return (_context.ARL_Insurances?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult InsuranceDropdown()
        {
            List<ARL_InsuranceViewModel.ARL_Insurance> insurances = _dropdownService.GetInsurancesFromDatabase();
            return PartialView("_InsuranceDropdown", insurances);
        }
    }
}
