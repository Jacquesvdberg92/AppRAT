using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Identity;
using AppRat.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using AppRat.Services;

namespace AppRat.Controllers
{
    [Authorize]
    public class AR_ApplicationController : Controller
    {
		private readonly ILogger<HomeController> _logger;
		private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IApplicationsService _applicationsService;

        public AR_ApplicationController(ILogger<HomeController> logger, AppRatDbContext context, UserManager<IdentityUser> userManager, IApplicationsService applicationsService)
        {
			_logger = logger;
			_context = context;
            _userManager = userManager;
            _applicationsService = applicationsService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetApplicationsPartialView(DateTime? startDate, DateTime? endDate)
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            ARR_DealerLink? DealerLink = await _context.ARR_DealerLink.FirstOrDefaultAsync(o => o.UserId == user.Id);

            try
            {
                if (!startDate.HasValue || !endDate.HasValue)
                {
                    // If startDate and endDate are not provided, set them to represent the current month
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    endDate = startDate.Value.AddMonths(1).AddDays(-1);
                }

                if (DealerLink == null)
                {
                    return Problem("DealerLink not found.");
                }

                if (_context.AR_Applications == null)
                {
                    return Problem("Entity set 'AppRatDbContext.AR_Applications' is null.");
                }

                // Get the current date
                DateTime currentDate = DateTime.Now;

                //Filter applications between the selected date range
                var applicationQuery = _context.AR_Applications
                    .Where(o => o.Date >= startDate && o.Date <= endDate)
                    .AsQueryable();

                var ar_applications = await applicationQuery.ToListAsync();

                if (ar_applications == null || !ar_applications.Any())
                {
                    return Problem("No applications found for the current month.");
                }

                var conditions = await _context.ARL_Conditions.ToDictionaryAsync(c => c.Id, c => c.Description);
                var dealers = await _context.ARL_Dealerships.ToDictionaryAsync(c => c.Id, c => c.Description);
                var results = await _context.ARL_Results.ToDictionaryAsync(c => c.Id, c => c.Description);
                var insurances = await _context.ARL_Insurances.ToDictionaryAsync(c => c.Id, c => c.Description);
                var remarks = await _context.ARL_Remarks.ToDictionaryAsync(c => c.Id, c => c.Description);
                var salesPeople = await _context.ARL_SalesPeople.ToDictionaryAsync(c => c.Id, c => c.Description);

                var viewModel = new AR_ApplicationViewModel
                {
                    AR_ApplicationsWithLookups = ar_applications.Select(o => new AR_ApplicationViewModel.AR_ApplicationWithLookup
                    {
                        Id = o.Id,
                        Franchise = o.Franchise,
                        UserId = o.UserId,
                        DealerId = o.DealerId,
                        SalesPeople = o.SalesPeople,
                        Client = o.Client,
                        Date = o.Date,
                        Results_Id = o.Results_Id,
                        Condition_Id = o.Condition_Id,
                        Validated = o.Validated,
                        Invoiced = o.Invoiced,
                        Signed = o.Signed,
                        Insurance_Id = o.Insurance_Id,
                        TradeIn = o.TradeIn,
                        Paid = o.Paid,
                        Spotter = o.Spotter,
                        ClientOutOfTown = o.ClientOutOfTown,
                        Remarks_Id = o.Remarks_Id,
                        Comments = o.Comments,
                        ConditionDescription = conditions.TryGetValue(o.Condition_Id, out string value0) ? value0 : string.Empty,
                        SalesPersonDescription = salesPeople.TryGetValue(o.SalesPeople, out string value5) ? value5 : string.Empty,
                        DealerDescription = dealers.TryGetValue(o.DealerId, out string value1) ? value1 : string.Empty,
                        ResultDescription = results.TryGetValue(o.Results_Id, out string value2) ? value2 : string.Empty,
                        InsuranceDescription = insurances.TryGetValue(o.Insurance_Id, out string value3) ? value3 : string.Empty,
                        RemarkDescription = remarks.TryGetValue(o.Remarks_Id, out string value4) ? value4 : string.Empty
                    }).ToList()
                };

                return PartialView("_ApplicationsContent", viewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log them, and return an appropriate error response
                return Problem("An error occurred while processing the request.", statusCode: 500);
            }
        }

        // GET: AR_Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AR_Applications == null)
            {
                return NotFound();
            }

            var aR_Application = await _context.AR_Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Application == null)
            {
                return NotFound();
            }

            return View(aR_Application);
        }

        // POST: AR_Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Franchise,UserId,DealerId,SalesPeople,Client,Date,Results_Id,Condition_Id,Validated,Invoiced,Signed,Insurance_Id,TradeIn,Paid,Spotter,ClientOutOfTown,Remarks_Id,Comments")] Models.AR_Application aR_Application)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await _userManager.GetUserAsync(User);

                aR_Application.Franchise = "1";
                aR_Application.UserId = user.Id;

                _context.Add(aR_Application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Use the service to get the model data
            var model = await _applicationsService.GetModelDataAsync(User);

            // Use the ViewComponent method to render the view component with the retrieved model data
            return ViewComponent("ApplicationCreatePartial", model);
        }

        // GET: AR_Application/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var aR_Application = await _applicationsService.GetAR_ApplicationAsync(id);

            if (aR_Application == null)
            {
                return NotFound();
            }

            // Use the view component to render the edit form
            return ViewComponent("ApplicationEditPartial", new { id, aR_Application });
            //return PartialView("_ApplicationEditPartial", aR_Application);
        }

        // POST: AR_Application/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Franchise,UserId,DealerId,SalesPeople,Client,Date,Results_Id,Condition_Id,Validated,Invoiced,Signed,Insurance_Id,TradeIn,Paid,Spotter,ClientOutOfTown,Remarks_Id,Comments")] Models.AR_Application aR_Application)
        {
            if (id != aR_Application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _applicationsService.UpdateAR_ApplicationAsync(aR_Application);
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, return the ApplicationEditPartialViewComponent with the model data
            return ViewComponent("ApplicationEditPartial", new { id = aR_Application.Id, model = aR_Application });
        }

        // GET: AR_Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AR_Applications == null)
            {
                return NotFound();
            }

            var aR_Application = await _context.AR_Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aR_Application == null)
            {
                return NotFound();
            }

            return View(aR_Application);
        }

        // POST: AR_Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AR_Applications == null)
            {
                return Problem("Entity set 'AppRatDbContext.AR_Applications'  is null.");
            }
            var aR_Application = await _context.AR_Applications.FindAsync(id);
            if (aR_Application != null)
            {
                _context.AR_Applications.Remove(aR_Application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AR_ApplicationExists(int id)
        {
            return (_context.AR_Applications?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ExportToExcel([FromBody] AR_ApplicationViewModel AR_ApplicationViewModel)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tools");

            // Add header row
            var headers = new[]
                {
                        "Id", "Franchise", "User ID", "Sales People", "Client", "Date",
                        "Results ID", "Condition ID", "Validated", "Invoiced", "Signed",
                        "Insurance ID", "TradeIn", "Paid", "Spotter", "Client Out Of Town",
                        "Remarks ID", "Comments", "Dealer ID"
                    };
            for (var i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // Populate data rows
            var rowIndex = 2;

            if (AR_ApplicationViewModel.AR_Applications != null)
            {
                foreach (var ar_applications in AR_ApplicationViewModel.AR_Applications)
                {
                    worksheet.Cell(rowIndex, 1).Value = ar_applications.Id;
                    worksheet.Cell(rowIndex, 2).Value = ar_applications.Franchise;
                    worksheet.Cell(rowIndex, 3).Value = ar_applications.UserId;
                    worksheet.Cell(rowIndex, 4).Value = ar_applications.DealerId;
                    worksheet.Cell(rowIndex, 5).Value = ar_applications.SalesPeople;
                    worksheet.Cell(rowIndex, 6).Value = ar_applications.Client;
                    worksheet.Cell(rowIndex, 7).Value = ar_applications.Date;
                    worksheet.Cell(rowIndex, 8).Value = ar_applications.Results_Id;
                    worksheet.Cell(rowIndex, 9).Value = ar_applications.Condition_Id;
                    worksheet.Cell(rowIndex, 10).Value = ar_applications.Validated;
                    worksheet.Cell(rowIndex, 11).Value = ar_applications.Invoiced;
                    worksheet.Cell(rowIndex, 12).Value = ar_applications.Signed;
                    worksheet.Cell(rowIndex, 13).Value = ar_applications.Insurance_Id;
                    worksheet.Cell(rowIndex, 14).Value = ar_applications.TradeIn;
                    worksheet.Cell(rowIndex, 15).Value = ar_applications.Paid;
                    worksheet.Cell(rowIndex, 16).Value = ar_applications.Spotter;
                    worksheet.Cell(rowIndex, 17).Value = ar_applications.ClientOutOfTown;
                    worksheet.Cell(rowIndex, 18).Value = ar_applications.Remarks_Id;
                    worksheet.Cell(rowIndex, 19).Value = ar_applications.Comments;
                    rowIndex++;
                }
            }
            else
            {
                return NotFound();
            }

            // Create a memory stream to save the workbook
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);

            // Set response headers for downloading the file
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = "Applications.xlsx",
                Inline = false
            };
            HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet]
        [Route("/AR_Application/GetAllApplications")]
        public IActionResult GetAllTools()
        {
            var allTools = _context.AR_Applications.ToList(); // Fetch all tools from your database

            return Json(allTools); // Return tools as JSON response
        }
    }
}
