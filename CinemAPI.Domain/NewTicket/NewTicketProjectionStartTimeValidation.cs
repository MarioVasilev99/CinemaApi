using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Dtos.Projection;
using System;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketProjectionStartTimeValidation : INewTicket
    {
        private readonly INewTicket newTicket;
        private readonly IProjectionRepository projectRepo;

        public NewTicketProjectionStartTimeValidation(INewTicket newTicket, IProjectionRepository projectRepo)
        {
            this.newTicket = newTicket;
            this.projectRepo = projectRepo;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            ProjectionStartDto projection = projectRepo.GetProjectionStartDate(ticket.ProjectionId);

            DateTime projectionStartTime = projection.StartDate;
            DateTime now = DateTime.UtcNow;

            TimeSpan timeDiff = projectionStartTime - now;
            double timeDiffInMinutes = timeDiff.TotalMinutes;

            if (timeDiffInMinutes < 0)
            {
                return new NewTicketSummary(false, ErrorMessagesHelper.TickerProjectionPassed);
            }

            return newTicket.New(ticket);
        }
    }
}
