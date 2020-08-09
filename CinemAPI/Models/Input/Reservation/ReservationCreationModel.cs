namespace CinemAPI.Models.Input.Reservation
{
    public class ReservationCreationModel
    {
        public long ProjectionId { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}