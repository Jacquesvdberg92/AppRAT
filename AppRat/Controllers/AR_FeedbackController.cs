using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Identity;

namespace AppRat.Controllers
{
    public class AR_FeedbackController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AR_FeedbackController(AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: AR_Feedback
        public async Task<IActionResult> Index()
        {
              return _context.AR_Feedback != null ? 
                          View(await _context.AR_Feedback.ToListAsync()) :
                          Problem("Entity set 'AppRatDbContext.AR_Feedback'  is null.");
        }

        // GET: AR_Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AR_Feedback == null)
            {
                return NotFound();
            }

            var aR_Feedback = await _context.AR_Feedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Feedback == null)
            {
                return NotFound();
            }

            return View(aR_Feedback);
        }

        // GET: AR_Feedback/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AR_Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Heading,Feedback,Image")] AR_Feedback aR_Feedback)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await _userManager.GetUserAsync(User);
                aR_Feedback.UserId = "1";
                aR_Feedback.UserId = user.Id;

                _context.Add(aR_Feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aR_Feedback);
        }

        // GET: AR_Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AR_Feedback == null)
            {
                return NotFound();
            }

            var aR_Feedback = await _context.AR_Feedback.FindAsync(id);
            if (aR_Feedback == null)
            {
                return NotFound();
            }
            return View(aR_Feedback);
        }

        // POST: AR_Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Heading,Feedback,Image")] AR_Feedback aR_Feedback)
        {
            if (id != aR_Feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aR_Feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AR_FeedbackExists(aR_Feedback.Id))
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
            return View(aR_Feedback);
        }

        // GET: AR_Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AR_Feedback == null)
            {
                return NotFound();
            }

            var aR_Feedback = await _context.AR_Feedback
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Feedback == null)
            {
                return NotFound();
            }

            return View(aR_Feedback);
        }

        // POST: AR_Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AR_Feedback == null)
            {
                return Problem("Entity set 'AppRatDbContext.AR_Feedback'  is null.");
            }
            var aR_Feedback = await _context.AR_Feedback.FindAsync(id);
            if (aR_Feedback != null)
            {
                _context.AR_Feedback.Remove(aR_Feedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AR_FeedbackExists(int id)
        {
          return (_context.AR_Feedback?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
