namespace CinemAPI.Domain.Contracts.Models
{
    public class ReservationCancellationSummary
    {
        public ReservationCancellationSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public ReservationCancellationSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool IsCreated { get; set; }
    }
}
