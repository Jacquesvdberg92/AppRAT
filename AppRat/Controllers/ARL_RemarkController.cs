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
    public class ARL_RemarkController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly IDropdownService _dropdownService;

        public ARL_RemarkController(AppRatDbContext context, IDropdownService dropdownService)
        {
            _context = context;
            _dropdownService = dropdownService;
        }

        // GET: ARL_Remark
        public async Task<IActionResult> Index()
        {
              return _context.ARL_Remarks != null ? 
                          View(await _context.ARL_Remarks.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_Remarks'  is null.");
        }

        // GET: ARL_Remark/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Remarks == null)
            {
                return NotFound();
            }

            var aRL_Remark = await _context.ARL_Remarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Remark == null)
            {
                return NotFound();
            }

            return View(aRL_Remark);
        }

        // GET: ARL_Remark/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Remark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Remark aRL_Remark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Remark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Remark);
        }

        // GET: ARL_Remark/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Remarks == null)
            {
                return NotFound();
            }

            var aRL_Remark = await _context.ARL_Remarks.FindAsync(id);
            if (aRL_Remark == null)
            {
                return NotFound();
            }
            return View(aRL_Remark);
        }

        // POST: ARL_Remark/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Remark aRL_Remark)
        {
            if (id != aRL_Remark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Remark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_RemarkExists(aRL_Remark.Id))
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
            return View(aRL_Remark);
        }

        // GET: ARL_Remark/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Remarks == null)
            {
                return NotFound();
            }

            var aRL_Remark = await _context.ARL_Remarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Remark == null)
            {
                return NotFound();
            }

            return View(aRL_Remark);
        }

        // POST: ARL_Remark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Remarks == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Remarks'  is null.");
            }
            var aRL_Remark = await _context.ARL_Remarks.FindAsync(id);
            if (aRL_Remark != null)
            {
                _context.ARL_Remarks.Remove(aRL_Remark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_RemarkExists(int id)
        {
          return (_context.ARL_Remarks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult RemarkDropdown()
        {
            List<ARL_RemarkViewModel.ARL_Remark> remarks = _dropdownService.GetRemarksFromDatabase();
            return PartialView("_RemarkDropdown", remarks);
        }
    }
}
