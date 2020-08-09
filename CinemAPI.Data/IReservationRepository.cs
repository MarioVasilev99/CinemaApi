using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Reservation;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        void Insert(IReservationCreation reservation);

        void CancelReservation(long reservationId);

        bool DoesReservationExists(long projectionId, int row, int col);

        bool DoesReservationExists(long reservationId);

        bool GetReservationStatus(long reservationId);

        long GetReservationProjectionId(long reservationId);

        IReservation GetReservationById(long reservationId);

        ReservationTicket GetReservationTicket(long projectionId, int row, int col);

        ReservationValidationData GetReservationValidationData(long reservationId);
    }
}
