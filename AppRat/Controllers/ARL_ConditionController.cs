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
    public class ARL_ConditionController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly IDropdownService _dropdownService;
        public ARL_ConditionController(AppRatDbContext context, IDropdownService dropdownService)
        {
            _context = context;
            _dropdownService = dropdownService;
        }

        // GET: ARL_Condition
        public async Task<IActionResult> Index()
        {
            return _context.ARL_Conditions != null ?
                        View(await _context.ARL_Conditions.ToListAsync()) :
                        Problem("Entity set 'AppRatDbContext.ARL_Conditions'  is null.");
        }

        // GET: ARL_Condition/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Conditions == null)
            {
                return NotFound();
            }

            var aRL_Condition = await _context.ARL_Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Condition == null)
            {
                return NotFound();
            }

            return View(aRL_Condition);
        }

        // GET: ARL_Condition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Condition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Condition aRL_Condition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Condition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Condition);
        }

        // GET: ARL_Condition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Conditions == null)
            {
                return NotFound();
            }

            var aRL_Condition = await _context.ARL_Conditions.FindAsync(id);
            if (aRL_Condition == null)
            {
                return NotFound();
            }
            return View(aRL_Condition);
        }

        // POST: ARL_Condition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Condition aRL_Condition)
        {
            if (id != aRL_Condition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Condition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_ConditionExists(aRL_Condition.Id))
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
            return View(aRL_Condition);
        }

        // GET: ARL_Condition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Conditions == null)
            {
                return NotFound();
            }

            var aRL_Condition = await _context.ARL_Conditions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Condition == null)
            {
                return NotFound();
            }

            return View(aRL_Condition);
        }

        // POST: ARL_Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Conditions == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Conditions'  is null.");
            }
            var aRL_Condition = await _context.ARL_Conditions.FindAsync(id);
            if (aRL_Condition != null)
            {
                _context.ARL_Conditions.Remove(aRL_Condition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_ConditionExists(int id)
        {
            return (_context.ARL_Conditions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ConditionDropdown()
        {
            List<ARL_ConditionViewModel.ARL_Condition> conditions = _dropdownService.GetConditionsFromDatabase();
            return PartialView("_ConditionDropdown", conditions);
        }
    }
}
