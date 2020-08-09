namespace CinemAPI.Models.Input.Ticket
{
    public class TicketCreationModel
    {
        public long ProjectionId { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}