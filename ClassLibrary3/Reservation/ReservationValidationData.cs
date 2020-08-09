using System;

namespace CinemAPI.Models.Dtos.Reservation
{
    public class ReservationValidationData
    {
        public DateTime ProjectionStartDate { get; set; }

        public bool IsCancelled { get; set; }
    }
}
