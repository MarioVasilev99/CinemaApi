using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Projection;
using System;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationProjectionFinishedValidation : INewReservation
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly INewReservation newReservation;

        public NewReservationProjectionFinishedValidation(IProjectionRepository projectionRepo, INewReservation newReservation)
        {
            this.projectionRepo = projectionRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            ProjectionStartDto projection = projectionRepo.GetProjectionStartDate(reservation.ProjectionId);

            DateTime now = DateTime.UtcNow;

            if (projection.StartDate < now)
            {
                return new NewReservationSummary(false, ErrorMessagesHelper.ReservationProjectionFinished);
            }

            return newReservation.New(reservation);
        }
    }
}
