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
    public class ARL_ResultController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly IDropdownService _dropdownService;

        public ARL_ResultController(AppRatDbContext context, IDropdownService dropdownService)
        {
            _context = context;
            _dropdownService = dropdownService;
        }

        // GET: ARL_Result
        public async Task<IActionResult> Index()
        {
              return _context.ARL_Results != null ? 
                          View(await _context.ARL_Results.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_Results'  is null.");
        }

        // GET: ARL_Result/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Results == null)
            {
                return NotFound();
            }

            var aRL_Result = await _context.ARL_Results
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Result == null)
            {
                return NotFound();
            }

            return View(aRL_Result);
        }

        // GET: ARL_Result/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Result/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Result aRL_Result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Result);
        }

        // GET: ARL_Result/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Results == null)
            {
                return NotFound();
            }

            var aRL_Result = await _context.ARL_Results.FindAsync(id);
            if (aRL_Result == null)
            {
                return NotFound();
            }
            return View(aRL_Result);
        }

        // POST: ARL_Result/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Result aRL_Result)
        {
            if (id != aRL_Result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_ResultExists(aRL_Result.Id))
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
            return View(aRL_Result);
        }

        // GET: ARL_Result/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Results == null)
            {
                return NotFound();
            }

            var aRL_Result = await _context.ARL_Results
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Result == null)
            {
                return NotFound();
            }

            return View(aRL_Result);
        }

        // POST: ARL_Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Results == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Results'  is null.");
            }
            var aRL_Result = await _context.ARL_Results.FindAsync(id);
            if (aRL_Result != null)
            {
                _context.ARL_Results.Remove(aRL_Result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_ResultExists(int id)
        {
          return (_context.ARL_Results?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ResultDropdown()
        {
            List<ARL_ResultViewModel.ARL_Result> results = _dropdownService.GetResultsFromDatabase();
            return PartialView("_ResultDropdown", results);
        }
    }
}
