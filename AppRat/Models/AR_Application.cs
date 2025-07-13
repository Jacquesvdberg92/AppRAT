using System.ComponentModel.DataAnnotations;

namespace AppRat.Models
{
    public class AR_Application
    {
        public int Id { get; set; }
        // Make Franchise not required
        [Required(AllowEmptyStrings = false)]
        public string Franchise { get; set; }
        // Make UserId not required
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }
        public int DealerId { get; set; }
        public int SalesPeople { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public int Results_Id { get; set; }
        public int Condition_Id { get; set; }
        public bool Validated { get; set; }
        public bool Invoiced { get; set; }
        public bool Signed { get; set; }
        public int Insurance_Id { get; set; }
        public bool TradeIn { get; set; }
        public bool Paid { get; set; }
        public bool Spotter { get; set; }
        public bool ClientOutOfTown { get; set; }
        public int Remarks_Id { get; set; }
        public string? Comments { get; set; }
    }
}
