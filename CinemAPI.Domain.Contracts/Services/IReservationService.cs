using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Dtos.Reservation;

namespace CinemAPI.Domain.Contracts.Services
{
    public interface IReservationService
    {
        ReservationTicket GetReservationTicket(long projectionId, int row, int col);

        ReservationCancellationSummary CancelReservation(long reservationId);

        long GetReservationProjectionId(long reservationId);

        ReservationValidationSummary ValidateReservation(long reservationId);
    }
}
