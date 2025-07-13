using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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
using AppRat.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using AppRat.Services;

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "InsuranceDropdown")]
    public class ARL_InsuranceDropdownViewComponent : ViewComponent
    {
        private readonly IDropdownService _dropdownService;

        public ARL_InsuranceDropdownViewComponent(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ARL_InsuranceViewModel.ARL_Insurance> insurances = _dropdownService.GetInsurancesFromDatabase();

            var viewModel = new ARL_InsuranceViewModel
            {
                aRL_Insurances = insurances,
                Insurance_Id = 1 // Set the selected ID here
            };

            return View("_InsuranceDropdown", viewModel);
        }
    }
}