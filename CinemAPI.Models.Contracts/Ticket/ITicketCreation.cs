namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicketCreation
    {
        long ProjectionId { get; set; }

        int Row { get; set; }

        int Col { get; set; }
    }
}
