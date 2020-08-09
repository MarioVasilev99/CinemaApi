using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Dtos.Room;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketSeatExistValidation : INewTicket
    {
        private readonly IRoomRepository roomRepo;
        private readonly INewTicket newTicket;

        public NewTicketSeatExistValidation(IRoomRepository roomRepo, INewTicket newTicket)
        {
            this.roomRepo = roomRepo;
            this.newTicket = newTicket;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            RoomSeatsRowsDto roomSpacesInfoDto = roomRepo.GetRoomSeatsRowsInfo(ticket.ProjectionId);

            bool rowIsValid = ticket.Row <= roomSpacesInfoDto.Rows &&
                              ticket.Row >= 0;

            bool colIsValid = ticket.Col <= roomSpacesInfoDto.SeatsPerRow &&
                              ticket.Col >= 0;

            if (rowIsValid && colIsValid)
            {
                return newTicket.New(ticket);
            }

            return new NewTicketSummary(false, string.Format(
                ErrorMessagesHelper.TicketSeatNotExisting,
                roomSpacesInfoDto.Rows,
                roomSpacesInfoDto.SeatsPerRow));
        }
    }
}
