using System.ComponentModel.DataAnnotations;


namespace AppRat.ViewModels
{
    public class ARL_UserViewModel
    {
		[Display(Name = "User ID")]
		public string User { get; set; }

		[Display(Name = "User")]
		public List<ARL_User> aRL_User { get; set; }

        public class ARL_User
        {
			[Display(Name = "ID")]
			public string Id { get; set; }

			[Display(Name = "Description")]
			public string Description { get; set; }
        }
    }
}
