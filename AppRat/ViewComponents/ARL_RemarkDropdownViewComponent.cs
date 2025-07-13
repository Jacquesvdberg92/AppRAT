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
    [ViewComponent(Name = "RemarkDropdown")]
    public class ARL_RemarkDropdownViewComponent : ViewComponent
    {
        private readonly IDropdownService _dropdownService;

        public ARL_RemarkDropdownViewComponent(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ARL_RemarkViewModel.ARL_Remark> remarks = _dropdownService.GetRemarksFromDatabase();

            var viewModel = new ARL_RemarkViewModel
            {
                aRL_Remarks = remarks,
                Remarks_Id = 1 // Set the selected ID here
            };

            return View("_RemarkDropdown", viewModel);
        }
    }
}