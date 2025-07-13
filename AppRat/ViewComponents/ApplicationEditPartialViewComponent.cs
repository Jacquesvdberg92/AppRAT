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

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "ApplicationEditPartial")]
    public class ApplicationEditPartialViewComponent : ViewComponent
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationEditPartialViewComponent(AppRatDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await GetModelDataAsync(id);

            return View("_ApplicationEditPartial", model);
        }

        private async Task<AR_Application> GetModelDataAsync(int id)
        {
            // Retrieve the AR_Application with the specified ID from your data source
            var application = await _context.AR_Applications.FindAsync(id);

            if (application == null)
            {
                // Handle the null case appropriately
                throw new KeyNotFoundException($"Application with ID {id} not found.");
            }

            // You can add additional logic here to load related data if needed

            return application;
        }
    }

    //[ViewComponent(Name = "ApplicationEditPartial")]1
    //public class ApplicationEditPartialViewComponent : ViewComponent
    //{
    //	private readonly AppRatDbContext _context;
    //	private readonly UserManager<IdentityUser> _userManager;

    //	public ApplicationEditPartialViewComponent(AppRatDbContext context, UserManager<IdentityUser> userManager)
    //	{
    //		_context = context;
    //		_userManager = userManager;
    //	}

    //       [HttpGet]
    //       public async Task<IViewComponentResult> InvokeAsync()
    //	{
    //		var model = await GetModelDataAsync();

    //		return View("_ApplicationEditPartial", model);
    //	}

    //       private async Task<AR_Application> GetModelDataAsync()
    //       {
    //           var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

    //           //var userBasketItems = await _context.Basket
    //           //    .Where(o => o.UserId == user.Id)
    //           //    .Select(o => new AR_ApplicationCreateViewModel.Basket
    //           //    {
    //           //        Id = o.Id,
    //           //        UserId = o.UserId,
    //           //        ToolId = o.ToolId,
    //           //        Tool_Description = o.Tool_Description
    //           //    })
    //           //    .ToListAsync();

    //           //         var viewModel = new AR_ApplicationCreateViewModel
    //           //{
    //           //             //BasketItems = userBasketItems
    //           //         };

    //           var viewModel = new AR_Application
    //           {
    //               //BasketItems = userBasketItems
    //               Franchise = "1",
    //               UserId = "1"
    //           };
    //           return viewModel;
    //       }
    //   }
}
