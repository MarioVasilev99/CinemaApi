namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservation
    {
        long Id { get; }

        long ProjectionId { get; }

        int Row { get; }

        int Col { get; }
    }
}
