using System.Data.Entity.Core;
using System.Linq;

using CinemAPI.Data.EF;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Reservation;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;

        public ReservationRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public bool DoesReservationExists(long projectionId, int row, int col)
        {
            return db.Reservations
                .Any(x => x.ProjectionId == projectionId &&
                          x.Row == row &&
                          x.Col == col);
        }

        public bool DoesReservationExists(long reservationId) => db.Reservations.Any(x => x.Id == reservationId);

        public ReservationTicket GetReservationTicket(long projectionId, int row, int col)
        {
            return db.Reservations
                .Where(x => x.ProjectionId == projectionId &&
                            x.Row == row &&
                            x.Col == col)
                .Select(x => new ReservationTicket()
                {
                    ReservationId = x.Id,
                    ProjectionStartDate = x.Projection.StartDate,
                    MovieName = x.Projection.Movie.Name,
                    CinemaName = x.Projection.Room.Cinema.Name,
                    RoomNumber = x.Projection.Room.Number,
                    Row = x.Row,
                    Col = x.Col,
                })
                .FirstOrDefault();
        }

        public void Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionId, reservation.Row, reservation.Col);

            db.Reservations.Add(newReservation);

            db.SaveChanges();
        }

        public void CancelReservation(long reservationId)
        {
            Reservation reservation = db.Reservations.FirstOrDefault(x => x.Id == reservationId);
            reservation.IsCancelled = true;
        }

        public long GetReservationProjectionId(long reservationId)
        {
            long projectionId = db.Reservations
                .Where(x => x.Id == reservationId)
                .Select(x => x.ProjectionId)
                .FirstOrDefault();

            return projectionId;
        }

        public bool GetReservationStatus(long reservationId)
        {
            bool reservationStatus = db.Reservations
                 .Where(x => x.Id == reservationId)
                 .Select(x => x.IsCancelled)
                 .FirstOrDefault();

            return reservationStatus;
        }

        public ReservationValidationData GetReservationValidationData(long reservationId)
        {
            return db.Reservations
                .Where(x => x.Id == reservationId)
                .Select(x => new ReservationValidationData()
                {
                    ProjectionStartDate = x.Projection.StartDate,
                    IsCancelled = x.IsCancelled,
                })
                .FirstOrDefault();
        }

        public IReservation GetReservationById(long reservationId)
        {
            return db.Reservations.FirstOrDefault(x => x.Id == reservationId);
        }
    }
}
