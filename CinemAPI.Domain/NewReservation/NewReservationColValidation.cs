using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Dtos.Room;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationColValidation : INewReservation
    {
        private readonly IRoomRepository roomRepo;
        private readonly INewReservation newReservation;

        public NewReservationColValidation(IRoomRepository roomRepo, INewReservation newReservation)
        {
            this.roomRepo = roomRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            RoomSeatsRowsDto room = roomRepo.GetRoomSeatsRowsInfo(reservation.ProjectionId);

            if (reservation.Col > room.SeatsPerRow ||
                reservation.Col < 0)
            {
                return new NewReservationSummary(false, string.Format(ErrorMessagesHelper.ReservationColumnNotValid, reservation.Col));
            }

            return newReservation.New(reservation);
        }
    }
}
