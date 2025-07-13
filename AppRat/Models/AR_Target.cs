namespace AppRat.Models
{
    public class AR_Target
    {
        public int Id { get; set; }
        public string UserId { get; set; }
		public int DealerId { get; set; }
		public int New { get; set; }
        public int Used { get; set; }
        public DateTime Date { get; set; }
    }
}
