using System;

namespace CinemAPI.Models.Dtos.Reservation
{
    public class ReservationTicket
    {
        public long ReservationId { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }
}