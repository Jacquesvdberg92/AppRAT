using Microsoft.AspNetCore.Mvc;
using AppRat.ViewModels;
using AppRat.Services;

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "SalesPersonDropdown")]
    public class ARL_SalesPersonDropdownViewComponent : ViewComponent
    {
        private readonly IDropdownService _dropdownService;

        public ARL_SalesPersonDropdownViewComponent(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedSalesPersonId)
        {
            List<ARL_SalesPeopleViewModel.ARL_SalesPeople> salesPeople = _dropdownService.GetSalesPeopleFromDatabase();

            var viewModel = new ARL_SalesPeopleViewModel
            {
                aRL_SalesPeople = salesPeople,
                SalesPeople = selectedSalesPersonId
            };

            return View("_SalesPersonDropdown", viewModel);
        }
    }
}