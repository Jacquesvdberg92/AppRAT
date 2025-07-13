using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Identity;

namespace AppRat.Controllers
{
    public class AR_TargetController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AR_TargetController(AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AR_Target
        public async Task<IActionResult> Index()
        {
              return _context.AR_Target != null ? 
                          View(await _context.AR_Target.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.AR_Target'  is null.");
        }

        // GET: AR_Target/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AR_Target == null)
            {
                return NotFound();
            }

            var aR_Target = await _context.AR_Target
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Target == null)
            {
                return NotFound();
            }

            return View(aR_Target);
        }

        // GET: AR_Target/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var model = new AR_Target { UserId = userId };
            return View(model);
        }

        // POST: AR_Target/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,New,Used,Date")] AR_Target aR_Target)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await _userManager.GetUserAsync(User);
                aR_Target.UserId = "1";
                aR_Target.UserId = user.Id;

                _context.Add(aR_Target);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aR_Target);
        }

        // GET: AR_Target/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AR_Target == null)
            {
                return NotFound();
            }

            var aR_Target = await _context.AR_Target.FindAsync(id);
            if (aR_Target == null)
            {
                return NotFound();
            }
            return View(aR_Target);
        }

        // POST: AR_Target/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,New,Used,Date")] AR_Target aR_Target)
        {
            if (id != aR_Target.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aR_Target);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AR_TargetExists(aR_Target.Id))
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
            return View(aR_Target);
        }

        // GET: AR_Target/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AR_Target == null)
            {
                return NotFound();
            }

            var aR_Target = await _context.AR_Target
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Target == null)
            {
                return NotFound();
            }

            return View(aR_Target);
        }

        // POST: AR_Target/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AR_Target == null)
            {
                return Problem("Entity set 'AppRatDbContext.AR_Target'  is null.");
            }
            var aR_Target = await _context.AR_Target.FindAsync(id);
            if (aR_Target != null)
            {
                _context.AR_Target.Remove(aR_Target);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AR_TargetExists(int id)
        {
          return (_context.AR_Target?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
