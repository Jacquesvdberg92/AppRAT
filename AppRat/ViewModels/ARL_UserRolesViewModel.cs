namespace AppRat.ViewModels
{
    public class ARL_UserRolesViewModel
    {
        public string UserId { get; set; } // Added UserId
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public string RoleId { get; set; } // Added RoleId
    }
}
