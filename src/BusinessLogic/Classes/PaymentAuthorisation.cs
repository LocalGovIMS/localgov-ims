namespace BusinessLogic.Classes
{
    public class PaymentAuthorisation
    {
        public string ExpiryDate { get; set; }
        public string AuthorisationCode { get; set; }
        public string LastFourCardDigits { get; set; }

        public PaymentAuthorisation(string expiryDate, string authorisationCode, string lastFourCardDigits)
        {
            ExpiryDate = expiryDate;
            AuthorisationCode = authorisationCode;
            LastFourCardDigits = lastFourCardDigits;
        }
    }
}