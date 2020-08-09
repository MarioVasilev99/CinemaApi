using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.Services;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Reservation;
using System;

namespace CinemAPI.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IReservationRepository reservationRepo;

        public ReservationService(IProjectionRepository projectionRepo, IReservationRepository reservationRepo)
        {
            this.projectionRepo = projectionRepo;
            this.reservationRepo = reservationRepo;
        }

        public ReservationCancellationSummary CancelReservation(long reservationId)
        {
            bool reservationExist = reservationRepo.DoesReservationExists(reservationId);

            if (reservationExist == false)
            {
                return new ReservationCancellationSummary(false, $"Reservation with Id {reservationId} does not exist.");
            }

            bool reservationCancelled = reservationRepo.GetReservationStatus(reservationId);

            if (reservationCancelled)
            {
                return new ReservationCancellationSummary(false, $"Reservation with Id {reservationId} is already cancelled.");
            }

            reservationRepo.CancelReservation(reservationId);
            return new ReservationCancellationSummary(true);
        }

        public long GetReservationProjectionId(long reservationId)
        {
            long projectionId = reservationRepo.GetReservationProjectionId(reservationId);
            return projectionId;
        }

        public ReservationTicket GetReservationTicket(long projectionId, int row, int col)
        {
            return reservationRepo.GetReservationTicket(projectionId, row, col);
        }

        public ReservationValidationSummary ValidateReservation(long reservationId)
        {
            ReservationValidationData reservationData = reservationRepo.GetReservationValidationData(reservationId);

            if (reservationData.IsCancelled)
            {
                return new ReservationValidationSummary(false, $"Ticket can't be bought with cancelled reservation.");
            }

            DateTime now = DateTime.UtcNow;
            TimeSpan timeTillProjectionStart = reservationData.ProjectionStartDate - now;
            double minutesRemaining = timeTillProjectionStart.TotalMinutes;

            if (minutesRemaining < 0)
            {
                return new ReservationValidationSummary(false, $"Projection has already started or finished. Reservation is invalid.");
            }
            else if (minutesRemaining < 10.0)
            {
                return new ReservationValidationSummary(false, $"Projection starts in less than 10 minutes. Reservation is invalid.");
            }

            return new ReservationValidationSummary(true);
        }
    }
}
