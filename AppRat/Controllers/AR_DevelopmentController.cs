using AppRat.Data;
using AppRat.Models;
using AppRat.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Structures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using static AppRat.ViewModels.AR_ApplicationViewModel;

namespace AppRat.Controllers
{
    [Authorize]
    public class AR_DevelopmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AR_DevelopmentController(ILogger<HomeController> logger, AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ApplicationsConcept()
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

                // Filter applications for the current month
                //var applicationQuery = _context.AR_Applications
                //    .Where(o => o.Date.Year == currentDate.Year && o.Date.Month == currentDate.Month)
                //    .AsQueryable();

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

                var viewModel = new AR_ApplicationConceptViewModel
                {
                    AR_ApplicationsWithLookups = ar_applications.Select(o => new AR_ApplicationConceptViewModel.AR_ApplicationWithLookup
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
                        DealerDescription = dealers.TryGetValue(o.DealerId, out string value1) ? value1 : string.Empty,
                        ResultDescription = results.TryGetValue(o.Results_Id, out string value2) ? value2 : string.Empty,
                        InsuranceDescription = insurances.TryGetValue(o.Insurance_Id, out string value3) ? value3 : string.Empty,
                        RemarkDescription = remarks.TryGetValue(o.Remarks_Id, out string value4) ? value4 : string.Empty
                    }).ToList()
                };

                return PartialView("_ApplicationsContentConcept", viewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log them, and return an appropriate error response
                return Problem("An error occurred while processing the request.", statusCode: 500);
            }
        }


        //public async Task<IActionResult> GetApplicationsPartialView()
        //{
        //    IdentityUser? user = await _userManager.GetUserAsync(User);
        //    ARR_DealerLink? DealerLink = await _context.ARR_DealerLink.FirstOrDefaultAsync(o => o.UserId == user.Id);

        //    if (_context.AR_Applications == null)
        //    {
        //        return Problem("Entity set 'AppRatDbContext.AR_Applications' is null.");
        //    }

        //    // Get the current date
        //    DateTime currentDate = DateTime.Now;

        //    // Filter applications for the current month
        //    var applicationQuery = _context.AR_Applications
        //        .Where(o => o.Date.Year == currentDate.Year && o.Date.Month == currentDate.Month)
        //        .AsQueryable();

        //    var totalApplicationsCount = await applicationQuery.CountAsync();
        //    var ar_applications = await applicationQuery
        //    .ToListAsync();

        //    var conditions = await _context.ARL_Conditions.ToDictionaryAsync(c => c.Id, c => c.Description);
        //    var dealers = await _context.ARL_Dealerships.ToDictionaryAsync(c => c.Id, c => c.Description);
        //    var results = await _context.ARL_Results.ToDictionaryAsync(c => c.Id, c => c.Description);
        //    var insurances = await _context.ARL_Insurances.ToDictionaryAsync(c => c.Id, c => c.Description);
        //    var remarks = await _context.ARL_Remarks.ToDictionaryAsync(c => c.Id, c => c.Description);

        //    var viewModel = new AR_ApplicationConceptViewModel
        //    {
        //        AR_ApplicationsWithLookups = ar_applications.Select(o => new AR_ApplicationConceptViewModel.AR_ApplicationWithLookup
        //        {
        //            Id = o.Id,
        //            Franchise = o.Franchise,
        //            UserId = o.UserId,
        //            DealerId = o.DealerId,
        //            SalesPeople = o.SalesPeople,
        //            Client = o.Client,
        //            Date = o.Date,
        //            Results_Id = o.Results_Id,
        //            Condition_Id = o.Condition_Id,
        //            Validated = o.Validated,
        //            Invoiced = o.Invoiced,
        //            Signed = o.Signed,
        //            Insurance_Id = o.Insurance_Id,
        //            TradeIn = o.TradeIn,
        //            Paid = o.Paid,
        //            Spotter = o.Spotter,
        //            ClientOutOfTown = o.ClientOutOfTown,
        //            Remarks_Id = o.Remarks_Id,
        //            Comments = o.Comments,
        //            ConditionDescription = conditions.TryGetValue(o.Condition_Id, out string value0) ? value0 : string.Empty,
        //            DealerDescription = dealers.TryGetValue(o.DealerId, out string value1) ? value1 : string.Empty,
        //            ResultDescription = results.TryGetValue(o.Results_Id, out string value2) ? value2 : string.Empty,
        //            InsuranceDescription = insurances.TryGetValue(o.Insurance_Id, out string value3) ? value3 : string.Empty,
        //            RemarkDescription = remarks.TryGetValue(o.Remarks_Id, out string value4) ? value4 : string.Empty
        //        }).ToList()
        //    };

        //    return PartialView("_ApplicationsContentConcept", viewModel);
        //}

        public async Task<IActionResult> DashboardConcept()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            ARR_DealerLink? DealerLink = await _context.ARR_DealerLink.FirstOrDefaultAsync(o => o.UserId == user.Id);

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Targets Section Part1 Start																									   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            var targets = await _context.AR_Target //TODO: add to Where to select based on a selected date
                .Where(o => o.DealerId == DealerLink.DealerId)
                .Select(o => new AR_DashboardContentViewModel.AR_Target
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    DealerId = o.DealerId,
                    New = o.New,
                    Used = o.Used,
                    Date = o.Date
                })
                .ToListAsync();
            int targetNew = targets.FirstOrDefault().New;
            int targetUsed = targets.FirstOrDefault().Used;
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Target Section Part1 End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Working Days Section Start																									   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            DateTime targetMonth = targets.Select(target => target.Date.Date).Distinct().FirstOrDefault();
            DateTime currentDate = DateTime.Now;
            int workingDaysInMonth = 0;
            int workingDaysUpToToday = 0;


            int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDay = new DateTime(targetMonth.Year, targetMonth.Month, day);
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDaysInMonth++;
                    if (currentDay <= currentDate)
                    {
                        workingDaysUpToToday++;
                    }
                }
            }
            float workingDaysPercentage = (float)Math.Round(((float)workingDaysUpToToday / workingDaysInMonth) * 100, 0);
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Working Days Section End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- New Cars Section Start																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            int newCarsAppsCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 2)
                .CountAsync();
            int newCarsValidatedCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 2 && app.Validated == true)
                .CountAsync();
            int newCarsTakenUpCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 2 && app.Signed == true)
                .CountAsync();
            int newCarsNTUCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 2 && app.Signed == true)
                .CountAsync();//TODO: Some research required to determine
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- New Cars Section End																											   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Used Cars Section Start																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            int usedCarsAppsCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 3)
                .CountAsync();
            int usedCarsDemoAppsCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Condition_Id == 4)
                .CountAsync();
            int usedCarsValidatedCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && (app.Condition_Id == 3 || app.Condition_Id == 4) && app.Validated == true)
                .CountAsync();
            int usedCarsTakenUpCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && (app.Condition_Id == 3 || app.Condition_Id == 4) && app.Signed == true)
                .CountAsync();
            int usedCarsNTUCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && (app.Condition_Id == 3 || app.Condition_Id == 4) && app.Signed == false)
                .CountAsync();//TODO: Some research required to determine
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Used Cars Section End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Targets Section Part2 Start																									   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            float projectedNewCount = (newCarsTakenUpCount / workingDaysUpToToday) * workingDaysInMonth;
            float ProjectedUsedCount = (usedCarsAppsCount / workingDaysUpToToday) * workingDaysInMonth;
            float projectedTotalCount = projectedNewCount + ProjectedUsedCount;
            float dailyAppsRequiredNewCount = 0;
            if (!(projectedNewCount > targetNew))
            {
                dailyAppsRequiredNewCount = ((targetNew - newCarsTakenUpCount) * (newCarsAppsCount / newCarsTakenUpCount)) / (workingDaysInMonth - workingDaysUpToToday);
            }
            float dailyAppsRequiredUsedCount = 0;
            if (!(ProjectedUsedCount > targetUsed))
            {
                dailyAppsRequiredUsedCount = ((targetUsed - usedCarsTakenUpCount) * (usedCarsAppsCount / usedCarsTakenUpCount)) / (workingDaysInMonth - workingDaysUpToToday);
            }
            float dailyAppsRequiredTotalCount = dailyAppsRequiredNewCount + dailyAppsRequiredUsedCount;
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Target Section Part2 End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Ratio Section Part1 Start																									   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            int approvedTotalCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Results_Id == 2)
                .CountAsync();
            int declinedTotalCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Results_Id == 3)
                .CountAsync();
            int selfFinanceTotalCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Results_Id == 7)
                .CountAsync();
            int cashTotalCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Results_Id == 5)
                .CountAsync();
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Ratio Section Part1 End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- App Values Section Start																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            int totalAppsCount = newCarsAppsCount + usedCarsAppsCount + usedCarsDemoAppsCount;
            float appsPerDayCount = totalAppsCount / workingDaysUpToToday;
            int unitsSoldCount = newCarsTakenUpCount + usedCarsTakenUpCount;
            float appsPerDealCount = totalAppsCount / unitsSoldCount;
            float totalProjectedCount = (unitsSoldCount / workingDaysUpToToday) * workingDaysInMonth;
            int validatedAppsCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Validated == true)
                .CountAsync();
            int totalApps2Count = approvedTotalCount + declinedTotalCount + selfFinanceTotalCount + cashTotalCount;
            float validatedAppsRatio = (float)Math.Round(((float)validatedAppsCount / totalApps2Count), 2);
            int appsTakenUpCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Signed == true)
                .CountAsync();
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- App Values Section End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Ratio Section Part2 Start																									   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            float approvedRatio = (float)Math.Round(((float)approvedTotalCount / totalApps2Count) * 100, 2);
            float declinedRatio = (float)Math.Round(((float)declinedTotalCount / totalApps2Count) * 100, 2);
            float selfFinanceRatio = (float)Math.Round(((float)selfFinanceTotalCount / totalApps2Count) * 100, 2);
            float cashRatio = (float)Math.Round(((float)cashTotalCount / totalApps2Count) * 100, 2);
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Ratio Section Part2 End																										   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- NTU Section Start																											   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            int totalSoldCount = newCarsTakenUpCount + usedCarsTakenUpCount;
            int totalNTUCount = newCarsNTUCount + usedCarsNTUCount;
            int totalSpotterDeals = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Spotter == true)
                .CountAsync();
            int validatedSpotterDealsCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Spotter == true && app.Validated == true)
                .CountAsync();
            int spotterDealsNTUCount = await _context.AR_Applications
                .Where(app => app.DealerId == DealerLink.DealerId && app.Date.Year == targetMonth.Year && app.Date.Month == targetMonth.Month && app.Spotter == true && app.Signed == false)//TODO: this is probably wrong
                .CountAsync();
            float spotterDealsNTURatio = spotterDealsNTUCount / validatedSpotterDealsCount;
            float nTURatio = (float)totalNTUCount / (newCarsValidatedCount + usedCarsValidatedCount);
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- NTU Section End																												   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- ViewModel Start																												   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\ 
            var viewModel = new AR_DashboardContentViewModel
            {
                //Working Days
                WorkingDaysPassed = workingDaysUpToToday,
                WorkingDaysTotal = workingDaysInMonth,
                WorkingDaysPercetage = workingDaysPercentage,
                //Targets
                TargetTotal = targets.Sum(t => t.Used + t.New),
                ProjectedNew = projectedNewCount,
                ProjectedUsed = ProjectedUsedCount,
                ProjectedTotal = projectedTotalCount,
                DailyAppsRequiredNew = dailyAppsRequiredNewCount,
                DailyAppsRequiredUsed = dailyAppsRequiredUsedCount,
                DailyAppsRequiredTotal = dailyAppsRequiredTotalCount,
                AR_Targets = targets,
                //
                totalCarApps = newCarsAppsCount + usedCarsAppsCount + usedCarsDemoAppsCount,
                //New Cars
                NewCarsApps = newCarsAppsCount,
                NewCarsValidated = newCarsValidatedCount,
                NewCarsTakenUp = newCarsTakenUpCount,
                NewCarsNTU = newCarsNTUCount,
                NewCarsAppsPerDeal = newCarsAppsCount / newCarsTakenUpCount,
                //Used Cars
                UsedCarsApps = usedCarsAppsCount,
                UsedCarsDemoApps = usedCarsDemoAppsCount,
                UsedCarsValidated = usedCarsValidatedCount,
                UsedCarsTakenUp = usedCarsTakenUpCount,
                UsedCarsNTU = usedCarsNTUCount,
                UsedCarsAppsPerDeal = (usedCarsAppsCount + usedCarsDemoAppsCount) / newCarsTakenUpCount,
                //App Values
                TotalApps = totalAppsCount,
                AppsPerDay = appsPerDayCount,
                UnitsSold = unitsSoldCount,
                AppsPerDeal = appsPerDealCount,
                TotalProjected = totalProjectedCount,
                ValidatedApps = validatedAppsCount,
                TotalApps2 = totalApps2Count,
                ValidatedAppsRatio = validatedAppsRatio,
                AppsTakenUp = appsTakenUpCount,
                //Ratio
                ApprovedTotal = approvedTotalCount,
                DeclinedTotal = declinedTotalCount,
                SelfFinanceTotal = selfFinanceTotalCount,
                CashTotal = cashTotalCount,
                ApprovedRatio = approvedRatio,
                DeclinedRatio = declinedRatio,
                SelfFinanceRatio = selfFinanceRatio,
                CashRatio = cashRatio,
                //NTU
                TotalSold = totalSoldCount,
                TotalNTU = totalNTUCount,
                TotalSpotterDeals = totalSpotterDeals,
                ValidatedSpotterDeals = validatedSpotterDealsCount,
                SpotterDealsNTU = spotterDealsNTUCount,
                SpotterDealsNTURatio = spotterDealsNTURatio,
                NTURatio = nTURatio
            };
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- ViewModel End																												   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\


            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Chart vars Start																												   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            DateTime defaultStartDate = DateTime.Today.AddDays(-7);
            DateTime defaultEndDate = DateTime.Today;
            //List<DateTime> dateRange = GetDateRange(defaultStartDate, defaultEndDate);
            //ViewBag.DateRange = dateRange;
            //
            //ViewBag.ChartData = new
            //{
            //    categories = dateRange,
            //    totalUsed = GetTotalUsedData(dateRange), // Generate totalUsed dynamically
            //    totalNew = GetTotalNewData(dateRange),   // Generate totalNew dynamically
            //    totalDemo = GetTotalDemoData(dateRange), // Generate totalDemo dynamically
            //    totalNtu = GetTotalNtuData(dateRange)    // Generate totalNtu dynamically
            //};


            ///---------------------------------------------------------------------------------------------------------------------------------------\\\
            ///--- Chart Vars End																												   ---\\\
            ///---------------------------------------------------------------------------------------------------------------------------------------\\\

            return View(viewModel);
        }

        public async Task<IActionResult> AngularJSTesting()
        {
            return View();
        }
    }
}
