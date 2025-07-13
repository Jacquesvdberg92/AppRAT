using Microsoft.AspNetCore.Mvc;
using AppRat.ViewModels;
using AppRat.Services;

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "DealershipDropdown")]
    public class ARL_DealershipDropdownViewComponent : ViewComponent
    {
        private readonly IDropdownService _dropdownService;

        public ARL_DealershipDropdownViewComponent(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ARL_DealershipViewModel.ARL_Dealership> dealerships = _dropdownService.GetDealershipsFromDatabase();

            var viewModel = new ARL_DealershipViewModel
            {
                aRL_Dealerships = dealerships,
                DealerId = 1 // Set the selected ID here
            };

            return View("_DealershipDropdown", viewModel);
        }
    }
}