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

namespace AppRat.Controllers
{
    [Authorize]
    public class ARL_AuthController : Controller
    {
        private readonly AppRatDbContext _context;

        public ARL_AuthController(AppRatDbContext context)
        {
            _context = context;
        }

        // GET: ARL_Auth
        public async Task<IActionResult> Index()
        {
              return _context.ARL_Auths != null ? 
                          View(await _context.ARL_Auths.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.ARL_Auths'  is null.");
        }

        // GET: ARL_Auth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARL_Auths == null)
            {
                return NotFound();
            }

            var aRL_Auth = await _context.ARL_Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Auth == null)
            {
                return NotFound();
            }

            return View(aRL_Auth);
        }

        // GET: ARL_Auth/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ARL_Auth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ARL_Auth aRL_Auth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aRL_Auth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aRL_Auth);
        }

        // GET: ARL_Auth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARL_Auths == null)
            {
                return NotFound();
            }

            var aRL_Auth = await _context.ARL_Auths.FindAsync(id);
            if (aRL_Auth == null)
            {
                return NotFound();
            }
            return View(aRL_Auth);
        }

        // POST: ARL_Auth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ARL_Auth aRL_Auth)
        {
            if (id != aRL_Auth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aRL_Auth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARL_AuthExists(aRL_Auth.Id))
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
            return View(aRL_Auth);
        }

        // GET: ARL_Auth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARL_Auths == null)
            {
                return NotFound();
            }

            var aRL_Auth = await _context.ARL_Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRL_Auth == null)
            {
                return NotFound();
            }

            return View(aRL_Auth);
        }

        // POST: ARL_Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARL_Auths == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARL_Auths'  is null.");
            }
            var aRL_Auth = await _context.ARL_Auths.FindAsync(id);
            if (aRL_Auth != null)
            {
                _context.ARL_Auths.Remove(aRL_Auth);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARL_AuthExists(int id)
        {
          return (_context.ARL_Auths?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
