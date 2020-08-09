using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCreation : INewReservation
    {
        private readonly IReservationRepository reservationRepo;

        public NewReservationCreation(IReservationRepository reservationRepo)
        {
            this.reservationRepo = reservationRepo;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            reservationRepo.Insert(new Reservation(reservation.ProjectionId, reservation.Row, reservation.Col));

            return new NewReservationSummary(true);
        }
    }
}
