namespace AppRat.Models
{
    public class AR_Feedback
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Heading { get; set; }
        public string Feedback { get; set; }
        public string? Image { get; set; }      
    }
}
