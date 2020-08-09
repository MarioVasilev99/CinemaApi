using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Models
{
    public class Ticket : ITicket, ITicketCreation
    {
        public Ticket(long projectionId, int row, int col)
        {
            this.ProjectionId = projectionId;
            this.Row = row;
            this.Col = col;
        }

        public long Id { get; set; }

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}
