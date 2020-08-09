using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCreation : INewTicket
    {
        private readonly ITicketRepository ticketRepo;

        public NewTicketCreation(ITicketRepository ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            ticketRepo.Insert(new Ticket(ticket.ProjectionId, ticket.Row, ticket.Col));

            return new NewTicketSummary(true);
        }
    }
}
