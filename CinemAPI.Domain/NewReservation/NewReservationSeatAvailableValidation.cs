using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationSeatAvailableValidation : INewReservation
    {
        private readonly IReservationRepository reservationRepo;
        private readonly INewReservation newReservation;

        public NewReservationSeatAvailableValidation(IReservationRepository reservationRepo, INewReservation newReservation)
        {
            this.reservationRepo = reservationRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            bool reservationExist = reservationRepo.DoesReservationExists(
                                                    reservation.ProjectionId,
                                                    reservation.Row,
                                                    reservation.Col);

            if (reservationExist == true)
            {
                return new NewReservationSummary(false, string.Format(
                    ErrorMessagesHelper.ReservationSeatAlreadyReserved,
                    reservation.Row,
                    reservation.Col));
            }

            return newReservation.New(reservation);
        }
    }
}
