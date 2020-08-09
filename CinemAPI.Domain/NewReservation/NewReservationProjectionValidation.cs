    using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationProjectionValidation : INewReservation
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly INewReservation newReservation;

        public NewReservationProjectionValidation(IProjectionRepository projectionRepo, INewReservation newReservation)
        {
            this.projectionRepo = projectionRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            bool projectionExist = projectionRepo.DoesProjectionExist(reservation.ProjectionId);

            if (projectionExist == false)
            {
                return new NewReservationSummary(false, string.Format(
                    ErrorMessagesHelper.ReservationProjectionNotExist,
                    reservation.ProjectionId));
            }

            return newReservation.New(reservation);
        }
    }
}
