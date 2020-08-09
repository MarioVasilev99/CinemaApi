namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicket
    {
        long Id { get; }

        long ProjectionId { get; }

        int Row { get; }

        int Col { get; }
    }
}
