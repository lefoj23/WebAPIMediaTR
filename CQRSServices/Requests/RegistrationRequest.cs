namespace CQRSServices.Requests
{
    public class RegistrationRequest
    {      
        public required string accountIdentity { set; get; }
        public required string password { set; get; }
        public required string firstName { set; get; }
        public required string lastName { set; get; }

    }
}