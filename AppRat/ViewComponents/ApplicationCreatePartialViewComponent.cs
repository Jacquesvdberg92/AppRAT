using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppRat.Models;
using AppRat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppRat.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using AppRat.Services;
using System.Security.Claims;

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "ApplicationCreatePartial")]
    public class ApplicationCreatePartialViewComponent : ViewComponent
    {
        private readonly IApplicationsService _applicationsService;

        public ApplicationCreatePartialViewComponent(IApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal User)
        {
            var model = await _applicationsService.GetModelDataAsync(User);

            return View("_ApplicationCreatePartial", model);
        }

        //private readonly AppRatDbContext _context;
        //private readonly UserManager<IdentityUser> _userManager;

        //public ApplicationCreatePartialViewComponent(AppRatDbContext context, UserManager<IdentityUser> userManager)
        //{
        //	_context = context;
        //	_userManager = userManager;
        //}

        //      [HttpGet]
        //      public async Task<IViewComponentResult> InvokeAsync()
        //{
        //	var model = await GetModelDataAsync();

        //	return View("_ApplicationCreatePartial", model);
        //}

        //      private async Task<AR_Application> GetModelDataAsync()
        //      {
        //          var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

        //          var viewModel = new AR_Application
        //          {
        //              Franchise = "1",
        //              UserId = "1"
        //          };
        //          return viewModel;
        //      }
    }
}
