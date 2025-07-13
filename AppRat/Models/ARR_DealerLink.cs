using Microsoft.AspNetCore.Identity;

namespace AppRat.Models
{
    public class ARR_DealerLink
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DealerId { get; set; }

        // Navigation properties
        public IdentityUser User { get; set; }
        public ARL_Dealership Dealer { get; set; }
    }
}
