using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Projection;
using System;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationProjectionStartingValidation : INewReservation
    {
        private const int MinMinutesLeft = 10;


        private readonly IProjectionRepository projectionRepo;
        private readonly INewReservation newReservation;

        public NewReservationProjectionStartingValidation(IProjectionRepository projectionRepo, INewReservation newReservation)
        {
            this.projectionRepo = projectionRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            ProjectionStartDto projection = projectionRepo.GetProjectionStartDate(reservation.ProjectionId);

            if (projection == null)
            {
                return new NewReservationSummary(false, string.Format(
                    ErrorMessagesHelper.ReservationProjectionNotExist,
                    reservation.ProjectionId));
            }


            DateTime now = DateTime.UtcNow;
            TimeSpan timeTillStart = projection.StartDate - now;
            double minutesTillStart = timeTillStart.TotalMinutes;

            if (minutesTillStart < MinMinutesLeft &&
                minutesTillStart > 0)
            {
                return new NewReservationSummary(false,
                    string.Format(
                        ErrorMessagesHelper.ReservationProjectionStartSoon,
                        MinMinutesLeft));
            }
            else if (minutesTillStart < 0)
            {
                return new NewReservationSummary(false, ErrorMessagesHelper.ReservationProjectionStarted);
            }

            return newReservation.New(reservation);
        }
    }
}
