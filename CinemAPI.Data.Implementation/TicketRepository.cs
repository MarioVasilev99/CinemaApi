using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public bool DoesTicketExist(long projectionId, int row, int col)
        {
            return db.Tickets
                .Any(x => x.ProjectionId == projectionId &&
                          x.Row == row &&
                          x.Col == col);
        }

        public void Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionId, ticket.Row, ticket.Col);

            db.Tickets.Add(newTicket);

            db.SaveChanges();
        }
    }
}
