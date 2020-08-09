namespace CinemAPI.Domain.Contracts.Models
{
    public class ReservationValidationSummary
    {
        public ReservationValidationSummary(bool isValid)
        {
            this.IsValid = isValid;
        }

        public ReservationValidationSummary(bool isValid, string message)
            :this(isValid)
        {
            this.Message = message;
        }

        public bool IsValid { get; }

        public string Message { get;  }
    }
}
