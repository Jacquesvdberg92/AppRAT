namespace AppRat.Services
{
    public class AR_NotFoundException : Exception
    {
        public AR_NotFoundException()
        {
        }

        public AR_NotFoundException(string message) : base(message)
        {
        }

        public AR_NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
