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
using Microsoft.AspNetCore.Identity;

namespace AppRat.Controllers
{
    [Authorize]
    public class ARR_DealerLinkController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ARR_DealerLinkController(AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ARR_DealerLink
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var dealers = await _context.ARL_Dealerships.ToListAsync();
            var dealerLinks = await _context.ARR_DealerLink.ToListAsync();

            ViewBag.Users = users;
            ViewBag.Dealers = dealers;
            ViewBag.DealerLinks = dealerLinks;

            return View();
        }

        // GET: ARR_DealerLink/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ARR_DealerLink == null)
            {
                return NotFound();
            }

            var aRR_DealerLink = await _context.ARR_DealerLink
                .Include(dl => dl.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRR_DealerLink == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(aRR_DealerLink.UserId);
            ViewBag.UserName = user?.UserName;
            ViewBag.DealerName = aRR_DealerLink.Dealer?.Description;

            return View(aRR_DealerLink);
        }

        // GET: ARR_DealerLink/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            ViewData["DealerId"] = new SelectList(_context.ARL_Dealerships.ToList(), "Id", "Description");
            return View();
        }

        // POST: ARR_DealerLink/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,DealerId")] ARR_DealerLink aRR_DealerLink)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(aRR_DealerLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["UserId"] = new SelectList(_userManager.Users.ToList(), "Id", "UserName", aRR_DealerLink.UserId);
            ViewData["DealerId"] = new SelectList(_context.ARL_Dealerships.ToList(), "Id", "Description", aRR_DealerLink.DealerId);
            return View(aRR_DealerLink);
        }

        // GET: ARR_DealerLink/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ARR_DealerLink == null)
            {
                return NotFound();
            }

            var aRR_DealerLink = await _context.ARR_DealerLink.FindAsync(id);
            if (aRR_DealerLink == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(aRR_DealerLink.UserId);
            ViewBag.UserName = user?.UserName;
            ViewData["DealerId"] = new SelectList(_context.ARL_Dealerships.ToList(), "Id", "Description");

            return View(aRR_DealerLink);
        }

        // POST: ARR_DealerLink/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DealerId")] ARR_DealerLink aRR_DealerLink)
        {
            if (id != aRR_DealerLink.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(aRR_DealerLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ARR_DealerLinkExists(aRR_DealerLink.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(aRR_DealerLink);
        }

        // GET: ARR_DealerLink/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ARR_DealerLink == null)
            {
                return NotFound();
            }

            var aRR_DealerLink = await _context.ARR_DealerLink
                .Include(dl => dl.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aRR_DealerLink == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(aRR_DealerLink.UserId);
            ViewBag.UserName = user?.UserName;
            ViewBag.DealerName = aRR_DealerLink.Dealer?.Description;

            return View(aRR_DealerLink);
        }

        // POST: ARR_DealerLink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ARR_DealerLink == null)
            {
                return Problem("Entity set 'AppRatDbContext.ARR_DealerLink'  is null.");
            }
            var aRR_DealerLink = await _context.ARR_DealerLink.FindAsync(id);
            if (aRR_DealerLink != null)
            {
                _context.ARR_DealerLink.Remove(aRR_DealerLink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ARR_DealerLinkExists(int id)
        {
          return (_context.ARR_DealerLink?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
