using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketProjectionExistValidation : INewTicket
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly INewTicket newTicket;

        public NewTicketProjectionExistValidation(IProjectionRepository projectionRepo, INewTicket newTicket)
        {
            this.projectionRepo = projectionRepo;
            this.newTicket = newTicket;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            bool projectionExist = projectionRepo.DoesProjectionExist(ticket.ProjectionId);

            if (projectionExist == false)
            {
                return new NewTicketSummary(false, string.Format(
                    ErrorMessagesHelper.TicketProjectionNotExist,
                    ticket.ProjectionId));
            }

            return newTicket.New(ticket);
        }
    }
}
