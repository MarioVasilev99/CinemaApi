using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Dtos.Projection;
using System;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketSeatFreeValidation : INewTicket
    {
        private readonly IReservationRepository reservationRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IProjectionRepository projectionRepo;
        private readonly INewTicket newTicket;

        public NewTicketSeatFreeValidation(
            IReservationRepository reservationRepo,
            ITicketRepository ticketRepo,
            IProjectionRepository projectionRepo,
            INewTicket newTicket)
        {
            this.reservationRepo = reservationRepo;
            this.ticketRepo = ticketRepo;
            this.projectionRepo = projectionRepo;
            this.newTicket = newTicket;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            bool seatReserved = reservationRepo.DoesReservationExists(ticket.ProjectionId, ticket.Row, ticket.Col);

            ProjectionStartDto projectionStartInfo = projectionRepo.GetProjectionStartDate(ticket.ProjectionId);
            TimeSpan timeTillProjectionStart = projectionStartInfo.StartDate - DateTime.UtcNow;

            bool reservationIsValid = timeTillProjectionStart.TotalMinutes > 10.0;

            bool seatBought = ticketRepo.DoesTicketExist(ticket.ProjectionId, ticket.Row, ticket.Col);

            if (seatReserved && reservationIsValid || seatBought)
            {
                return new NewTicketSummary(false, string.Format(
                    ErrorMessagesHelper.TicketSeatAlreadyTaken,
                    ticket.Row,
                    ticket.Col));
            }

            return newTicket.New(ticket);
        }
    }
}
