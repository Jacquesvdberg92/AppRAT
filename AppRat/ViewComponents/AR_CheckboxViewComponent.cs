using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class AR_CheckboxViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(List<AR_CheckboxItem> checkboxItems)
    {
        return View(checkboxItems);
    }
}

public class AR_CheckboxItem
{
    public string Id { get; set; }
    public string Label { get; set; }
    public bool IsChecked { get; set; }
}
