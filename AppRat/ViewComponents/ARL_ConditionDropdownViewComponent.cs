using Microsoft.AspNetCore.Mvc;

using AppRat.ViewModels;
using AppRat.Services;

namespace AppRat.ViewComponents
{
    [ViewComponent(Name = "ConditionDropdown")]
    public class ARL_ConditionDropdownViewComponent : ViewComponent
    {
        private readonly IDropdownService _dropdownService;

        public ARL_ConditionDropdownViewComponent(IDropdownService dropdownService)
        {
            _dropdownService = dropdownService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ARL_ConditionViewModel.ARL_Condition> conditions = _dropdownService.GetConditionsFromDatabase();

            var viewModel = new ARL_ConditionViewModel
            {
                aRL_Conditions = conditions,
                Condition_Id = 1 // Set the selected ID here
            };

            return View("_ConditionDropdown", viewModel);
        }
    }
}