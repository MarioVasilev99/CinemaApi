using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Models
{
    public class Reservation : IReservation, IReservationCreation
    {
        public Reservation()
        {
        }

        public Reservation(long projectionId, int row, int col)
        {
            this.ProjectionId = projectionId;
            this.Row = row; 
            this.Col = col;
            this.IsCancelled = false;
        }
        
        public long Id { get; set; }

        public long ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public bool IsCancelled { get; set; }
    }
}
