using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppRat.ViewModels
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public List<SelectListItem> AllRoles { get; set; }
        public string SelectedRoleId { get; set; }
    }
}
