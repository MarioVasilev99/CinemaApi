using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Room;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationRowValidation : INewReservation
    {
        private readonly IRoomRepository roomRepo;
        private readonly INewReservation newReservation;

        public NewReservationRowValidation(IRoomRepository roomRepo, INewReservation newReservation)
        {
            this.roomRepo = roomRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            RoomSeatsRowsDto room = roomRepo.GetRoomSeatsRowsInfo(reservation.ProjectionId);

            if (reservation.Row > room.Rows ||
                reservation.Row < 0)

            {
                //TODO: To Remove Magic String.
                return new NewReservationSummary(false, string.Format(
                    ErrorMessagesHelper.ReservationRowNumberNotValid,
                    reservation.Row));
            }

            return newReservation.New(reservation);
        }
    }
}
